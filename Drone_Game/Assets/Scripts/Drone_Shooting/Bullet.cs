using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    /// <summary>
    /// Inherits from BulletBase, Handle specific cases for primary bullet functionality
    /// </summary>
    public class Bullet : BulletBase
    {
        [SerializeField] private bool m_TargetMousePointer = default; //Decides whether to shoot bullets in the forward direction of the Barrel / Mouse pointer direction
        [SerializeField] private string m_MousePointerIgnoreLayer = default;

        private LayerMask mIgnoreLayer;

        public override void InitializeBullet(Vector3 forward, Transform target = null)
        {
            mCurDirection = forward;
            mIgnoreLayer = ~(1 << LayerMask.NameToLayer(m_MousePointerIgnoreLayer));
            mInitialized = true;
            if (m_TargetMousePointer)
            {
                RaycastHit hit;
                Vector3 dir = default;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mIgnoreLayer))
                {
                    mCurDirection = (hit.point - transform.position);
                    mCurDirection.Normalize();
                }
            }

            mRigidbody.velocity = mCurDirection * m_BulletData.Speed;
            transform.LookAt(mCurDirection);
        }
    }
}
