using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TurnAnimation : MonoBehaviour
{
    [SerializeField] bool turning=false;
    [SerializeField]float speed = 1f;
    float turnSpeed = 1;
    //private void Awake() => PlayerState.OnEventChange += PlayerState_OnEventChange;

    private void Awake()
    {
        PlayerState.OnEventChange += PlayerState_OnEventChange;
        PlayerUnit.Instance.chopper.OnBladeChange += Blade_OnBladeChange;
    }

    private void Blade_OnBladeChange(Blade obj)
    {
        SetSpeed(obj.speed);
        transform.DOKill();
        turnSpeed = 10 / speed;
        Debug.Log(turnSpeed);
        transform.DOLocalRotate(new Vector3(0, 360, 0), turnSpeed, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }

    private void PlayerState_OnEventChange(BladeState obj)
    {
        turning = obj == BladeState.Standing;
        //Debug.Log("turning:" + turning);
        if (!turning)
            transform.DOPlay();
        else
            transform.DOPause();
    }

    private void OnDestroy()
    { 
         PlayerState.OnEventChange -= PlayerState_OnEventChange;
    } 
    void SetSpeed(float _i)
    {
        speed = _i;
        Debug.Log("Speed changed to: " + speed);
    }
}
