using System.Collections;
using System.Collections.Generic;
using Tuwan;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = Tuwan.GameEntry;

namespace Home
{
    public class LoginStarter : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Log.Debug("LoginStarter");

            GameEntry.UI.OpenUIForm(UIFormId.LoginForm);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}