using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeUnit : MonoBehaviour
{
    [SerializeField] Tree tree;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Blade") { 
            BladeAction.Instance.DoDamage(tree);
            CheckState();
        }
    }
    void CheckState()
    {
        switch (tree.treeState)
        {
            case TreeState.full:
                break;
            case TreeState.leafless:
                break;
            case TreeState.chopped:
                break;
            case TreeState.destroyed:
                break;
            default:
                break;
        }
    }
}
public enum TreeState
{
    full,
    leafless,
    chopped,
    destroyed
}
