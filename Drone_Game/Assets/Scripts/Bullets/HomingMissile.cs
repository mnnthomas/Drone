﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DroneGame
{
    /// <summary>
    /// Inherits from BulletBase, Handle specific cases for homing missile functionality
    /// </summary>
    public class HomingMissile : BulletBase
    {
        [SerializeField] private float m_MissileLaunchForce = default; // The force at which the missle is thrusted upwards
        [SerializeField] private float m_MissileSeekStartDelay = default; // Iniitial delay before the homing missile starts seeking
        [SerializeField] private AudioSource m_AudioSource = default;
        [SerializeField] private AudioClip m_MissileSeekClip = default;
        [SerializeField] private AudioClip m_MissileFollowClip = default;

        private float mMissleLaunchTime;
        private bool mIsSeeking;

        public override void InitializeBullet(Vector3 forward, Transform target)
        {
            mCurTarget = target;
            mRigidbody.AddForce(m_MissileLaunchForce * forward, ForceMode.Force);
            transform.LookAt(Vector3.down);
            if (m_AudioSource && m_MissileSeekClip)
            {
                m_AudioSource.clip = m_MissileSeekClip;
                m_AudioSource.Play();
            }

            mMissleLaunchTime = Time.time;
        }

        private void Update()
        {
            if (mRigidbody.isKinematic)
                return;

            if(Time.time - mMissleLaunchTime >= m_MissileSeekStartDelay && !mIsSeeking)
            {
                mIsSeeking = true;
                m_AudioSource.Stop();
            }

            SeekTarget();
        }


        private void SeekTarget()
        {
            if (mIsSeeking && mCurTarget)
            {
                if (m_AudioSource && m_MissileFollowClip && m_AudioSource.isPlaying != m_MissileFollowClip)
                {
                    m_AudioSource.clip = m_MissileFollowClip;
                    m_AudioSource.Play();
                }

                mRigidbody.velocity = transform.forward * m_BulletData.Speed;
                Quaternion lookRot = Quaternion.LookRotation(mCurTarget.position - transform.position);
                mRigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, lookRot, m_BulletData.TurnSpeed));
            }
            else if(mIsSeeking && !mCurTarget)
            {
                DestroyBullet();
            }
        }

        private void ResetMissileData()
        {
            mIsSeeking = false;
            mMissleLaunchTime = default;
            mCurTarget = null;
            mRigidbody.Sleep();
        }

        protected override void DestroyBullet()
        {
            ResetMissileData();
            base.DestroyBullet();
        }
    }

}

