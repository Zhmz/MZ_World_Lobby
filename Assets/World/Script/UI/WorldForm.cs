using System.Collections;
using System.Collections.Generic;
using Tuwan;
using UnityEngine;

namespace World
{
    public class WorldForm : UGuiForm
    {
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }

        public void OnEnterLobbyButtonClick()
        {
            GameEntry.Event.Fire(this, SwitchProcedureSuccessEventArgs.Create("ProcedureWorld", "ProcedureLobby"));
            Close();
        }

        public void OnReturnHomeButtonClick()
        {
            GameEntry.Event.Fire(this, SwitchProcedureSuccessEventArgs.Create("ProcedureWorld", "ProcedureHome"));
            Close();
        }
    }
}
