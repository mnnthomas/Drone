using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    /// <summary>
    /// BulletBarrel extends the Ishoot interface
    /// Spawns bullet from the spawn point's forward
    /// </summary>
    public class BulletBarrel : MonoBehaviour, IShoot
    {

        [SerializeField] private Transform m_SpawnPoint = default;
        [SerializeField] private AudioClip m_SpawnClip = default;
        [SerializeField] private GameObject m_Bullet = default;
        private AudioSource mAudioSource;

        void Start()
        {
            mAudioSource = GetComponent<AudioSource>();
        }

        public void Shoot(Transform target = null)
        {
            GameObject bullet;
            bullet = ObjectPoolManager.pInstance.SpawnObject("Bullet", m_SpawnPoint.position, Quaternion.identity, transform);//  Instantiate(m_Bullet, m_SpawnPoint.position, Quaternion.identity);

            if(bullet)
            {
                bullet.GetComponent<Bullet>().InitializeBullet(m_SpawnPoint.forward);

                if (mAudioSource && m_SpawnClip)
                    mAudioSource.PlayOneShot(m_SpawnClip, 1f);
            }
        }
    }
}

