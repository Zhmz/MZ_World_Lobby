using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tuwan;
using FishNet.Object;
using GameFramework.Event;
using Tuwan.Const;

namespace Lobby
{
    public class LobbyStarter : NetworkBehaviour
    {
        public AudioSource Audio;
        // Start is called before the first frame update
        void Start()
        {
            //if (IsClient)
            //{
            //    EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_FROM_WORLD_ENTER_LOBBY);
            //    EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_ENTER_LOBBY_FORM, "LobbyOther");
            //}
            OnChangeBgm(0);
        }
        void OnChangeBgm(int Idx)
        {
            if (Audio == null)
            {
                return;
            }

            if (Store.BgmList[Idx] != null && Store.BgmList[Idx].ContainsKey("path"))
            {
                string musicPath = Store.BgmList[Idx]["path"];
                AudioClip musicClip = Resources.Load<AudioClip>(musicPath);
                if (musicClip != null)
                {
                    Debug.Log("AudioClip加载成功:" + musicPath);
                    Audio.clip = musicClip;
                    Audio.Play();
                }

            }
        }
        private void OnEnable()
        {
            EventCenter.inst.AddEventListener<int>((int)UIEventTag.EVENT_UI_CHANGE_BGM, OnChangeBgm);
        }
        private void OnDisable()
        {
            EventCenter.inst.RemoveEventListener<int>((int)UIEventTag.EVENT_UI_CHANGE_BGM, OnChangeBgm);
        }
        // Update is called once per frame
        void Update()
        {

        }

    }
}
