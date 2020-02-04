using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public class DroneHealthController : MonoBehaviour, IHealth
    {
        [SerializeField] private float m_MaxHealth = default;

        public float pCurHealth { get; private set; }

        private void Start()
        {
            pCurHealth = m_MaxHealth;
        }

        public void OnHealthDepleted()
        {
           
        }

        public void TakeDamage(float value)
        {
            pCurHealth -= value;
            OnHealthDepleted();

            if (pCurHealth <= 0)
                GameManger.pInstance.OnGameOver();
        }
    }
}

