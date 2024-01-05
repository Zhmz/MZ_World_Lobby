using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTrace : BaseTrace
{
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            if (targetTransform != null)
            {
                Vector3 targetPosition = targetTransform.position;

                targetPosition += OffsetVec;

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                if (transform.position == targetPosition)
                {
                    isMoving = false;

                    //Destroy(this.gameObject);
                    PoolManager.DestoryByRecycle(this.gameObject);
                }

            }
        }
    }
}
