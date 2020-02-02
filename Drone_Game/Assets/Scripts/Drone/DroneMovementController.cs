using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    /// <summary>
    /// Handles drone movement in X,Y,Z axis and also updates drone's forward based on camera's forward.
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
        private string mClickTime;

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
            Vector3 movementVector = new Vector3(Input.GetAxis(m_HorizontalKey) * m_HorizontalSpeed, Input.GetAxis(m_VerticalKey) * m_VerticalSpeed, Input.GetAxis(m_ForwardKey) * m_ForwardSpeed) * Time.deltaTime;
            transform.Translate(movementVector, Space.Self);

            UpdateEngineSound(movementVector);
        }

        private void CheckDoubleClick()
        {
            if (Input.GetButtonDown(m_HorizontalKey))
            {
                Debug.Log(" >> horizontal key pressed " + Input.GetButtonDown(m_HorizontalKey).ToString()+" >> "+ mRigidbody.velocity);
            }
        }

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

