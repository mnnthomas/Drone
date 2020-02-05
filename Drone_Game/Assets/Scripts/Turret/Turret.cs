using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public class Turret : MonoBehaviour, IHealth
    {
        [SerializeField] private float m_MissileCount = default;
        [SerializeField] private float m_MaxHealth = default;

        [SerializeField] private BulletBarrel m_Barrel = default;
        [SerializeField] private TurretScanner m_Scanner = default;

        [SerializeField] private GameObject m_DestroyEffect = default;

        private Transform mTarget;
        private float mHealth;
        public bool pIsActive { get; private set; }

        private void Start()
        {
            mHealth = m_MaxHealth;
            pIsActive = true;
        }

        public void OnHealthDepleted()
        {
            DestroyTurret();
        }

        public void TakeDamage(float value)
        {
            mHealth -= value;

            if (mHealth <= 0)
                OnHealthDepleted();
        }

        void Update()
        {
            if(mTarget)
            {
                Vector3 targetPos = new Vector3(mTarget.transform.position.x, transform.position.y, mTarget.transform.position.z);
                m_Barrel.transform.LookAt(targetPos);
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
            if (m_DestroyEffect)
                Instantiate(m_DestroyEffect, (transform.position + m_DestroyEffect.transform.position), Quaternion.identity);

            GameManger.pInstance.OnTurretDestroyed();
            pIsActive = false;
            gameObject.SetActive(false);
        }
    }
}