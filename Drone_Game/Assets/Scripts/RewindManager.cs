using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the rewind inputs and cooldown
/// Rewindable Objects like BulletRewind, HealthRewind and TurretRewind register to the RewindManager's actions to start and stop rewind
/// </summary>
public class RewindManager : MonoBehaviour
{
    [SerializeField] private float m_RewindDuration = default;
    [SerializeField] private float m_RewindCooldown = default;
    [SerializeField] private string m_RewindKey = default;

    public static RewindManager pInstance { get; private set; }
    public float pRewindDuration { get => m_RewindDuration; }

    public System.Action OnStartRewind;
    public System.Action OnStopRewind;

    private float mRewindCooldownStart;

    private void Start()
    {
        if (pInstance == null)
            pInstance = this;
    }

    void Update()
    {
        if (Input.GetButtonDown(m_RewindKey) && IsRewindReady())
            OnStartRewind?.Invoke();
        else if (Input.GetButtonUp(m_RewindKey))
        {
            mRewindCooldownStart = Time.time;
            OnStopRewind?.Invoke();
        }
    }

    public bool IsRewindReady()
    {
        return (mRewindCooldownStart == 0 || Time.time - mRewindCooldownStart >= m_RewindCooldown);
    }

    private void OnDestroy()
    {
        pInstance = null;
    }
}
