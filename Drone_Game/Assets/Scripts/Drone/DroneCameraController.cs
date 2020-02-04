using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    /// <summary>
    /// DroneCameraController takes care of the camera rotation based on Mouse X and Y axis
    /// </summary>
    public class DroneCameraController : MonoBehaviour
    {
        [Header ("Mouse axis keys")]
        [SerializeField] private string m_MouseXKey = default;
        [SerializeField] private string m_MouseYKey = default;

        [Header("Camera values")]
        [SerializeField] private float m_CameraTurnSpeed = default;
        [SerializeField] private Utilities.MinMax m_CameraPitchClamp = default;

        private float mMouseX, mMouseY;

        void LateUpdate()
        {
            mMouseX += Input.GetAxis(m_MouseXKey) * m_CameraTurnSpeed;
            mMouseY -= Input.GetAxis(m_MouseYKey) * m_CameraTurnSpeed;
            mMouseY = Mathf.Clamp(mMouseY, m_CameraPitchClamp.Min, m_CameraPitchClamp.Max);

            UpdateCamera();
        }

        void UpdateCamera()
        {
            if (!GameManger.pInstance.IsReady())
                return;

            Quaternion cameraTargetRot = Quaternion.Euler(mMouseY, mMouseX, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, cameraTargetRot, m_CameraTurnSpeed * Time.deltaTime);
        }
    }
}
