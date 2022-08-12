using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TurnAnimation : MonoBehaviour
{
    [SerializeField] bool turning=false;
    [SerializeField]float speed = 1f;
    private void Awake() => GameManager.OnAfterStateChanged += GameManager_OnAfterStateChanged;
    private void OnDestroy() => GameManager.OnAfterStateChanged -= GameManager_OnAfterStateChanged;

    private void GameManager_OnAfterStateChanged(GameState obj)
    {
        turning = obj == GameState.Starting;
        if (!turning)
            transform.DOKill();
    }
    private void Update()
    {
        if (!turning) return;
        
    }
    private void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 0), 1f / speed, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
    void SetSpeed(float _i)
    {
        speed = _i;
    }
}
