using UnityEngine;

public class BaseTrace : MonoBehaviour
{
    protected bool isMoving = true;

    //初始位置
    protected Vector3 startPosition;
    protected float startTime;

    //目标transform，目标可能会移动
    [Header("结束位置的Transform")]
    protected Transform targetTransform;

    [Header("移动速度")]
    protected float speed = 10f;

    [Header("目标位置的偏移")]
    protected Vector3 OffsetVec = Vector3.zero;

    // Use this for initialization
    protected virtual void Start()
    {
        startPosition = transform.position;
        // 计算移动的起始时间
        startTime = Time.time;
    }

    public void SetDestination(Transform target, Vector3 offset)
    {
        targetTransform = target;
        OffsetVec = offset;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }
}
