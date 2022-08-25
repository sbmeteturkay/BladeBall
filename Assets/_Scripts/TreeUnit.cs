using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelSystem;
public class TreeUnit : MonoBehaviour
{
    public Tree tree;
    [Header("Parts to being destructed")]
    [SerializeField] Transform TopLeafs;
    [SerializeField] Transform BotLeafs;
    [SerializeField] Transform Wood;
    [Header("LowPoly version of trees without any rigidbodys or multiple meshes for performance")]
    [SerializeField] GameObject StaticTree;
    [SerializeField] MeshRenderer treeMat;
    [Header("Colliders before and after destruction")]
    [SerializeField] Collider fullTree;
    [SerializeField] Collider wood;
    Tree copiedTree;
    public static Color treeColor,secondSideTreeColor;
    public bool secondSideTree = false;
    private void Start()
    {
        copiedTree = ScriptableObject.CreateInstance<Tree>();
        copiedTree.currentHealth = tree.currentHealth;
        copiedTree.health = tree.health;
        copiedTree.treeState = tree.treeState;
        if (secondSideTree)
        {
            treeMat.material.color = secondSideTreeColor;
        }
        else { treeMat.material.color = treeColor; }
        copiedTree.OnEventChange += CopiedTree_OnEventChange;
    }

    private void CopiedTree_OnEventChange(TreeState obj)
    {
        switch (copiedTree.treeState)
        {
            case TreeState.full:
                break;
            case TreeState.leafless:
                OpenChildRigidbodys(TopLeafs);
                //SoundManager.Instance.Play(SoundManager.Sounds.leafFall,false);
                break;
            case TreeState.chopped:
                OpenChildRigidbodys(BotLeafs);
                fullTree.enabled = false;
                wood.enabled = true;
                //SoundManager.Instance.Play(SoundManager.Sounds.treeFall,false);
                break;
            case TreeState.destroyed:
                OpenChildRigidbodys(Wood);
                wood.enabled = false;
                GameDataManager.AddCoins(copiedTree.givenGold, CollectType.wood);
                SoundManager.Instance.Play(SoundManager.Sounds.treeDestroy,false);
                LevelManager.OnTreeBreak.Invoke();
                Helpers.Wait(this, 2f, () => { StaticTree.transform.parent.gameObject.SetActive(false); });
                break;
            default:
                
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) { 
            BladeAction.Instance.DoDamage(copiedTree);
            CheckState();
        }
    }
    void CheckState()
    {
        if (copiedTree.treeState==TreeState.full && copiedTree.currentHealth <= tree.health / 3 * 2)
        {
            if(copiedTree.treeState!= TreeState.leafless)
                copiedTree.SetState(TreeState.leafless);
        }
        if (copiedTree.treeState == TreeState.leafless && copiedTree.currentHealth <= tree.health / 3 * 1)
        {
            if (copiedTree.treeState != TreeState.chopped)
                copiedTree.SetState(TreeState.chopped);
        }
        if (copiedTree.treeState == TreeState.chopped && copiedTree.currentHealth <= 0)
        {
            if (copiedTree.treeState != TreeState.destroyed)
                copiedTree.SetState(TreeState.destroyed);
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
