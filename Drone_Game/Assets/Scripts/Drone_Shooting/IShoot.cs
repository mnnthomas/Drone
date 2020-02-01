using UnityEngine;

/// <summary>
/// Drone's different shooting mechanisms extend from Ishoot
/// Eg - BulletBarrel, MissleBarrel
/// </summary>
namespace DroneGame
{
    public interface IShoot
    {
        void Shoot(Transform target = null);
    }
}

