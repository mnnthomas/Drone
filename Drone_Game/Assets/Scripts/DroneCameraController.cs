using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCameraController : MonoBehaviour
{
    [SerializeField] private Transform m_DroneTransform;
   // [SerializeField] private Transform m_TargetTransform;
    [SerializeField] private string m_MouseXKey;
    [SerializeField] private string m_MouseYKey;

    [SerializeField] private float m_CameraTurnSpeed;
    [SerializeField] private float m_DroneTurnSpeed;

    private float mMouseX, mMouseY;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        mMouseX += Input.GetAxis(m_MouseXKey) * m_CameraTurnSpeed;
        mMouseY -= Input.GetAxis(m_MouseYKey) * m_CameraTurnSpeed;
        mMouseY = Mathf.Clamp(mMouseY, -35, 60);

        UpdateCamera();
        //UpdateDrone();
    }


    void UpdateCamera()
    {
        //transform.LookAt(m_TargetTransform);
        Quaternion cameraTargetRot = Quaternion.Euler(mMouseY, mMouseX, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraTargetRot, m_CameraTurnSpeed * Time.deltaTime);

    }

    void UpdateDrone()
    {
        Quaternion droneTargetRot = Quaternion.Euler(0, mMouseX, 0);
        m_DroneTransform.rotation = Quaternion.Slerp(m_DroneTransform.rotation, droneTargetRot, m_DroneTurnSpeed * Time.deltaTime);
    }
}
