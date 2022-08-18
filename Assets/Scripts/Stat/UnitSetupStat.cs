using Win.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace Win.Attribute
{
    public class UnitSetupStat : MonoBehaviour
    {
        [Serializable]
        public struct KeyStatPair
        {
            [ReadOnly()] public UnitStatKey Key;
            [ReadOnly()] public float Value;
        }

        public KeyStatPair[] Stats;
        private Dictionary<UnitStatKey, float> m_StatDict = new Dictionary<UnitStatKey, float>();

        public float GetValue(UnitStatKey key)
        {
            return Stats.Single(data => data.Key == key).Value;
        }
    }
}
