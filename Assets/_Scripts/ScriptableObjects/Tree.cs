using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Tree", menuName = "Environment/Tree")]
public class Tree : ScriptableObject
{
    [Header("In Game Values")]
    public float health = 12;
    public float currentHealth=12;
    public TreeState treeState=TreeState.full;
    [Header("Given Gold Amount")]
    public int givenGold = 1;
    public void SetState(TreeState _treeState)
    {
        treeState = _treeState;
    }
}
