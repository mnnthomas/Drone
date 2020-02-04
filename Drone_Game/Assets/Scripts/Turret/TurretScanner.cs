using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public class TurretScanner : MonoBehaviour
    {
        [SerializeField] private float m_LockdownTimer = default;
        [SerializeField] private float m_ScanDelay = default;
        [SerializeField] private string m_LockdownTag = default;
        [SerializeField] private Turret m_Turret = default;

        private float mPlayerScanStartTime;
        private float mPlayerScanDelayStartTime;
        private GameObject mPlayerObject;
        private bool mStartBarrage = false;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == m_LockdownTag)
            {
                mPlayerObject = other.gameObject;
                mPlayerScanStartTime = Time.time;
            }
        }

        private void Update()
        {
            if (Time.time - mPlayerScanDelayStartTime <= m_ScanDelay)
                return;

            if(mPlayerObject && Time.time - mPlayerScanStartTime >= m_LockdownTimer && !mStartBarrage)
            {
                mStartBarrage = true;
                OnPlayerScanned();
            }
        }

        private void OnPlayerScanned()
        {
            Debug.Log(" >>> Player Scanned !");
            m_Turret.StartBarrage(mPlayerObject.transform);
        }

        public void ResetScanner()
        {
            mStartBarrage = false;
            mPlayerScanDelayStartTime = Time.time;
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.tag == m_LockdownTag)
            {
                mPlayerObject = null;
                mPlayerScanStartTime = default;
            }
        }
    }
}
