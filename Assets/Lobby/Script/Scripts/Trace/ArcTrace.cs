using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//圆弧
public class ArcTrace : BaseTrace
{
    [Header("抛物线的高度")]
    private float arcHeight = 2f;

    private float distanceToTarget;

    protected override void Start()
    {
        base.Start();
        // 计算起始点到落点的距离
        distanceToTarget = Vector3.Distance(startPosition, targetTransform.position);
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Vector3 targetPosition = targetTransform.position;
            targetPosition += OffsetVec;

            // 计算已经经过的时间
            float elapsedTime = Time.time - startTime;

            // 计算当前位置
            float lerpValue = elapsedTime * speed / distanceToTarget;
            Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, lerpValue);

            // 计算抛物线高度
            currentPos.y += Mathf.Sin(lerpValue * Mathf.PI) * arcHeight;

            // 移动物体
            transform.position = currentPos;

            // 如果已经到达目标点，结束移动
            if (lerpValue >= 1f)
            {
                // 确保物体在落点位置
                transform.position = targetPosition;
                isMoving = false;

                //Destroy(this.gameObject);
                PoolManager.DestoryByRecycle(this.gameObject);
            }
        }
    }

    public void SetArcHeight(float height)
    {
        arcHeight = height;
    }
}
