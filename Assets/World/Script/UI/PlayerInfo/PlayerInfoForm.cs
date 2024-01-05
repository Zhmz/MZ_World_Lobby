using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tuwan;
using Tuwan.Proto;

namespace World
{
    public class PlayerInfoForm : UGuiForm
    {
        public Image AvatarImage;
        public Text IDText;
        public Image LevelImage;
        public Text LevelText;
        public Text NameText;
        public Button SendGiftButton;
        public Button ReportButton;
        public Button ExitButton;

        private UserInfoResponsedData UserInfo = null;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            UserInfo = userData as UserInfoResponsedData;
            InitPlayInfoUI();
            SendGiftButton.onClick.AddListener(OnSendGiftButtonClick);
            ReportButton.onClick.AddListener(OnReportButtonClick);
            ExitButton.onClick.AddListener(OnExitButtonClick);
        }
        private void InitPlayInfoUI()
        {
            ResManager.inst.LoadTextureUrl(AvatarImage, UserInfo.avatar);
            IDText.text = "UID:" + UserInfo.uid.ToString();
            NameText.text = UserInfo.nickname.ToString();
        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            SendGiftButton.onClick.RemoveListener(OnSendGiftButtonClick);
            ReportButton.onClick.RemoveListener(OnReportButtonClick);
            ExitButton.onClick.RemoveListener(OnExitButtonClick);
        }

        void OnSendGiftButtonClick()
        {
            Debug.Log("OnSendGiftButtonClick");
            GameEntry.UI.OpenUIForm(UIFormId.GiftForm, UserInfo);
            Close();
        }

        void OnReportButtonClick()
        {
            Debug.Log("OnReportButtonClick");
        }

        void OnExitButtonClick()
        {
            Close();
        }
    }
}
