using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    /// <summary>
    /// Turret class - Handles the turrent activation, firing and destroy
    /// Extends IHealth to handle health updates on bullet hit
    /// </summary>
    public class Turret : MonoBehaviour, IHealth
    {
        [SerializeField] private float m_MissileCount = default;
        [SerializeField] private float m_MaxHealth = default;

        [SerializeField] private BulletBarrel m_Barrel = default;
        [SerializeField] private TurretScanner m_Scanner = default;
        [SerializeField] private List<Renderer> m_TurretRenderes = default;
        [SerializeField] private List<BoxCollider> m_TurretColliders = default;
        [SerializeField] private GameObject m_DestroyEffect = default;

        private Transform mTarget;
        public float pHealth { get; set; }
        public bool pIsActive { get; private set; }

        private void Start()
        {
            pHealth = m_MaxHealth;
            pIsActive = true;
        }

        public void OnHealthDepleted()
        {
            DestroyTurret();
        }

        public void TakeDamage(float value)
        {
            pHealth -= value;

            if (pHealth <= 0)
                OnHealthDepleted();
        }

        void Update()
        {
            if(mTarget && pIsActive)
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
                if (pIsActive)
                    m_Barrel.Shoot(mTarget);

                yield return new WaitForSeconds(0.5f);
            }
            m_Scanner.ResetScanner();
            mTarget = null;
        }

        public void OnTurretRewinded()
        {
            GameManger.pInstance.UpdateTurretCount(1);
            pIsActive = true;
            EnableTurret(true);
        }

        public void DestroyTurret()
        {
            if (m_DestroyEffect)
                Instantiate(m_DestroyEffect, (transform.position + m_DestroyEffect.transform.position), Quaternion.identity);

            GameManger.pInstance.UpdateTurretCount(-1);
            pIsActive = false;
            EnableTurret(false);
        }

        void EnableTurret(bool value)
        {
            for (int i = 0; i < m_TurretRenderes.Count; i++)
                m_TurretRenderes[i].enabled = value;

            for (int i = 0; i < m_TurretColliders.Count; i++)
                m_TurretColliders[i].enabled = value;
        }
    }
}