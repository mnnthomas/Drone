using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
