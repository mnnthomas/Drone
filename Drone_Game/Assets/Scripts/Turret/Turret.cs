using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public class Turret : MonoBehaviour, IHealth
    {
        [SerializeField] private float m_MissileCount = default;
        [SerializeField] private float m_Health = default;
        [SerializeField] private BulletBarrel m_Barrel = default;
        [SerializeField] private TurretScanner m_Scanner = default;

        private Transform mTarget;

        public void OnHealthDepleted()
        {
            DestroyTurret();
        }

        public void TakeDamage(float value)
        {
            m_Health -= value;
            if (m_Health <= 0)
                OnHealthDepleted();
        }

        void Update()
        {
            if(mTarget)
            {
                //Quaternion lookRot = Quaternion.LookRotation(mTarget.position - transform.position);
                // transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, 5f);
                transform.LookAt(mTarget);
            }
        }

        public void StartBarrage(Transform target)
        {
            mTarget = target;
            StartCoroutine(FireBarriage());
        }

        IEnumerator FireBarriage()
        {
            for (int i = 0; i < m_MissileCount; i++)
            {
                m_Barrel.Shoot(mTarget);
                yield return new WaitForSeconds(0.5f);
            }
            m_Scanner.ResetScanner();
            mTarget = null;
        }

        public void DestroyTurret()
        {

        }

    }
}