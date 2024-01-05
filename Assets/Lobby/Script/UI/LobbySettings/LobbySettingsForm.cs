using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tuwan;
using UnityEngine.UI;

namespace Lobby
{
    public class LobbySettingsForm : UGuiForm
    {
        public Button ExitButton;
        public Button TurnOnSoundButton;
        public Button TurnOffSoundButton;
        public Button ReportLobbyButton;

        //默认声音开着
        private bool IsSoundOn = true;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            UpdateSoundButtonState();
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            ExitButton.onClick.AddListener(OnExitButtonClick);
            TurnOnSoundButton.onClick.AddListener(OnTurnOnSoundButtonClick);
            TurnOffSoundButton.onClick.AddListener(OnTurnOffSoundButtonClick);
            ReportLobbyButton.onClick.AddListener(OnReportLobbyButtonClick);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);

            ExitButton.onClick.RemoveListener(OnExitButtonClick);
            TurnOnSoundButton.onClick.RemoveListener(OnTurnOnSoundButtonClick);
            TurnOffSoundButton.onClick.RemoveListener(OnTurnOffSoundButtonClick);
            ReportLobbyButton.onClick.RemoveListener(OnReportLobbyButtonClick);
        }

        void OnExitButtonClick()
        {
            Close();
        }

        void OnTurnOnSoundButtonClick()
        {
            Debug.Log("OnTurnOnSoundButtonClick");
            IsSoundOn = !IsSoundOn;
            UpdateSoundButtonState();
        }

        void OnTurnOffSoundButtonClick()
        {
            Debug.Log("OnTurnOffSoundButtonClick");
            IsSoundOn = !IsSoundOn;
            UpdateSoundButtonState();
        }

        void OnReportLobbyButtonClick()
        {
            Debug.Log("OnReportLobbyButtonClick");
        }

        void UpdateSoundButtonState()
        {
            TurnOnSoundButton.gameObject.SetActive(!IsSoundOn);
            TurnOffSoundButton.gameObject.SetActive(IsSoundOn);
        }
    }
}