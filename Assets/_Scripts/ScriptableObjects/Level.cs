using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LevelSystem
{
    [CreateAssetMenu(fileName = "Level", menuName = "Desing/Level")]
    public class Level : ScriptableObject
    {
        public GroundType groundType;
        [Header("Trade area")]
        public bool wood2coinTrade = true;
        public bool coin2GemTrade = false;
        public bool bladeStore = true;
        public enum GroundType
        {
            defaultGround,
            color,
            sand,
            lava
        }
    }
}
