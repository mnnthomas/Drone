using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public class DroneHealthRewind : BaseRewind
    {
        [SerializeField] private DroneHealthController m_DroneHealthController = default;
        private List<float> mHealthList = new List<float>();

        protected override void Rewind()
        {
            if (mHealthList.Count > 0)
            {
                m_DroneHealthController.SetCurrentHealth(mHealthList[0]);
                mHealthList.RemoveAt(0);
            }
            else
                StopRewind();
        }

        protected override void Record()
        {
            if (mHealthList.Count > Mathf.Round(RewindManager.pInstance.pRewindDuration / Time.fixedDeltaTime))
                mHealthList.RemoveAt(mHealthList.Count - 1);

            mHealthList.Insert(0, m_DroneHealthController.pCurHealth);
        }
    }
}
