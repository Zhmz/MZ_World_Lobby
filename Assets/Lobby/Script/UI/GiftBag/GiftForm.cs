using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tuwan;
using UnityEngine.UI;
using System.Linq;
using Tuwan.Lobby.Entity;
using Unity.VisualScripting;
using GameFramework.Event;
using Tuwan.Const;
using Tuwan.Proto;
using Tuwan.UserData;

namespace Lobby
{
    public class GiftForm : UGuiForm
    {
        public GameObject GiftContent;
        public Text Text_Diamond;

        public GameObject PrefabGiftItem;
        public Dropdown DropdownPlayer;
        public Dropdown DropdownCount;
        Dictionary<int, WheatResponse> WheatMap = new Dictionary<int, WheatResponse>();
        private int CurrentGiftId = 0;
        private int CurrentGiftCount = 1;
        private UserInfoResponsedData UserInfo = null;
        private List<int> TotalGiftUids = new List<int>();
        private List<int> CurrentSelectUids = new List<int>();
        private List<GiftItemResponse> GiftItemList = new List<GiftItemResponse>();
        private Dictionary<int, string> DicCount = new Dictionary<int, string>()
             {
                 { 1, "一心一意" },
                 { 10, "十全十美" },
                 { 66, "一切顺利" },
                 { 188, "要抱抱" },
                 { 520, "我爱你" },
                 { 1314, "一生一世" }
              };
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            InitGiftList();
            InitDropDownCount();
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            if (userData != null)
            {
                UserInfo = userData as UserInfoResponsedData;
            }
            InitUI();
            InitPlayerData();
            InitDropDownPlayer();
            DropdownCount.onValueChanged.AddListener(OnDropDownValueChange);
            DropdownPlayer.onValueChanged.AddListener(OnDropDownPlayerValueChange);
            EventCenter.inst.AddEventListener<int>((int)UIEventTag.EVENT_UI_GIFTBAG_TOGGLE_CHANGE, OnToggleChange);
            EventCenter.inst.AddEventListener((int)UIEventTag.EVENT_UI_DIAMOND_CHANGE, setDiamond);

        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            DropdownCount.onValueChanged.RemoveListener(OnDropDownValueChange);
            DropdownPlayer.onValueChanged.RemoveListener(OnDropDownPlayerValueChange);
            EventCenter.inst.RemoveEventListener<int>((int)UIEventTag.EVENT_UI_GIFTBAG_TOGGLE_CHANGE, OnToggleChange);
            EventCenter.inst.RemoveEventListener((int)UIEventTag.EVENT_UI_DIAMOND_CHANGE, setDiamond);
            base.OnClose(isShutdown, userData);

        }
        /// <summary>
        /// InitUI  初始化基础UI信息 
        /// </summary>
        private void InitUI()
        {
            setDiamond();
        }
        private void setDiamond()
        {
            if (Store.Config.MoneyInfo != null)
            {
                Text_Diamond.text = Store.Config.MoneyInfo.diamond_str;
            }
        }
        /// <summary>
        /// InitPlayerData  初始化麦位数据 
        /// </summary>
        private void InitPlayerData()
        {
            TotalGiftUids.Clear();
            CurrentSelectUids.Clear();
            WheatMap = LobbyData.inst.WheatMap;
            foreach (int key in WheatMap.Keys)
            {
                if (WheatMap[key].uid != Store.Config.UserInfo.uid)
                {
                    TotalGiftUids.Add(WheatMap[key].uid);
                }
            }
            if (UserInfo != null)
            {
                CurrentSelectUids.Add(UserInfo.uid);
            }
            else
            {
                CurrentSelectUids = TotalGiftUids;
            }

        }
        /// <summary>
        /// OnDropDownValueChange  礼物数量DropDown变化监听
        /// </summary>
        private void OnDropDownValueChange(int index)
        {
            List<int> keyList = DicCount.Keys.ToList();
            if (index >= 0 && index < keyList.Count)
            {
                DropdownCount.captionText.text = keyList[index].ToString();
                CurrentGiftCount = keyList[index];
            }
        }
        /// <summary>
        /// OnDropDownPlayerValueChange  麦位信息DropDown变化监听
        /// </summary>
        private void OnDropDownPlayerValueChange(int index)
        {
            CurrentSelectUids.Clear();
            if (index <= 0)
            {
                CurrentSelectUids = TotalGiftUids;
            }
            else
            {
                CurrentSelectUids.Add(WheatMap.ToList()[index - 1].Value.uid);
            }
        }
        /// <summary>
        /// OnToggleChange  当前礼物变化
        /// </summary>
        private void OnToggleChange(int GiftId)
        {
            CurrentGiftId = GiftId;
            // Debug.Log("CurrentGiftId===" + CurrentGiftId);
        }
        /// <summary>
        /// InitGiftList  初始化礼物列表
        /// </summary>
        private void InitGiftList()
        {
            GiftResponse response = Tuwan.Lobby.Logic.GiftBag.RequestGiftList();
            if (response != null)
            {
                GiftItemList = response.data;
                for (int i = 0; i < GiftItemList.Count; i++)
                {
                    if (i == 0)
                    {
                        CurrentGiftId = GiftItemList[i].id;
                    }
                    if (GiftItemList[i].type == 7)
                    {
                        continue;
                    }
                    GameObject obj = Instantiate(PrefabGiftItem);
                    GiftItem item = obj.GetComponent<GiftItem>();
                    item.Init(i, GiftItemList[i]);
                    item.transform.parent = GiftContent.transform;
                }
            }
        }
        /// <summary>
        /// InitDropDownCount  初始化礼物数量列表
        /// </summary>
        private void InitDropDownCount()
        {
            DropdownCount.options.Clear();
            foreach (int key in DicCount.Keys)
            {
                string OpDesc = "  " + key.ToString() + "    " + DicCount[key];
                var options = DropdownCount.options;
                options.Add(new Dropdown.OptionData(OpDesc));
            }
        }
        /// <summary>
        /// InitDropDownPlayer  初始化麦位信息列表
        /// </summary>
        private void InitDropDownPlayer()
        {
            DropdownPlayer.options.Clear();
            if (UserInfo != null)
            {
                DropdownPlayer.captionText.text = UserInfo.nickname;
            }
            else
            {
                string totalDesc = "送给麦上所有人";
                DropdownPlayer.captionText.text = totalDesc;
                DropdownPlayer.options.Add(new Dropdown.OptionData(totalDesc));
                foreach (int key in WheatMap.Keys)
                {
                    if (WheatMap[key].uid != Store.Config.UserInfo.uid)
                    {
                        string OpDesc = WheatMap[key].nickname;
                        var options = DropdownPlayer.options;
                        options.Add(new Dropdown.OptionData(OpDesc));
                    }
                }
            }

        }
        /// <summary>
        /// onClickSendGift  赠送礼物
        /// </summary>
        public void onClickSendGift()
        {
            string uidStr = string.Join(",", CurrentSelectUids);
            Tuwan.Lobby.Logic.GiftBag.SendGift(CurrentGiftId, CurrentGiftCount, uidStr);
        }


    }
}