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

        [Header("Experiemental feature")]
        [SerializeField] private bool m_AllowTilting = default;
        [SerializeField] private float m_TiltAngle = default;

        [Header("Object references")]
        [SerializeField] private Camera m_Camera = default;
        [SerializeField] private GameObject m_DroneMesh = default;


        private string mPreviousClickedKey;
        private string mClickTime;

        void Update()
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
        void Move()
        {
            Vector3 movementVector = new Vector3(Input.GetAxis(m_HorizontalKey) * m_HorizontalSpeed, Input.GetAxis(m_VerticalKey) * m_VerticalSpeed, Input.GetAxis(m_ForwardKey) * m_ForwardSpeed) * Time.deltaTime;
            transform.Translate(movementVector, Space.Self);
        }

        void CheckDoubleClick()
        {
            if(Input.anyKeyDown)
            {
                //need to code
            }
        }

        /// <summary>
        /// Turns the drone towards camera's forward. 
        /// Added an experimental drone tilt feature.
        /// </summary>
        void Turn()
        {
            Quaternion cameraRot = m_Camera.transform.rotation;

            if(m_AllowTilting)
            {
                Quaternion quaternion = Quaternion.Euler(Input.GetAxis(m_ForwardKey) * m_TiltAngle, 0f, -Input.GetAxis(m_HorizontalKey) * m_TiltAngle);
                m_DroneMesh.transform.localRotation = quaternion;
            }

            Vector3 velocity = Vector3.zero;
            transform.forward = Vector3.SmoothDamp(transform.forward, m_Camera.transform.forward, ref velocity, m_TurnSpeed* Time.deltaTime);
            m_Camera.transform.rotation = cameraRot;
        }
    }
}

