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

        private void Update()
        {
            if (Input.GetButtonUp(m_PrimaryWeaponKey))
            {
                FireBullets();
            }
            else if (Input.GetButtonDown(m_SecondayWeaponKey))
            {
                ScanTargets();
            }
            else if (Input.GetButtonUp(m_SecondayWeaponKey))
            {
                FireMissiles();
            }
        }

        private void FireBullets()
        {
            m_LeftBarrel.Shoot();
            m_RightBarrel.Shoot();
        }

        private void ScanTargets()
        {

        }

        private void FireMissiles()
        {

        }
    }
}
