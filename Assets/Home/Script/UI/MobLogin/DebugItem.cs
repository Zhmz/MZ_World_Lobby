using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class DebugItem : MonoBehaviour
{
    public string Cookie = "";
    // private
    public void Init(string uid, string token)
    {
        gameObject.GetComponentInChildren<Text>().text = "游客" + uid;
        Cookie = token;
    }


}
