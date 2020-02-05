using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DroneGame
{
    public class GameManger : MonoBehaviour
    {
        [SerializeField] private UIGameHUD m_GameHUD = default;
        [SerializeField] private int m_TotalTurrets = default;
        public static GameManger pInstance { get; private set; }
        private bool mIsReady;
        private int mCurTurretCount;


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
            mCurTurretCount = m_TotalTurrets;
        }

        public void OnGameOver()
        {
            mIsReady = false;
            m_GameHUD.ShowGameText("Game Over!");
        }

        public void OnGameWon()
        {
            mIsReady = false;
            m_GameHUD.ShowGameText("Game Won!");
        }

        public void OnTurretDestroyed()
        {
            mCurTurretCount--;
            m_GameHUD.UpdateTurrets(mCurTurretCount);

            if (mCurTurretCount <= 0)
                OnGameWon();
        }

        public void OnRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnDestroy()
        {
            pInstance = null;
        }
    }
}

