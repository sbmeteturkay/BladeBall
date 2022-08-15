using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Tree", menuName = "Environment/Tree")]
public class Tree : ScriptableObject
{
    public event Action<TreeState> OnEventChange;
    [Header("In Game Values")]
    public float health = 12;
    public float currentHealth=12;
    public Color color=Color.white;
    public TreeState treeState=TreeState.full;
    [Header("Given Gold Amount")]
    public int givenGold = 1;
    public void SetState(TreeState _treeState)
    {
        Debug.Log("SetState inside");
        treeState = _treeState;
        OnEventChange?.Invoke(treeState);
    }
}
