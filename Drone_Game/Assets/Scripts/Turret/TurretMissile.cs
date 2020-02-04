using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public class TurretMissile : BulletBase
    {
        public override void InitializeBullet(Vector3 forward, Transform target)
        {
            mCurDirection = forward;
            mRigidbody.velocity = mCurDirection * m_BulletData.Speed;
            transform.LookAt(mCurDirection);
        }
    }
}
