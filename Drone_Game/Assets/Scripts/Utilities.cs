using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneGame
{
    public class Utilities
    {
        /// <summary>
        /// A class to hold min and max float
        /// </summary>
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

        /// <summary>
        /// returns a euler angle above 180 is negative
        /// </summary>
        /// <param name="angle"></param>
        /// <returns>float value of the angle</returns>
        public static float NegativeEuler(float angle)
        {
            float curAngle;
            return (curAngle = (angle > 180) ? angle - 360 : angle);
        }

        /// <summary>
        /// A rangemap to find the mapped value between two floats dynamically.
        /// </summary>
        [System.Serializable]
        public struct RangeMap
        {
            [System.Serializable]
            public struct Map
            {
                public float _Value;
                public float _MappedValue;
            }

            public Map _Min;
            public Map _Max;

            public float GetMappedValue(float num)
            {
                return ((num - _Min._Value) / (_Max._Value - _Min._Value) * (_Max._MappedValue - _Min._MappedValue)) + _Min._MappedValue;
            }
        }
    }
}
