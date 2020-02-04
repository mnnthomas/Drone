using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    /// <summary>
    /// Handles drone movement in X,Y,Z axis and also updates drone's forward based on camera's forward.
    /// The sound for drone's Idle and movement is also handled here
    /// </summary>
    public class DroneMovementController : MonoBehaviour
    {
        [Header("Keyboard axis keys")]
        [SerializeField] private string m_HorizontalKey = default;
        [SerializeField] private string m_ForwardKey = default;
        [SerializeField] private string m_VerticalKey = default;

        [Header("Drone movement speed & angles")]
        [SerializeField] private float m_HorizontalSpeed = default;
        [SerializeField] private float m_ForwardSpeed = default;
        [SerializeField] private float m_VerticalSpeed = default;
        [SerializeField] private float m_TurnSpeed = default;
        [SerializeField] private float m_DashDistance = default;
        [SerializeField] private float m_DashDuration = default;

        [Header("Drone movement audio")]
        [SerializeField] private Utilities.MinMax m_MovingPitchRange = default;
        [SerializeField] private Utilities.MinMax m_IdlingPitchRange = default;
        [SerializeField] private float m_MovingVolume = default;
        [SerializeField] private float m_IdlingVolume = default;

        [Header("Experiemental feature")]
        [SerializeField] private bool m_AllowTilting = default;
        [SerializeField] private float m_TiltAngle = default;

        [Header("Object references")]
        [SerializeField] private Camera m_Camera = default;
        [SerializeField] private GameObject m_DroneMesh = default;

        private AudioSource mAudioSource;
        private Rigidbody mRigidbody;
        private bool mIsIdlePlaying;

        private string mPreviousClickedKey;
        private float mPreviousClickedAxis;
        private float mPreviousClickTime;
        private bool mIsDashing;

        private void Start()
        {
            mAudioSource = GetComponent<AudioSource>();
            mRigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Move();
            CheckDoubleClick();
        }

        private void LateUpdate()
        {
            Turn();
        }

        /// <summary>
        /// get the keyboard inputs and decides movementVector for drone every frame.
        /// </summary>
        private void Move()
        {
            Vector3 movementVector = default;
            if (!mIsDashing)
            {
                movementVector = new Vector3(Input.GetAxis(m_HorizontalKey) * m_HorizontalSpeed, Input.GetAxis(m_VerticalKey) * m_VerticalSpeed, Input.GetAxis(m_ForwardKey) * m_ForwardSpeed) * Time.deltaTime;
                transform.Translate(movementVector, Space.Self);
            }

            UpdateEngineSound(movementVector);
        }

        /// <summary>
        /// Checks for doubleClick on the drone movement input keys in update.
        /// Decides the direction of dashing based on the double Click of input keys.
        /// </summary>
        private void CheckDoubleClick()
        {
            if(Time.time - mPreviousClickTime > 0.5f && !string.IsNullOrEmpty(mPreviousClickedKey))
            {
                mPreviousClickedKey = default;
                mPreviousClickTime = default;
                mPreviousClickedAxis = default;
            }

            if(Input.GetButtonDown(m_HorizontalKey) || Input.GetButtonDown(m_VerticalKey) || Input.GetButtonDown(m_ForwardKey))
            {
                string curKey = default;
                if (Input.GetButtonDown(m_HorizontalKey))
                    curKey = m_HorizontalKey;
                else if (Input.GetButtonDown(m_VerticalKey))
                    curKey = m_VerticalKey;
                else if (Input.GetButtonDown(m_ForwardKey))
                    curKey = m_ForwardKey;

                if (!string.IsNullOrEmpty(curKey))
                {
                    if (string.IsNullOrEmpty(mPreviousClickedKey) || mPreviousClickedKey != curKey || ((mPreviousClickedKey == curKey ) && Utilities.IsPositiveValue(mPreviousClickedAxis) != Utilities.IsPositiveValue(Input.GetAxis(curKey))))
                    {
                        mPreviousClickedKey = curKey;
                        mPreviousClickedAxis = Input.GetAxis(mPreviousClickedKey);
                        mPreviousClickTime = Time.time;
                        return;
                    }
                    else if (mPreviousClickedKey == curKey && Utilities.IsPositiveValue(mPreviousClickedAxis) == Utilities.IsPositiveValue(Input.GetAxis(curKey)) && Time.time - mPreviousClickTime <= 0.5f)
                    {
                        bool isPositiveAxis = Utilities.IsPositiveValue(Input.GetAxis(curKey));
                        if (curKey == m_ForwardKey)
                            StartCoroutine(Dash(isPositiveAxis ? transform.forward : -transform.forward));
                        else if (curKey == m_VerticalKey)
                            StartCoroutine(Dash(isPositiveAxis ? transform.up : -transform.up));
                        else if (curKey == m_HorizontalKey)
                            StartCoroutine(Dash(isPositiveAxis ? transform.right : -transform.right));

                        mPreviousClickedKey = default;
                        mPreviousClickTime = default;
                        mPreviousClickedAxis = default;
                    }
                }
            }            
        }


        /// <summary>
        /// Coroutine to dash the drone in the given direction
        /// </summary>
        /// <param name="direction">provides the direction vector for the drone to move</param>
        /// <returns>null</returns>
        IEnumerator Dash(Vector3 direction)
        {
            Vector3 velocity = Vector3.zero;
            Vector3 targetPosition = transform.position + direction * m_DashDistance;
            if (targetPosition.y < 1.5f)
                targetPosition.y = 1.5f;

            mIsDashing = true;
            float startTime = Time.time;
            float step = default;
            while (step < 1 || Time.time - startTime <= m_DashDuration)
            {
                step += Time.deltaTime / m_DashDuration;
                transform.position = Vector3.Lerp(transform.position, targetPosition, step);
                yield return null;
            }
            mIsDashing = false;
        }


        /// <summary>
        /// Handles Engine sound based on the movement vector. 
        /// Idle and movement sounds
        /// </summary>
        /// <param name="movement">current movement vector</param>
        private void UpdateEngineSound(Vector3 movement)
        {
            if(movement == Vector3.zero && !mIsIdlePlaying)
            {
                //drone is Idling
                mIsIdlePlaying = true;
                mAudioSource.volume = m_IdlingVolume;
                mAudioSource.pitch = Random.Range(m_IdlingPitchRange.Min, m_IdlingPitchRange.Max);
                mAudioSource.Play();
            }
            else if(movement != Vector3.zero && mIsIdlePlaying)
            {
                mIsIdlePlaying = false;
                mAudioSource.volume = m_MovingVolume;
                mAudioSource.pitch = Random.Range(m_MovingPitchRange.Min, m_MovingPitchRange.Max);
                mAudioSource.Play();
            }
        }

        /// <summary>
        /// Turns the drone towards camera's forward. 
        /// Added an experimental drone tilt feature.
        /// </summary>
        private void Turn()
        {
            if(!mIsDashing)
            {

                Quaternion cameraRot = m_Camera.transform.rotation;

                Vector3 velocity = Vector3.zero;
                transform.forward = Vector3.SmoothDamp(transform.forward, m_Camera.transform.forward, ref velocity, Time.deltaTime, m_TurnSpeed);

                if (m_AllowTilting)
                {
                    Quaternion quaternion = Quaternion.Euler(Input.GetAxis(m_ForwardKey) * m_TiltAngle, 0f, -Input.GetAxis(m_HorizontalKey) * m_TiltAngle);
                    m_DroneMesh.transform.localRotation = quaternion;
                }

                m_Camera.transform.rotation = cameraRot;
            }
        }
    }
}

