﻿using System.Collections;
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
            GameManger.pInstance.OnGameOver();
        }

        public void SetCurrentHealth(float value)
        {
            pCurHealth = value;
        }

        public void TakeDamage(float value)
        {
            pCurHealth -= value;

            if (pCurHealth <= 0)
                OnHealthDepleted();
        }
    }
}

