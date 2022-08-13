using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Tree", menuName = "Environment/Tree")]
public class Tree : ScriptableObject
{
    public float health = 12;
    public float currentHealth=12;
    public Color color=Color.white;
    public TreeState treeState=TreeState.full;
}
