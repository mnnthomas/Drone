using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public class Utilities : MonoBehaviour
    {
        [System.Serializable]
        public class MinMax
        {
            public float Min;
            public float Max;

            public MinMax()
            {
            }

            public MinMax(float inMin, float inMax)
            {
                Max = inMax;
                Min = inMin;
            }

            public bool IsInRange(float inValue)
            {
                if (inValue < Min || inValue > Max)
                    return false;
                return true;
            }
        }
    }
}
