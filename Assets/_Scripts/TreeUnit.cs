using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeUnit : MonoBehaviour
{
    [SerializeField] Tree tree;
    [SerializeField] Transform TopLeafs;
    [SerializeField] Transform BotLeafs;
    [SerializeField] Transform Wood;
    [SerializeField] GameObject StaticTree;
    private void Start()
    {
        tree.currentHealth = tree.health;
        tree.treeState = TreeState.full;
    }
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
                OpenChildRigidbodys(TopLeafs);
                break;
            case TreeState.chopped:
                OpenChildRigidbodys(BotLeafs);
                break;
            case TreeState.destroyed:
                OpenChildRigidbodys(Wood);
                break;
            default:
                break;
        }
    }
    void OpenChildRigidbodys(Transform parent)
    {
        StaticTree.SetActive(false);
        parent.gameObject.transform.parent.gameObject.SetActive(true);
        foreach (Transform child in parent)
        {
            child.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(child.gameObject, 2);
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
