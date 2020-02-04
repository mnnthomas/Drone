using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    /// <summary>
    /// An Abstarct base class for Bullets. 
    /// This class handles Instantiating, Collision and destroying of all types of bullet
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BulletBase : MonoBehaviour
    {
        [SerializeField] protected BulletData m_BulletData = default;
        [SerializeField] protected List<string> m_IgnoreTags = default;
        [SerializeField] protected GameObject m_ExplosionParticle = default;

        protected Transform mCurTarget;
        protected Vector3 mCurDirection;
        protected bool mInitialized;

        protected Rigidbody mRigidbody;

        private void OnEnable()
        {
            mRigidbody = GetComponent<Rigidbody>();
            Invoke("DestroyBullet", m_BulletData.DurationAlive);
        }

        public abstract void InitializeBullet(Vector3 forward, Transform target = null);

        private void OnTriggerEnter(Collider other)
        {
            //TODO: Need to do a layermask check instead of tag
            if (m_IgnoreTags.Contains(other.gameObject.tag))
                return;

            OnCollided(other.gameObject);
        }

        protected virtual void OnCollided(GameObject obj)
        {
            if (m_ExplosionParticle)
                Instantiate(m_ExplosionParticle, transform.position, Quaternion.identity);

            if (obj.GetComponent<IHealth>() != null)
                obj.GetComponent<IHealth>().TakeDamage(m_BulletData.Damage);

            DestroyBullet();
        }

        protected virtual void DestroyBullet()
        {
            ObjectPoolManager.pInstance.AddBackToPool(gameObject);
        }

    }

}
