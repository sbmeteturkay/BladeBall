using UnityEngine;
using DG.Tweening;
public class UpAndDownAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float time=1;
    Vector3[] wayPoints;
    [SerializeField]float whenToStart=0;
    void Start()
    {
        wayPoints = new Vector3[3];
        wayPoints.SetValue(new Vector3(0, .5f), 0);
        wayPoints.SetValue(new Vector3(0, .5f), 1);
        transform.DOLocalPath(wayPoints, time/2).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart).SetDelay(whenToStart);
    }
}
