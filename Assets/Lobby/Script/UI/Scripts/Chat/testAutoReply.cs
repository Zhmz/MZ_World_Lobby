using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tuwan {
    public class testAutoReply : MonoBehaviour
    {
        public ChatUI ui;
        // Use this for initialization

        float tick = 0;
        string delayText = "";

        void Start()
        {
            if (ui != null)
                ui.OnChatMessage += Ui_OnChatMessage;
        }

        private void Ui_OnChatMessage(string text, ChatUI.EnumChatMessageType messageType)
        {
            if (messageType == ChatUI.EnumChatMessageType.MessageRight)
            {
                delayText = text;
                tick = 0;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (delayText != "")
            {
                tick += Time.deltaTime;
                if (tick >= 1.2f)
                {
                    ui.AddChatMessage(delayText, ChatUI.EnumChatMessageType.MessageLeft);
                    delayText = "";
                    tick = 0;
                }
            }
        }
    }
}
