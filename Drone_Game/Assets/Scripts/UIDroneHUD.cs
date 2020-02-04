using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DroneGame
{
    public class UIDroneHUD : MonoBehaviour
    {
        [SerializeField] private Transform m_DroneTransform = default;
        [SerializeField] private Transform m_DroneMeshTransform = default;
        [SerializeField] private DroneShootingController m_DroneShootingController = default;
        [SerializeField] private DroneHealthController m_DroneHealthController = default;

        [SerializeField] private Slider m_HealthSlider = default;
        [SerializeField] private TextMeshProUGUI m_PitchText = default;
        [SerializeField] private TextMeshProUGUI m_RollText = default;
        [SerializeField] private TextMeshProUGUI m_AltitudeText = default;
        [SerializeField] private TextMeshProUGUI m_CameraTiltText = default;
        [SerializeField] private Image m_MissileReady = default;
        [SerializeField] private Image m_AngleBar = default;
        [SerializeField] private Utilities.RangeMap m_AngleBarMap = default;

        // Update is called once per frame
        void LateUpdate()
        {
            if (!GameManger.pInstance.IsReady())
                return;

            UpdateUI();
        }

        private void UpdateUI()
        {
            if(m_DroneMeshTransform && m_PitchText && m_RollText)
            {
                m_PitchText.text = Utilities.NegativeEuler(m_DroneMeshTransform.localEulerAngles.x).ToString("F");
                m_RollText.text = Utilities.NegativeEuler(m_DroneMeshTransform.localEulerAngles.z).ToString("F");
            }
            if (m_DroneTransform && m_AltitudeText && m_CameraTiltText)
            {
                m_CameraTiltText.text = Utilities.NegativeEuler(m_DroneTransform.localEulerAngles.x).ToString("F");
                m_AltitudeText.text = m_DroneTransform.position.y.ToString("F");
            }
            if (m_AngleBar)
                m_AngleBar.rectTransform.anchoredPosition = new Vector2(0, m_AngleBarMap.GetMappedValue(Utilities.NegativeEuler(m_DroneTransform.localEulerAngles.x)));

            if (m_MissileReady && m_DroneShootingController)
                m_MissileReady.enabled = m_DroneShootingController.IsMissileReady();

            if (m_HealthSlider && m_DroneHealthController)
                m_HealthSlider.value = m_DroneHealthController.pCurHealth;

        }
    }
}
