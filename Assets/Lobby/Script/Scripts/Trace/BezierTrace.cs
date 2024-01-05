using UnityEngine;
using System.Collections;

public class BezierTrace : BaseTrace
{
    private Vector3 paramPos1;
    private Vector3 paramPos2;

    protected float totalTime;

    protected override void Start()
    {
        base.Start();

        totalTime = Vector3.Magnitude(targetTransform.position - startPosition) / speed;
    }


    private void FixedUpdate()
    {
        if (isMoving)
        {
            Vector3 targetPosition = targetTransform.position;
            targetPosition += OffsetVec;

            if (totalTime == 0)
            {
                return;
            }

            float lerpValue = (Time.time - startTime) / totalTime;
            //Debug.LogError();
            transform.position = BezierUtils.BezierCurve(startPosition, paramPos1, paramPos2, targetPosition, lerpValue);

            if (lerpValue >= 1)
            {
                transform.position = targetPosition;
                isMoving = false;

                //Destroy(this.gameObject);
                PoolManager.DestoryByRecycle(this.gameObject);
            }
        }
    }

    public void SetBezierParamPos(Vector3 pos1, Vector3 pos2)
    {
        paramPos1 = pos1;
        paramPos2 = pos2;
    }

    public void SetBezierParamPosSimplely(Vector3 start, Vector3 end, float bezierHeight)
    {
        Vector3 direction = end - start;
        Vector3 part1 = direction * 1 / 3.0f;
        Vector3 part2 = direction * 2 / 3.0f;
        part1.y += bezierHeight;
        part2.y += bezierHeight;
        paramPos1 = start + part1;
        paramPos2 = start + part2;
    }
}
