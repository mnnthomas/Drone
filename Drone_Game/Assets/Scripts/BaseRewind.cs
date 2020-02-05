using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public abstract class BaseRewind : MonoBehaviour
    {
        public bool pIsRewinding { get; private set; }
        protected abstract void Rewind();
        protected abstract void Record();

        protected virtual void Start()
        {
            RewindManager.pInstance.OnStartRewind += StartRewind;
            RewindManager.pInstance.OnStopRewind += StopRewind;
        }

        protected virtual void StartRewind()
        {
            pIsRewinding = true;
        }

        protected virtual void StopRewind()
        {
            pIsRewinding = false;
        }

        void FixedUpdate()
        {
            if (pIsRewinding)
                Rewind();
            else
                Record();
        }

        protected virtual void OnDestroy()
        {
            RewindManager.pInstance.OnStartRewind -= StartRewind;
            RewindManager.pInstance.OnStopRewind -= StopRewind;
        }
    }
}

