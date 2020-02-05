using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public class TurretMissile : BulletBase
    {
        public override void InitializeBullet(Vector3 forward, Transform target)
        {
            mCurTarget = target;
            mCurDirection = (target.position - transform.position);
            mCurDirection.Normalize();

            transform.LookAt(target.transform);
            mRigidbody.velocity = mCurDirection * m_BulletData.Speed;

        }
    }
}
