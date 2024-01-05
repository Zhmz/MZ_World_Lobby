using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tuwan;
using UnityEngine;
using UnityEngine.UI;

namespace Tuwan
{
    public class LobbyChat : UGuiForm
    {
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }

        public void BroadcastMsg(TMP_InputField input)
        {
             var inputTex = input.text;
        }
    }
}
