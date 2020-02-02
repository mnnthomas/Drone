using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    /// <summary>
    /// MissileBarrel extends the Ishoot interface
    /// Spawns missile from the spawn point's forward
    /// </summary>
    public class MissileBarrel : MonoBehaviour, IShoot
    {
        [SerializeField] private Transform m_SpawnPoint = default;
        [SerializeField] private AudioClip m_SpawnClip = default;
        [SerializeField] private string m_MissileName = default;
        private AudioSource mAudioSource;

        void Start()
        {
            mAudioSource = GetComponent<AudioSource>();
        }

        public void Shoot(Transform target = null)
        {
            GameObject missile;
            missile = ObjectPoolManager.pInstance.SpawnObject(m_MissileName, m_SpawnPoint.position, Quaternion.identity);
            if (missile)
            {
                missile.GetComponent<HomingMissile>().InitializeBullet(m_SpawnPoint.forward, target);

                if (mAudioSource && m_SpawnClip)
                    mAudioSource.PlayOneShot(m_SpawnClip, 1f);
            }
        }
    }
}

