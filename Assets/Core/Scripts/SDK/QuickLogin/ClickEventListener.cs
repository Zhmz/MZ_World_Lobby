using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEventListener : AndroidJavaProxy
{
    public ClickEventListener() : base("com.netease.nis.quicklogin.listener.ClickEventListener") { }

    public void onClick(int viewType, int code)
    {
        Debug.Log("被点击的view为：" + viewType + "协议复选框状态为：" + code);
    }
}
