using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public class DroneMovementController : MonoBehaviour
    {
        [SerializeField] private string m_HorizontalKey = default;
        [SerializeField] private string m_ForwardKey = default;
        [SerializeField] private string m_VerticalKey = default;
        
        [SerializeField] private float m_HorizontalSpeed = default;
        [SerializeField] private float m_ForwardSpeed = default;
        [SerializeField] private float m_VerticalSpeed = default;
        [SerializeField] private float m_TurnSpeed = default;
        [SerializeField] private float m_TiltAngle = default;

        [SerializeField] private Camera m_Camera = default;

        private Rigidbody mRigidbody;

        void Start()
        {
            mRigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            Move();
        }

        private void LateUpdate()
        {
            Turn();

        }

        void Move()
        {
            Vector3 movementVector = new Vector3(Input.GetAxis(m_HorizontalKey) * m_HorizontalSpeed, Input.GetAxis(m_VerticalKey) * m_VerticalSpeed, Input.GetAxis(m_ForwardKey) * m_ForwardSpeed) * Time.deltaTime;
            Quaternion quaternion = Quaternion.Euler(Input.GetAxis(m_ForwardKey) * m_TiltAngle, 0f, -Input.GetAxis(m_HorizontalKey) * m_TiltAngle);

            transform.Translate(movementVector, Space.Self);
            transform.GetChild(0).localRotation = quaternion;
        }

        void Turn()
        {
            Quaternion cameraRot = m_Camera.transform.rotation;
            // Quaternion droneTargetRot = Quaternion.Euler(0, m_Camera.transform.localRotation.y, 0);
            // Debug.Log(" >> " + m_Camera.transform.localRotation.y);
            // transform.rotation = Quaternion.Slerp(transform.rotation, droneTargetRot, 5 * Time.deltaTime);

            transform.forward = Vector3.Slerp(transform.forward, m_Camera.transform.forward, 5 * Time.deltaTime);
            m_Camera.transform.rotation = cameraRot;

        }
    }
}

