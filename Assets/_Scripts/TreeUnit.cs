using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelSystem;
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
    public static int spawnedLevelIndex = 1;
    public bool secondSideTree = false;
    int spawnedIndex;
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
    }

    private void CopiedTree_OnEventChange(TreeState state)
    {
        copiedTree.SetState(state);
        switch (state)
        {
            case TreeState.full:
                break;
            case TreeState.leafless:
                OpenChildRigidbodys(TopLeafs,topLeafsRB,topLeafsRBTransforms);
                //SoundManager.Instance.Play(SoundManager.Sounds.leafFall,false);
                break;
            case TreeState.chopped:
                OpenChildRigidbodys(BotLeafs,botLeafsRB,botLeafsRBTransforms);
                fullTree.enabled = false;
                wood.enabled = true;
                //SoundManager.Instance.Play(SoundManager.Sounds.treeFall,false);
                break;
            case TreeState.destroyed:
                OpenChildRigidbodys(Wood,woodRB,woodTransforms);
                wood.enabled = false;
                GameDataManager.AddCoins(copiedTree.givenGold, CollectType.wood);
                SoundManager.Instance.Play(SoundManager.Sounds.treeDestroy,false);
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
            transforms.Add(rigidbodies[i].position);
        }
        Debug.Log("msdalmdsadsa");
        Helpers.Wait(this, 2f, () => { parent.gameObject.SetActive(false); });
        Debug.Log("mölsdþadmösadsla");
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
            rigidbodies[i].transform.position = transforms[i];
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
