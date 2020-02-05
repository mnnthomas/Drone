using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public class BulletTransform
    {
        public Vector3 _Position;
        public Quaternion _Rotation;

        public BulletTransform()
        {

        }

        public BulletTransform(Vector3 pos, Quaternion rot)
        {
            _Position = pos;
            _Rotation = rot;
        }
    }

    public class BulletRewind : BaseRewind
    {
        [SerializeField] private BulletBase m_BulletBase = default;
        private List<BulletTransform> mBulletTransformList = new List<BulletTransform>();

        protected override void Rewind()
        {
            if (mBulletTransformList.Count > 0)
            {
                m_BulletBase.transform.position = mBulletTransformList[0]._Position;
                m_BulletBase.transform.rotation = mBulletTransformList[0]._Rotation;

                mBulletTransformList.RemoveAt(0);
            }
            else
                StopRewind();
        }

        protected override void StartRewind()
        {
            base.StartRewind();
            m_BulletBase.SetKinetic(true);
        }

        protected override void StopRewind()
        {
            base.StopRewind();
            m_BulletBase.SetKinetic(false);
        }

        protected override void Record()
        {
            if (mBulletTransformList.Count > Mathf.Round(RewindManager.pInstance.pRewindDuration / Time.fixedDeltaTime))
                mBulletTransformList.RemoveAt(mBulletTransformList.Count - 1);

            mBulletTransformList.Insert(0, new BulletTransform(m_BulletBase.transform.localPosition, m_BulletBase.transform.localRotation));
        }
    }
}

