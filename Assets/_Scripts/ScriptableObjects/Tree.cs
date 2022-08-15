using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Tree", menuName = "Environment/Tree")]
public class Tree : ScriptableObject
{
    public event Action<TreeState> OnEventChange;
    public float health = 12;
    public float currentHealth=12;
    public Color color=Color.white;
    public TreeState treeState=TreeState.full;
    public void SetState(TreeState _treeState)
    {
        Debug.Log("SetState inside");
        treeState = _treeState;
        OnEventChange?.Invoke(treeState);
    }
}