using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using Tuwan;
using Tuwan.Const;
using Tuwan.Proto;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
    public class RequestMicForm : UGuiForm
    {
        public Button ExitButton;
        public GameObject RequestItem;
        public GameObject ScrollViewContent;

        private List<UserInfoResponsedData> ApplyList = new List<UserInfoResponsedData>();

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

        }
        private void RefrenshList(List<UserInfoResponsedData> list)
        {
            TuwanUtils.ClearChildren(ScrollViewContent.transform);
            ApplyList = list;
            for (int i = 0; i < ApplyList.Count; i++)
            {
                GameObject item = Instantiate(RequestItem, ScrollViewContent.transform);
                RequestMicItem s_Item = item.GetComponent<RequestMicItem>();
                s_Item.Init(i, ApplyList[i]);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(ScrollViewContent.GetComponent<RectTransform>());
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            RefrenshList(userData as List<UserInfoResponsedData>);
            RegisterSocketListener();
            ExitButton.onClick.AddListener(OnExitButtonClick);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            UnRegisterSocketListener();
            ExitButton.onClick.RemoveListener(OnExitButtonClick);
            // Destroy(gameObject);
        }
        void RegisterSocketListener()
        {
            EventCenter.inst.AddSocketEventListener<string, string>((int)SocketIoType.APPLY_WHEAT, OnApplyWheat);
        }
        void UnRegisterSocketListener()
        {
            EventCenter.inst.RemoveSocketEventListener<string, string>((int)SocketIoType.APPLY_WHEAT, OnApplyWheat);
        }
        private void OnApplyWheat(string uid, string data)
        {

            if (LobbyData.inst.JudgeSelfHoster())
            {
                List<UserInfoResponsedData> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserInfoResponsedData>>(data);
                RefrenshList(list);
            }

        }
        void OnExitButtonClick()
        {
            Close();

        }
    }
}
