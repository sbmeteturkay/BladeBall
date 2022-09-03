using System;
using System.IO;
using UnityEngine;using UnityEngine.UI;

namespace LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        public event Action<PlayerPositionState> OnStateChange;
        public static Action OnTreeBreak;
        [Header("Level Creator Info")]
        public int level=1;
        [SerializeField] Vector3 spaceBetweenLevels;
        public GameObject[] levelContainers;
        public static PlayerPositionState playerPositionState;
        [SerializeField] GameObject[] inSceneForestAssets;
        LevelDesingUnit[] levelDesingUnits=new LevelDesingUnit[3];
        int brokenTrees = 0;
        [Header("Level UI")]
        [SerializeField] Image levelProgressBar;
        [SerializeField] TMPro.TMP_Text currentLevel, nextLevel;

        [SerializeField] ParticleSystem levelUpFX;
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
            SetNextLevelData();
            SetLevelText();
            OnTreeBreak += LevelManager_OnTreeBreak;
            levelProgressBar.fillAmount =(float) ((float)brokenTrees / (((float)levelDesingUnits[(int)playerPositionState].TreeCount / 10) * 9f));
           
            for(int i = 0; i < inSceneForestAssets.Length; i++)
            {
                levelDesingUnits[0].AddPrefabForests(inSceneForestAssets[i],i);
            }
            
            OnStateChange.Invoke(playerPositionState);
        }

        void SetNextLevelData()
        {
            levelDesingUnits[1].level = GetLevelDataFromResource(level + 1);
            levelDesingUnits[1].SetLevelIndex(level + 1);
            levelDesingUnits[2].level = GetLevelDataFromResource(level + 2);
            levelDesingUnits[2].SetLevelIndex(level + 2);
        }


        /// <summary>
        /// ///@@@@@@@@WARNINNNGGGGGGGGGGGG LOOOOKK HERE MY MAN
        /// </summary>
        private void LevelManager_OnTreeBreak()
        {
            brokenTrees++;
            levelProgressBar.fillAmount = (float)((float)brokenTrees / (((float)levelDesingUnits[(int)playerPositionState].TreeCount / 10) * 8f));
            if (levelProgressBar.fillAmount == 1&&brokenTrees!=0)
            {
                brokenTrees = 0;
                LevelUp();
            }
        }
        void SetLevelText()
        {
            currentLevel.text = level.ToString();
            nextLevel.text = (level + 1).ToString();
        }
        private void LevelDesing_OnStateChange(PlayerPositionState obj)
        {
            //set position of level places
            switch (obj)
            {
                case PlayerPositionState.first:
                    levelContainers[1].transform.position = levelContainers[0].transform.position + spaceBetweenLevels;
                    levelDesingUnits[0].nextCollider.enabled = true;
                    levelDesingUnits[2].frontCollider.enabled = true;
                    levelDesingUnits[1].frontCollider.enabled = true;
                    break;
                case PlayerPositionState.middle:
                    levelContainers[2].transform.position = levelContainers[1].transform.position + spaceBetweenLevels;
                    levelDesingUnits[1].nextCollider.enabled = true;
                    levelDesingUnits[0].frontCollider.enabled = true;
                    levelDesingUnits[2].frontCollider.enabled = true;
                    break;
                case PlayerPositionState.last:
                    levelContainers[0].transform.position = levelContainers[2].transform.position + spaceBetweenLevels;
                    levelDesingUnits[2].nextCollider.enabled = true;
                    levelDesingUnits[1].frontCollider.enabled = true;
                    levelDesingUnits[0].frontCollider.enabled = true;
                    break;
                default:
                    break;
            }
            //this function loads data of level place while setting level
            levelDesingUnits[(int)obj].level=GetLevelDataFromResource(level);
            levelDesingUnits[(int)obj].SetLevelIndex(level);
            levelDesingUnits[(int)obj].SetTreeColors();
            levelDesingUnits[(int)obj].SpawnTrees();
        }
        public void ChangePositionState()
        {
            LevelDesing_OnStateChange(playerPositionState);
        }
        public void LevelUp()
        {
            SoundManager.Instance.Play(SoundManager.Sounds.levelUp, true);
            Debug.Log("LEVEL UPPPPPPPPPPP");
            brokenTrees = 0;
            levelDesingUnits[(int)playerPositionState].nextCollider.enabled = false;
            level++;
            SetLevelText();
            PlayerPrefs.SetInt("level", level);
            if((int)playerPositionState<2)
            {
                playerPositionState++;
            }
            else
            {
                playerPositionState = 0;
            }
            levelDesingUnits[(int)playerPositionState].frontCollider.enabled = false;
            Debug.Log(playerPositionState);
            OnStateChange?.Invoke(playerPositionState);
            levelProgressBar.fillAmount = 0;
            levelUpFX.Play();
        }
        Level GetLevelDataFromResource(int i)
        {
            //to avoid default level which is 0, TYPE LAST NUMBER OF CREATED LEVELS
            string filePath = "Assets/Resources/Level/Level";
            int fileCount=Directory.GetFiles(filePath).Length;
            Debug.Log(fileCount);
            i--;
            i %= fileCount-1;
            i++;
            //
            string path = "Level/Level/"+ i.ToString();
            var lvl= Resources.Load<Level>(path);
            if (lvl!=null)
            {
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
        private void OnApplicationQuit()
        {
            GameDataManager.SavePlayerData();
        }
    }
}

