using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    /// <summary>
    /// The Drone Shooting controller enables the Drone barrels to shoot based on User Inputs.
    /// This class also handles the scanning of Turrets for the Homing missile targets
    /// </summary>
    public class DroneShootingController : MonoBehaviour
    {
        [Header("Firing axis keys")]
        [SerializeField] private string m_PrimaryWeaponKey = default;
        [SerializeField] private string m_SecondayWeaponKey = default;

        [Header("Barrel references")]
        [SerializeField] private List<BulletBarrel> m_PrimaryBarrels = new List<BulletBarrel>();
        [SerializeField] private List<BulletBarrel> m_MissileBarrels = new List<BulletBarrel>();

        [Header("References for Laser scanning the turrets")]
        [SerializeField] private LineRenderer m_LaserSights = default;
        [SerializeField] private string m_TurretTag = default;
        [SerializeField] private LayerMask m_LayerMask = default;
        [SerializeField] private int m_MaxScannedTargets = default;
        [SerializeField] private float m_MissleTossDelay = default;
        [SerializeField] private int m_MissileScanDelay = default;

        private float mMissleLaunchTime;
        private bool mLaunchingMissile = false;

        private List<GameObject> mCurScannedTurrets = new List<GameObject>();

        private void Update()
        {
            if (!GameManger.pInstance.IsReady())
                return;

            if (Input.GetButtonUp(m_PrimaryWeaponKey))
            {
                FireBullets();
            }
            else if (Input.GetButton(m_SecondayWeaponKey))
            {
                ScanTargets();
            }
            else if (Input.GetButtonUp(m_SecondayWeaponKey))
            {
                if(IsMissileReady())
                    StartCoroutine(FireMissiles());
            }
        }

        private void FireBullets()
        {
            for (int i = 0; i < m_PrimaryBarrels.Count; i++)
                m_PrimaryBarrels[i].Shoot();
        }

        public bool IsMissileReady()
        {
            return (Time.time - mMissleLaunchTime > m_MissileScanDelay) && !mLaunchingMissile;
        }

        private void ScanTargets()
        {
            if (!IsMissileReady())
                return;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_LayerMask))
            {
                m_LaserSights.enabled = true;
                m_LaserSights.SetPosition(0, m_LaserSights.transform.position);
                m_LaserSights.SetPosition(1, hit.point);
                if (hit.collider.tag == m_TurretTag &&  !mCurScannedTurrets.Contains(hit.collider.gameObject) && mCurScannedTurrets.Count < m_MaxScannedTargets)
                    mCurScannedTurrets.Add(hit.collider.gameObject);
            }
        }

        IEnumerator FireMissiles()
        {
            m_LaserSights.enabled = false;

            if (mCurScannedTurrets.Count > 0 && m_MissileBarrels.Count > 0)
            {
                mMissleLaunchTime = Time.time;
                mLaunchingMissile = true;
                for (int i = 0; i < mCurScannedTurrets.Count; i++)
                {
                    m_MissileBarrels[i % 2].Shoot(mCurScannedTurrets[i].transform);
                    yield return new WaitForSeconds(m_MissleTossDelay);
                }
                mCurScannedTurrets.Clear();
                mLaunchingMissile = false;
            }
        }
    }
}
