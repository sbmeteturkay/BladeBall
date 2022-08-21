using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesing : MonoBehaviour
{
    [SerializeField]Vector3 spaceBetweenLevels;
    public event Action<PlayerPositionState> OnStateChange;
    public GameObject[] levelContainers;
    [SerializeField] PlayerPositionState playerPositionState;
    private void Start()
    {
        OnStateChange += LevelDesing_OnStateChange;
    }

    private void LevelDesing_OnStateChange(PlayerPositionState obj)
    {
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
    }
     public void ChangePositionState()
    {
        LevelDesing_OnStateChange(playerPositionState);
    }

    public enum PlayerPositionState
    {
        first,
        middle,
        last
    }
}

