using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using Tuwan;
using Tuwan.Const;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
    public class LobbyBGMForm : UGuiForm
    {
        public Button ExitButton;
        public GameObject BGMItem;
        public GameObject ScrollViewContent;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            initMusicList();

        }
        private void initMusicList()
        {
            for (int i = 0; i < Store.BgmList.Count; i++)
            {
                GameObject item = Instantiate(BGMItem, ScrollViewContent.transform);
                LobbyBGMItem itemScipt = item.GetComponent<LobbyBGMItem>();
                itemScipt.Init(i, Store.BgmList[i]["musicName"]);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(ScrollViewContent.GetComponent<RectTransform>());
        }


        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            ExitButton.onClick.AddListener(OnExitButtonClick);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);

            ExitButton.onClick.RemoveListener(OnExitButtonClick);

        }

        void OnExitButtonClick()
        {
            Close();
        }
    }
}
