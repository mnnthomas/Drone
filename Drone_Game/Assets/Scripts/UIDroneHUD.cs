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

        [SerializeField] private TextMeshProUGUI m_PitchText = default;
        [SerializeField] private TextMeshProUGUI m_RollText = default;
        [SerializeField] private TextMeshProUGUI m_AltitudeText = default;
        [SerializeField] private TextMeshProUGUI m_CameraTiltText = default;
        [SerializeField] private Image m_AngleBar = default;
        [SerializeField] private Utilities.RangeMap m_AngleBarMap = default;

        // Update is called once per frame
        void LateUpdate()
        {
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
        }
    }
}
