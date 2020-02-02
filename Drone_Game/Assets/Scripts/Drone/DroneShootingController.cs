using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public class DroneShootingController : MonoBehaviour
    {
        [Header("Firing axis keys")]
        [SerializeField] private string m_PrimaryWeaponKey = default;
        [SerializeField] private string m_SecondayWeaponKey = default;

        [SerializeField] private BulletBarrel m_LeftBarrel = default;
        [SerializeField] private BulletBarrel m_RightBarrel = default;
        [SerializeField] private List<MissileBarrel> m_MissileBarrels = new List<MissileBarrel>();

        [SerializeField] private LineRenderer m_LaserSights = default;
        [SerializeField] private string m_TurretTag = default;
        [SerializeField] private int m_MaxScannedTargets = default;
        [SerializeField] private int m_ScanDelay = default;

        private List<GameObject> mCurScannedTurrets = new List<GameObject>();

        private void Update()
        {
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
                StartCoroutine(FireMissiles());
            }
        }

        private void FireBullets()
        {
            m_LeftBarrel.Shoot();
            m_RightBarrel.Shoot();
        }

        private void ScanTargets()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
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
                for(int i = 0; i < mCurScannedTurrets.Count; i++)
                {
                    m_MissileBarrels[Random.Range(0, m_MissileBarrels.Count)].Shoot(mCurScannedTurrets[i].transform);
                    yield return new WaitForSeconds(1);
                }
                mCurScannedTurrets.Clear();
            }
        }
    }
}
