using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera cameraToLookAt;
    public SelectXYZ selectXYZ = SelectXYZ.None;

    void LateUpdate()
    {
        //��cameraToLookAtΪ�գ����Զ�ѡ���������
        if (cameraToLookAt == null)
            cameraToLookAt = Camera.main;

        Vector3 vector3 = cameraToLookAt.transform.position - transform.position;
        switch (selectXYZ)
        {
            case SelectXYZ.x:
                vector3.y = vector3.z = 0.0f;
                break;
            case SelectXYZ.y:
                vector3.x = vector3.z = 0.0f;
                break;
            case SelectXYZ.z:
                vector3.x = vector3.y = 0.0f;
                break;
            case SelectXYZ.None:
                vector3.x = vector3.y = vector3.z = 0.0f;
                break;
        }

        transform.LookAt(cameraToLookAt.transform.position - vector3);
    }

    public enum SelectXYZ
    {
        x,
        y,
        z,
        None
    }

}

