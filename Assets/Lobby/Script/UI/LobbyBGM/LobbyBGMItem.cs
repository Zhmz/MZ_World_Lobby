using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using Tuwan.Const;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
    public class LobbyBGMItem : MonoBehaviour
    {
        public Text MusicName;

        public GameObject Current;
        public GameObject Btn_Play;

        private int Index = 0;
        private void OnEnable()
        {
            EventCenter.inst.AddEventListener<int>((int)UIEventTag.EVENT_UI_CHANGE_BGM, OnChangeBgm);
        }
        private void OnDisable()
        {
            EventCenter.inst.RemoveEventListener<int>((int)UIEventTag.EVENT_UI_CHANGE_BGM, OnChangeBgm);
        }

        public void Init(int Idx, string Bgm)
        {
            Index = Idx;
            ;
            MusicName.text = Bgm;
            Current.SetActive(0 == Idx);
            Btn_Play.SetActive(0 != Idx);
        }

        public void OnClickPlay()
        {
            EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_CHANGE_BGM, Index);
        }
        private void OnChangeBgm(int Idx)
        {
            if (Index == Idx)
            {
                Current.SetActive(true);
                Btn_Play.SetActive(false);
            }
            else
            {
                if (Current.activeInHierarchy)
                {
                    Current.SetActive(false);
                }
                if (!Btn_Play.activeInHierarchy)
                {
                    Btn_Play.SetActive(true);
                }
            }
        }

    }
}
