using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public class GameManger : MonoBehaviour
    {
        [SerializeField] private UIGameHUD m_GameHUD = default;
        public static GameManger pInstance { get; private set; }
        private bool mIsReady;

        private void Awake()
        {
            if (pInstance == null)
                pInstance = this;
        }

        public bool IsReady()
        {
            return mIsReady;
        }

        private void Start()
        {
            StartCoroutine(CheckReady());
        }

        IEnumerator CheckReady()
        {
            while (!mIsReady && LevelCreator.pInstance.IsReady() && ObjectPoolManager.pInstance.IsReady())
                yield return null;

            m_GameHUD.StartCountDown(OnGameReady);
        }

        public void OnGameReady()
        {
            mIsReady = true;
        }

        public void OnGameOver()
        {
            mIsReady = false;
        }
       

        private void OnDestroy()
        {
            pInstance = null;
        }
    }
}

