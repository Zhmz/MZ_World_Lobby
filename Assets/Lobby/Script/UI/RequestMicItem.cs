using System.Collections;
using System.Collections.Generic;
using Tuwan;
using Tuwan.Lobby.Logic;
using Tuwan.Proto;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
    public class RequestMicItem : MonoBehaviour
    {
        public HorizontalLayoutGroup InfoLayoutGroup;
        public Image Head;
        public Text Text_Name;
        public Text Text_Index;
        public GameObject Male;
        public GameObject FeMale;
        private UserInfoResponsedData UserInfo;

        void Start()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(InfoLayoutGroup.GetComponent<RectTransform>());
        }
        public void Init(int idx, UserInfoResponsedData data)
        {
            UserInfo = data;
            ResManager.inst.LoadTextureUrl(Head, UserInfo.avatar);
            Text_Name.text = TuwanUtils.SubString(UserInfo.nickname, 6);

            Text_Index.text = (idx + 1).ToString();
            Male.SetActive(UserInfo.sex == 0);
            FeMale.SetActive(UserInfo.sex == 1);
        }
        public void OnClickAgree()
        {
            WheatCtrl.AgreeWheat(UserInfo.uid);
            WheatCtrl.UnAgreeWheat(UserInfo.uid);
        }
        public void OnClickDisAgree()
        {
            WheatCtrl.UnAgreeWheat(UserInfo.uid);

        }

        void Update()
        {

        }
    }
}
