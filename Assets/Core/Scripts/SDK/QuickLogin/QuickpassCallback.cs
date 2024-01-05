using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickpassCallback : AndroidJavaProxy
{
    public QuickpassCallback() : base("com.netease.nis.quicklogin.listener.QuickLoginTokenListener") { }
    public void onGetTokenSuccess(string YDToken, string accessCode)
    {
        Debug.Log("onGetTokenSuccess" + YDToken);
        Debug.Log("onGetTokenSuccess" + accessCode);
        QuickpassHandler.CloseLoginAuthView();
    }

    public void onGetTokenError(string YDToken, string msg)
    {
        Debug.Log("onGetTokenError" + YDToken);
        Debug.Log("onGetTokenError" + msg);
        QuickpassHandler.CloseLoginAuthView();
    }

    public void onCancelGetToken()
    {
        QuickpassHandler.CloseLoginAuthView();
        Debug.Log("onCancelGetToken");
    }
}
