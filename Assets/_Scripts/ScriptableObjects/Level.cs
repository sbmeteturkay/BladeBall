using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LevelSystem
{
    [CreateAssetMenu(fileName = "Level", menuName = "Desing/Level")]
    public class Level : ScriptableObject
    {
        [Header("Level Area Desing")]
        public GroundType groundType;
        public Color groundColor=Color.white;
        public TreeModel treeModel=TreeModel.defaulttTree;
        public Color firstTreeColor = Color.white;
        public Color secondTreeColor = Color.white;
        public bool randomColor = false;
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
        public enum TreeModel
        {
            defaulttTree
        }
    }
}
