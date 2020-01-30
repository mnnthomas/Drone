using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public class DroneInputController : MonoBehaviour
    {
        [SerializeField] private string m_HorizontalKey = default;
        [SerializeField] private string m_ForwardKey = default;
        [SerializeField] private string m_VerticalKey = default;
        
        [SerializeField] private float m_MoveSpeed = default;
        [SerializeField] private float m_TurnSpeed = default;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

