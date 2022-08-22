using System;
using System.IO;
using UnityEngine;

namespace LevelSystem
{
    public class LevelDesing : MonoBehaviour
    {
        public event Action<PlayerPositionState> OnStateChange;
        public int level=1;
        [SerializeField] Vector3 spaceBetweenLevels;
        public GameObject[] levelContainers;
        [SerializeField] PlayerPositionState playerPositionState;

        LevelDesingUnit[] levelDesingUnits=new LevelDesingUnit[3];
        private void Awake()
        {
            OnStateChange += LevelDesing_OnStateChange;
            for(int i=0;i<levelContainers.Length;i++)
            {
              levelDesingUnits[i]=levelContainers[i].GetComponent<LevelDesingUnit>();
            }
            if (!PlayerPrefs.HasKey("level"))
                PlayerPrefs.SetInt("level", 1);
            level = PlayerPrefs.GetInt("level");
            OnStateChange.Invoke(playerPositionState);
            levelDesingUnits[1].level = GetLevelDataFromResource(level+1);
            levelDesingUnits[1].SetLevelIndex(level+1);
            levelDesingUnits[2].level = GetLevelDataFromResource(level + 2);
            levelDesingUnits[2].SetLevelIndex(level + 2);
        }

        private void LevelDesing_OnStateChange(PlayerPositionState obj)
        {
            //set position of level places
            switch (obj)
            {
                case PlayerPositionState.first:
                    levelContainers[1].transform.position = levelContainers[0].transform.position + spaceBetweenLevels;
                    break;
                case PlayerPositionState.middle:
                    levelContainers[2].transform.position = levelContainers[1].transform.position + spaceBetweenLevels;
                    break;
                case PlayerPositionState.last:
                    levelContainers[0].transform.position = levelContainers[2].transform.position + spaceBetweenLevels;
                    break;
                default:
                    break;
            }
            //this function loads data of level place while setting level
            Debug.Log((int)obj);
            levelDesingUnits[(int)obj].level=GetLevelDataFromResource(level);
            levelDesingUnits[(int)obj].SetLevelIndex(level);
            
        }
        public void ChangePositionState()
        {
            LevelDesing_OnStateChange(playerPositionState);
        }
        public void LevelUp()
        {
            level++;
            PlayerPrefs.SetInt("level", level);
            if((int)playerPositionState<2)
            {
                playerPositionState++;
            }
            else
            {
                playerPositionState = 0;
            }
            
            Debug.Log(playerPositionState);
            OnStateChange?.Invoke(playerPositionState);
        }
        Level GetLevelDataFromResource(int i)
        {
            i--;
            i %= 3;
            i++;
            Debug.Log("Level data" + i);
            string path = "Level/Level/"+ i.ToString();
            Debug.Log(path);
            var lvl= Resources.Load<Level>(path);
            if (lvl!=null)
            {
                Debug.Log("LEVEL EXIST "+i);
                return lvl;
            }
                
            else
                return Resources.Load<Level>("Level/Level/0");
        }
        public enum PlayerPositionState
        {
            first,
            middle,
            last
        }
    }
}

