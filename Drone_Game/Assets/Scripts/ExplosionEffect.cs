using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private float m_AliveTimer = default;

    private void Start()
    {
        Invoke("DestroyThis", m_AliveTimer);
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
