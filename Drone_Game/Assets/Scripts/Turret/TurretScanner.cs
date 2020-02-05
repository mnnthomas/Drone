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

        private float mPlayerLockdownStartTime;
        private float mScanDelayStartTime;
        private GameObject mPlayerObject;
        private bool mStartBarrage = false;

        private void Update()
        {
            if (!m_Turret.pIsActive || !GameManger.pInstance.IsReady())
                return;

            if (Time.time - mScanDelayStartTime <= m_ScanDelay)
                return;

            if(mPlayerObject && Time.time - mPlayerLockdownStartTime >= m_LockdownTimer && !mStartBarrage)
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
            mScanDelayStartTime = Time.time;
        }

        private void OnTriggerEnter(Collider other)
        {
            //Turret currenly scans to see if the target is within its scan collider to start lockdown timer.
            //The check to see if target is in front can be achived using DotProduct, Currently not doing it to have better gameplay experience 
            if (other.tag == m_LockdownTag)
            {
                mPlayerObject = other.gameObject;
                mPlayerLockdownStartTime = Time.time;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == m_LockdownTag)
            {
                mPlayerObject = null;
                mPlayerLockdownStartTime = default;
            }
        }
    }
}
