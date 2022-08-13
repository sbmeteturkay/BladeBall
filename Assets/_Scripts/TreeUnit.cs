using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeUnit : MonoBehaviour
{
    public Tree tree;
    [Header("Parts to being destructed")]
    [SerializeField] Transform TopLeafs;
    [SerializeField] Transform BotLeafs;
    [SerializeField] Transform Wood;
    [Header("LowPoly version of trees without any rigidbodys or multiple meshes for performance")]
    [SerializeField] GameObject StaticTree;
    [Header("Colliders before and after destruction")]
    [SerializeField] Collider fullTree;
    [SerializeField] Collider wood;
    Tree copiedTree;
    private void Start()
    {
        copiedTree = ScriptableObject.CreateInstance<Tree>();
        copiedTree.color = tree.color;
        copiedTree.currentHealth = tree.currentHealth;
        copiedTree.health = tree.health;
        copiedTree.treeState = tree.treeState;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Blade") { 
            BladeAction.Instance.DoDamage(copiedTree);
            CheckState();
        }
    }
    void CheckState()
    {
        switch (copiedTree.treeState)
        {
            case TreeState.full:
                break;
            case TreeState.leafless:
                OpenChildRigidbodys(TopLeafs);
                break;
            case TreeState.chopped:
                OpenChildRigidbodys(BotLeafs);
                fullTree.enabled = false;
                wood.enabled = true;
                break;
            case TreeState.destroyed:
                OpenChildRigidbodys(Wood);
                Helpers.Wait(this, 2f, () => { StaticTree.transform.parent.gameObject.SetActive(false); });
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
