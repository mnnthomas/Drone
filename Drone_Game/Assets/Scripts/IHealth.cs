using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public interface IHealth
    {
        void TakeDamage(float value);
        void OnHealthDepleted();
    }
}

