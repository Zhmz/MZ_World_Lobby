using System.Collections;
using System.Collections.Generic;
using Tuwan;
using UnityEngine;
using UnityEngine.UI;

namespace Home
{
    public class HomeForm : UGuiForm
    {
        public Button HomeButton;
        public Button WorldButton;
        public Button MineButton;

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
            StopAllCoroutines();
        }


        public void OnHomeButtonClick()
        {
            GameEntry.UI.OpenUIForm(UIFormId.LobbyForm);
        }

        public void OnWorldButtonClick()
        {
            GameEntry.UI.OpenDialog(new DialogParams()
            {
                Mode = 2,
                Title = "确认弹窗",
                Message = "是否进入兔玩世界",
                OnClickConfirm = delegate (object userData) {
                    GameEntry.UI.OpenUIForm(UIFormId.SceenLoading);
                    GameEntry.Event.Fire(this, SwitchProcedureSuccessEventArgs.Create("ProcedureHome", "ProcedureWorld"));
                    GameEntry.UI.CloseAllLoadedUIForms();
                },
            });
        }

        public void OnMineButtonClick()
        {

        }
    }
}