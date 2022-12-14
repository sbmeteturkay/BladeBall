using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelSystem;
using DG.Tweening;
public class TreeUnit : MonoBehaviour
{
    public class TreeInstance
    {
        [Header("In Game Values")]
        public float health = 12;
        public float currentHealth = 12;
        public TreeState treeState = TreeState.full;
        public int givenGold = 1;
        public void SetState(TreeState _treeState)
        {
            treeState = _treeState;
        }
    }

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
    TreeInstance copiedTree=new TreeInstance();
    public static Color treeColor,secondSideTreeColor;
    public bool secondSideTree = false;
    
    int spawnedIndex;
    public static int spawnedLevelIndex = 1;
    [SerializeField] AudioSource source;
    [Header("Rigidbodys")]
    [SerializeField] Rigidbody[] topLeafsRB;
    [SerializeField] Rigidbody[] botLeafsRB;
    [SerializeField] Rigidbody[] woodRB;
    List<Vector3> topLeafsRBTransforms = new List<Vector3>();
    List<Vector3> botLeafsRBTransforms = new List<Vector3>();
    List<Vector3> woodTransforms = new List<Vector3>();
    private void Start()
    {
        spawnedIndex = spawnedLevelIndex;
        copiedTree.currentHealth = tree.currentHealth;
        copiedTree.health = tree.health;
        copiedTree.treeState = tree.treeState;
        if (secondSideTree)
        {
            treeMat.material.color = secondSideTreeColor;
        }
        else { treeMat.material.color = treeColor; }
        LevelDesingUnit.OnTreeReload += LevelDesingUnit_OnTreeReload;
    }

    private void LevelDesingUnit_OnTreeReload()
    {
        if(spawnedIndex==spawnedLevelIndex-2)
            ResetAll();
    }

    private void CopiedTree_OnEventChange(TreeState state)
    {
        copiedTree.SetState(state);
        switch (state)
        {
            case TreeState.full:
                StaticTree.transform.DOShakePosition(1, 0.1f);
                source.PlayOneShot(source.clip);
                break;
            case TreeState.leafless:
                OpenChildRigidbodys(TopLeafs,topLeafsRB,topLeafsRBTransforms);
                BotLeafs.transform.DOShakePosition(1,0.1f);
                wood.transform.DOShakePosition(1, 0.1f);
                //SoundManager.Instance.Play(SoundManager.Sounds.leafFall,false);
                break;
            case TreeState.chopped:
                OpenChildRigidbodys(BotLeafs,botLeafsRB,botLeafsRBTransforms);
                fullTree.enabled = false;
                wood.enabled = true;
                wood.transform.DOShakePosition(1, 0.1f);
                //SoundManager.Instance.Play(SoundManager.Sounds.treeFall,false);
                break;
            case TreeState.destroyed:
                OpenChildRigidbodys(Wood,woodRB,woodTransforms);
                wood.enabled = false;
                GameDataManager.AddCoins(copiedTree.givenGold, CollectType.wood);
                SoundManager.Instance.Play(SoundManager.Sounds.treeDestroy,false);
                //source.Play();
                if (spawnedLevelIndex == spawnedIndex)
                {
                    LevelManager.OnTreeBreak.Invoke();
                }                //Helpers.Wait(this, 3f, () => { ResetAll(); });
                //
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
        if(copiedTree.currentHealth > tree.health / 3 * 2)
        {
            CopiedTree_OnEventChange(TreeState.full);
        }
        if (copiedTree.treeState==TreeState.full && copiedTree.currentHealth <= tree.health / 3 * 2)
        {
            if(copiedTree.treeState!= TreeState.leafless)
                CopiedTree_OnEventChange(TreeState.leafless);
        }
        if (copiedTree.treeState == TreeState.leafless && copiedTree.currentHealth <= tree.health / 3 * 1)
        {
            if (copiedTree.treeState != TreeState.chopped)
                CopiedTree_OnEventChange(TreeState.chopped);
        }
        if (copiedTree.treeState == TreeState.chopped && copiedTree.currentHealth <= 0)
        {
            if (copiedTree.treeState != TreeState.destroyed)
                CopiedTree_OnEventChange(TreeState.destroyed);
        }
    }
    void OpenChildRigidbodys(Transform parent)
    {
        StaticTree.SetActive(false);
        parent.gameObject.transform.parent.gameObject.SetActive(true);
        foreach (Transform child in parent)
        {
            child.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(child.gameObject,2f);
        }
    }
    void OpenChildRigidbodys(Transform parent,Rigidbody[] rigidbodies, List<Vector3> transforms)
    {
        StaticTree.SetActive(false);
        parent.gameObject.transform.parent.gameObject.SetActive(true);
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = false;
            transforms.Add(rigidbodies[i].transform.localPosition);
        }
        Helpers.Wait(this, 2f, () => { parent.gameObject.SetActive(false); });
        //parent.gameObject.SetActive(false);
    }

    private void ResetAll()
    {
        ResetRB(woodRB, woodTransforms);
        ResetRB(botLeafsRB, botLeafsRBTransforms);
        ResetRB(topLeafsRB, topLeafsRBTransforms);
        wood.enabled = false;
        fullTree.enabled = true;
        Wood.gameObject.SetActive(true);
        TopLeafs.gameObject.SetActive(true);
        BotLeafs.gameObject.SetActive(true);
        BotLeafs.parent.gameObject.SetActive(false);
        copiedTree.currentHealth = tree.currentHealth;
        copiedTree.health = tree.health;
        copiedTree.treeState = tree.treeState;
        spawnedIndex = spawnedLevelIndex;
        if (secondSideTree)
        {
            treeMat.material.color = secondSideTreeColor;
        }
        else { treeMat.material.color = treeColor; }
        StaticTree.SetActive(true);
    }
    void ResetRB(Rigidbody[] rigidbodies,List<Vector3> transforms)
    {
        
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = true;
            if (transforms.Count > 0)
            {
                rigidbodies[i].transform.localPosition= transforms[i];
            }
            rigidbodies[i].velocity = new Vector3(0f, 0f, 0f);
            rigidbodies[i].angularVelocity = new Vector3(0f, 0f, 0f);
            rigidbodies[i].angularDrag = 0f;
            //Destroy(child.gameObject, 2f);
        }
        transforms.Clear();
    }
}
public enum TreeState
{
    full,
    leafless,
    chopped,
    destroyed
}
