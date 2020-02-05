using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    [RequireComponent(typeof(DroneHealthController))]
    public class DroneHealthRewind : BaseRewind
    {
        private DroneHealthController mDroneHealthController = default;
        private List<float> mHealthList = new List<float>();

        protected override void Start()
        {
            base.Start();
            mDroneHealthController = GetComponent<DroneHealthController>();
        }

        protected override void Rewind()
        {
            if (mHealthList.Count > 0)
            {
                mDroneHealthController.SetCurrentHealth(mHealthList[0]);
                mHealthList.RemoveAt(0);
            }
            else
                StopRewind();
        }

        protected override void Record()
        {
            if (mHealthList.Count > Mathf.Round(RewindManager.pInstance.pRewindDuration / Time.fixedDeltaTime))
                mHealthList.RemoveAt(mHealthList.Count - 1);

            mHealthList.Insert(0, mDroneHealthController.pCurHealth);
        }
    }
}
