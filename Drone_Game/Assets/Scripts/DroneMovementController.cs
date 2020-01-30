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
        [SerializeField] private float m_TurnAngle = default;
        [SerializeField] private float m_TiltAngle = default;

        private Rigidbody mRigidbody;

        void Start()
        {
            mRigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            Vector3 movementVector = new Vector3(Input.GetAxis(m_HorizontalKey) * m_HorizontalSpeed, Input.GetAxis(m_VerticalKey) * m_VerticalSpeed, Input.GetAxis(m_ForwardKey) * m_ForwardSpeed) * Time.deltaTime;
            Quaternion quaternion = Quaternion.Euler(Input.GetAxis(m_ForwardKey) * m_TiltAngle, 0f, -Input.GetAxis(m_HorizontalKey) * m_TiltAngle);

            mRigidbody.MovePosition(transform.position + movementVector);
            mRigidbody.MoveRotation(quaternion);
        }
    }
}

