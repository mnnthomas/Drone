using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public abstract class BulletBase : MonoBehaviour
    {
        [SerializeField] protected float m_Damage = default;
        [SerializeField] protected float m_Speed = default;
        [SerializeField] protected float m_DurationAlive = default;
        [SerializeField] protected GameObject m_ExplosionParticle = default;

        protected Transform mCurTarget;
        protected Vector3 mCurDirection;
        protected bool mInitialized;

        protected Rigidbody mRigidbody;

        private void OnEnable()
        {
            mRigidbody = GetComponent<Rigidbody>();
            Invoke("DestroyBullet", m_DurationAlive);
        }

        public abstract void InitializeBullet(Vector3 forward, Transform target = null);

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
                return;

            OnCollided(other.gameObject);
        }

        protected virtual void OnCollided(GameObject obj)
        {
            if (m_ExplosionParticle)
                Instantiate(m_ExplosionParticle, transform.position, Quaternion.identity);

            DestroyBullet();
        }

        protected virtual void DestroyBullet()
        {
            ObjectPoolManager.pInstance.AddBackToPool(gameObject);
        }

    }

}
