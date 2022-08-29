using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace LevelSystem
{
    public class LevelDesingUnit : MonoBehaviour
    {
        public int levelIndex;
        public Level level;
        public int TreeCount;
        [SerializeField] MeshRenderer[] wallsMesh;
        [SerializeField] MeshRenderer GroundMesh;
        [SerializeField] GameObject TreeSpawnParent;
        public Collider nextCollider,frontCollider;
        [Header("Store Area")]
        [SerializeField] GameObject bladeStore;
        [SerializeField] GameObject wood2coinTrade;
        [SerializeField] GameObject coin2gemTrade;
        Material[] wallMat;
        Material material;
        List<GameObject> assetForests=new List<GameObject>();
        void SetData()
        {

            SetMaterial();

            #region Shop setters
            bladeStore.SetActive(level.bladeStore);
            wood2coinTrade.SetActive(level.wood2coinTrade);
            coin2gemTrade.SetActive(level.coin2GemTrade);
            #endregion
        }
        public void SpawnTrees()
        {
            TreeUnit.spawnedLevelIndex= PlayerPrefs.GetInt("level");
            Debug.Log(TreeSpawnParent.transform.GetChild(0).gameObject.name);
            Destroy(TreeSpawnParent.transform.GetChild(0).gameObject);
            //TreeSpawnParent.transform.GetChild(0).gameObject.SetActive(false);
            var forest = GetPrefabFromResource((int)level.treeModel);
            var obj=Instantiate(forest, TreeSpawnParent.transform);
            TreeCount = obj.transform.childCount * obj.transform.GetChild(0).gameObject.transform.childCount;
            Debug.Log("Tree count" + TreeCount);
        }
        public void ReuseSpawnedTrees()
        {
            Debug.Log(TreeSpawnParent.transform.GetChild(0).gameObject.name);
            //Destroy(TreeSpawnParent.transform.GetChild(0).gameObject);
            //TreeSpawnParent.transform.GetChild(0).gameObject.SetActive(false);
            var forest = GetPrefabFromResource((int)level.treeModel);
            var obj = Instantiate(forest, TreeSpawnParent.transform);
            TreeCount = obj.transform.childCount * obj.transform.GetChild(0).gameObject.transform.childCount;
            Debug.Log("Tree count" + TreeCount);
        }
        public void SetTreeColors()
        {
            TreeUnit.treeColor = level.firstTreeColor;
            TreeUnit.secondSideTreeColor = level.secondTreeColor;
        }
        void SetMaterial()
        {
            material = new Material(GetMaterialFromResource("Walls", (int)level.groundType));
            for (int i = 0; i < wallsMesh.Length; i++)
            {
                wallMat = wallsMesh[i].materials;
                wallMat[0] = material;
                wallMat[0].color = level.groundColor;
                wallsMesh[i].materials = wallMat;
            }
            material = new Material(GetMaterialFromResource("Ground", (int)level.groundType));
            wallMat = GroundMesh.materials;
            wallMat[0] = material;
            wallMat[0].color = level.groundColor;
            GroundMesh.materials = wallMat;
        }

        Material GetMaterialFromResource(string _path, int i)
        {
            i--;
            i %= 4;
            i++;
            string path = "Level/Materials/" + _path +"/" +i.ToString();
            var mat = Resources.Load<Material>(path);
            if (mat!=null)
            {
                return mat;
            }
            else {
                return Resources.Load<Material>("Level/Materials/" + _path + "/" + "0");
            }
                
        }
        GameObject GetPrefabFromResource(int i)
        {
            i--;
            i %= 4;
            i++;
            string path = "Forest/"  + i.ToString();
            var mat = Resources.Load<GameObject>(path);
            if (mat != null)
            {
                return mat;
            }
            else
            {
                return Resources.Load<GameObject>("Forest/0");
            }

        }

        public void SetLevelIndex(int level)
        {
            levelIndex = level;
            SetData();
        }

    }
}

