using LevelSystem;
using UnityEngine;

public class TargetLookAt : MonoBehaviour
{
    [SerializeField] Transform[] tradeArea;
    [SerializeField] Transform levelUp;
    public static Transform target;
    private void Start()
    {
        target = tradeArea[0];
        PlayerState.OnEventChange += PlayerState_OnEventChange;
    }

    private void PlayerState_OnEventChange(BladeState obj)
    {
        if (obj == BladeState.Full)
            target = tradeArea[(int)LevelManager.playerPositionState];
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), 1);
    }
}
