using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    /// <summary>
    /// BulletData is a ScriptableObject that hold the data needed for any bullet.
    /// Since there will be a lot of bullets in scene at a time, The data could be held in once place and referenced throughout.
    /// </summary>
    [CreateAssetMenu(menuName = "DroneGame/BulletData")]
    public class BulletData : ScriptableObject
    {
        [SerializeField] private float m_Damage = default;
        [SerializeField] private float m_Speed = default;
        [SerializeField] private float m_TurnSpeed = default;
        [SerializeField] private float m_DurationAlive = default;

        public float Damage { get => m_Damage; }
        public float Speed { get => m_Speed; }
        public float TurnSpeed { get => m_TurnSpeed; }
        public float DurationAlive { get => m_DurationAlive; }
    }
}
