using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickpassPreCallback : AndroidJavaProxy
{
    public QuickpassPreCallback() : base("com.netease.nis.quicklogin.listener.QuickLoginPreMobileListener") { }

    public void onGetMobileNumberSuccess(string YDToken, string mobileNumber)
    {
        Debug.Log("onGetMobileNumberSuccess" + YDToken);
        Debug.Log("onGetMobileNumberSuccess" + mobileNumber);
    }

    public void onGetMobileNumberError(string YDToken, string msg)
    {
        Debug.Log("onGetMobileNumberError" + YDToken);
        Debug.Log("onGetMobileNumberError" + msg);
    }
}
