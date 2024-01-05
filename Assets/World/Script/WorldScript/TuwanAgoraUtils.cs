using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuwanAgoraUtils
{
    //输出zxy
    public static float[] FormatAgoraVec3(Vector3 vec)
    {
        return new float[] { vec.z, vec.x, vec.y };
    }
}
