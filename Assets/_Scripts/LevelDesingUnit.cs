using System.IO;
using UnityEngine;

namespace LevelSystem
{
    public class LevelDesingUnit : MonoBehaviour
    {
        public int levelIndex;
        public Level level;
        [SerializeField] MeshRenderer[] wallsMesh;
        [SerializeField] MeshRenderer GroundMesh;
        [SerializeField] GameObject TreeSpawnParent;
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
                wallsMesh[i].materials = wallMat;
            }
            wallMat = GroundMesh.materials;
            wallMat[0] = GetMaterialFromResource("Ground", (int)level.groundType);
            GroundMesh.materials = wallMat;
            bladeStore.SetActive(level.bladeStore);
            wood2coinTrade.SetActive(level.wood2coinTrade);
            coin2gemTrade.SetActive(level.coin2GemTrade);
        }

        Material GetMaterialFromResource(string _path, int i)
        {
            i--;
            i %= 4;
            i++;
            string path = "Level/Materials/" + _path +"/" +i.ToString();
            var mat = Resources.Load<Material>(path);
            Debug.Log(path);
            if (mat!=null)
            {
                return mat;
            }
            else {
                Debug.Log("Default mat: "+ "Level / Materials / " + _path + " / " + "0");
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

