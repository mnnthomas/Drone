using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DroneGame
{
    public class HomingMissile : BulletBase
    {
        [SerializeField] private float m_MissileLaunchForce = default;
        [SerializeField] private float m_MissileSeekStartDelay = default;
        private float mMissleLaunchTime;
        private bool mIsSeeking;

        public override void InitializeBullet(Vector3 forward, Transform target)
        {
            mCurTarget = target;
            mRigidbody.AddForce(m_MissileLaunchForce * forward, ForceMode.Force);
            mMissleLaunchTime = Time.time;
        }

        private void Update()
        {
            if(Time.time - mMissleLaunchTime >= m_MissileSeekStartDelay && !mIsSeeking)
            {
                mIsSeeking = true;
            }

            SeekTarget();
        }


        private void SeekTarget()
        {
            if(mIsSeeking && mCurTarget)
            {
                mRigidbody.velocity = transform.forward * m_Speed;
                Quaternion lookRot = Quaternion.LookRotation(mCurTarget.position - transform.position);
                mRigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, lookRot, m_TurnSpeed));
            }
            else if(mIsSeeking && !mCurTarget)
            {
                DestroyBullet();
            }
        }

        private void ResetMissileData()
        {
            mIsSeeking = false;
            mMissleLaunchTime = default;
            mCurTarget = null;
            mRigidbody.Sleep();
        }

        protected override void DestroyBullet()
        {
            ResetMissileData();
            base.DestroyBullet();
        }
    }

}

