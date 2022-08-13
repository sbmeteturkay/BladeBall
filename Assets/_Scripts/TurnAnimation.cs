using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TurnAnimation : MonoBehaviour
{
    [SerializeField] bool turning=false;
    [SerializeField]float speed = 1f;
    private void Awake() => PlayerState.OnEventChange += PlayerState_OnEventChange;

    private void PlayerState_OnEventChange(BladeState obj)
    {
        turning = obj == BladeState.Standing;
        if (!turning)
            transform.DOPlay();
        else
            transform.DOPause();
    }

    private void OnDestroy() => PlayerState.OnEventChange -= PlayerState_OnEventChange;
    private void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 0), 0.1f * speed, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
    [SerializeField] void SetSpeed(float _i)
    {
        speed = _i;
    }
}
