using System.IO;
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
        void SetData()
        {
            
            for(int i=0;i<wallsMesh.Length;i++)
            {
                wallMat = wallsMesh[i].materials;
                wallMat[0] = GetMaterialFromResource("Walls", (int)level.groundType);
                wallMat[0].color = level.groundColor;
                wallsMesh[i].materials = wallMat;
            }
            wallMat = GroundMesh.materials;
            wallMat[0] = GetMaterialFromResource("Ground", (int)level.groundType);
            wallMat[0].color = level.groundColor;
            GroundMesh.materials = wallMat;
            bladeStore.SetActive(level.bladeStore);
            wood2coinTrade.SetActive(level.wood2coinTrade);
            coin2gemTrade.SetActive(level.coin2GemTrade);
            TreeCount = TreeSpawnParent.transform.childCount * TreeSpawnParent.transform.GetChild(0).gameObject.transform.childCount;
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

        public void SetLevelIndex(int level)
        {
            levelIndex = level;
            SetData();
        }

    }
}

