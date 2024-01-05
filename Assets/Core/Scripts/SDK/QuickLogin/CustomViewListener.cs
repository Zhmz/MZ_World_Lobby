using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomViewListener : AndroidJavaProxy
{
    public CustomViewListener() : base("com.netease.nis.quicklogin.utils.LoginUiHelper$CustomViewListener") { }

    public void onClick(AndroidJavaObject context, AndroidJavaObject view)
    {
        AndroidJavaObject tag = view.Call<AndroidJavaObject>("getTag");
        Debug.Log("自定义view被点击" + tag.Call<string>("toString"));
        QuickpassHandler.CloseLoginAuthView();
    }
}

