using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public class TurretData
    {
        public float _Health;
        public bool _IsAlive;

        public TurretData()
        {

        }

        public TurretData(float health, bool value)
        {
            _Health = health;
            _IsAlive = value;
        }
    }

    [RequireComponent(typeof(Turret))]
    public class TurretRewind : BaseRewind
    {
        private List<TurretData> mTurretDataList = new List<TurretData>();
        private Turret mTurret;

        protected override void Start()
        {
            base.Start();
            mTurret = GetComponent<Turret>();
        }

        protected override void Record()
        {
            if (mTurretDataList.Count > Mathf.Round(RewindManager.pInstance.pRewindDuration / Time.fixedDeltaTime))
                mTurretDataList.RemoveAt(mTurretDataList.Count - 1);

            mTurretDataList.Insert(0 , new TurretData(mTurret.pHealth, mTurret.pIsActive));
        }

        protected override void Rewind()
        {
            if (mTurretDataList.Count > 0)
            {
                mTurret.pHealth = mTurretDataList[0]._Health;
                if (mTurretDataList.Count >= 2 && mTurretDataList[0]._IsAlive == false && mTurretDataList[1]._IsAlive == true)
                {
                    mTurret.OnTurretRewinded();
                }

                mTurretDataList.RemoveAt(0);
            }
            else
                StopRewind();
        }
    }
}

