using UnityEngine;
using DG.Tweening;

public class TimerVisual : MonoBehaviour
{
    private RectTransform Pointer;
    private float Rotation = 0f;
    public bool OnShift = false;

    private void Start()
    {
        Pointer = transform.Find("Pointer Pivot").GetComponent<RectTransform>();
    }

    private void LateUpdate()
    { 
        if (!OnShift) return;

        float ShiftTime = Manager.Instance.ShiftTime;
        Rotation -= (360f / ShiftTime) * Time.deltaTime;

        Pointer.localRotation = Quaternion.Euler(0f, 0f, Rotation);
    }

    public void MoveTimer(float MoveFrom, float MoveTo)
    {   
        DOTween.To(() => MoveFrom, x => MoveFrom = x, MoveTo, 1f).OnUpdate(() =>
        {
            transform.localPosition = new Vector3(transform.localPosition.x, MoveFrom, transform.localPosition.z);
        });
    }
}
