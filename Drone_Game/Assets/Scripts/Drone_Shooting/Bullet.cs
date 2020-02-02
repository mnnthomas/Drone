using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public class Bullet : BulletBase
    {
        public override void InitializeBullet(Vector3 forward, Transform target = null)
        {
            mCurDirection = forward;
            mInitialized = true;
            mRigidbody.velocity = mCurDirection * m_Speed;
            transform.LookAt(mCurDirection);
        }

    }
}
