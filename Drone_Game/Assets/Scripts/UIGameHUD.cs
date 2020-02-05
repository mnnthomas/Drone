using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DroneGame
{
    public class UIGameHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_TurrentCountText = default;
        [SerializeField] private TextMeshProUGUI m_GameText = default;
        [SerializeField] private TextMeshProUGUI m_CountDownText = default;
        [SerializeField] private int m_CountDownTimer = default;

        private int mCountDown;

        public void StartCountDown(System.Action callback)
        {
            StartCoroutine(CountDown(callback));
        }

        IEnumerator CountDown(System.Action callback)
        {
            mCountDown = m_CountDownTimer;
            m_CountDownText.gameObject.SetActive(true);
            while (mCountDown > 0)
            {
                m_CountDownText.text = mCountDown.ToString();
                mCountDown--;
                yield return new WaitForSeconds(1);
            }
            m_CountDownText.gameObject.SetActive(false);
            callback?.Invoke();
        }

        public void UpdateTurrets(int value)
        {
            m_TurrentCountText.text = value.ToString();
        }

        public void ShowGameText(string text)
        {
            m_GameText.gameObject.SetActive(true);
            m_GameText.text = text;
        }
    }
}

