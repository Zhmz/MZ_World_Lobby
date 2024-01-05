using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using cn.sharesdk.unity3d;
using FishNet.Managing.Object;
using GameFramework.Event;
using Tuwan.Script.Entity;
using Tuwan.Proto;
using Tuwan.Script.Logic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using Tuwan.Const;
using Org.BouncyCastle.Bcpg;
using Tuwan.UserData;
using Tuwan.Lobby.Logic;
using Unity.Burst.CompilerServices;


namespace Tuwan.Script.UI.UmengLogin
{
    public class LoginForm : UGuiForm
    {
        private string str = "result";
        public MobSDK mobsdk;
        public ShareSDK ssdk;
        public CommonButton QQButton;
        public CommonButton WeChatButton;
        public CommonButton TravelButton;
        public CommonButton QuickLoginButton;
        public CommonButton AppleButton;
        public CommonButton EditorModeButton;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
#if !UNITY_EDITOR
            //MobSDK 回调注册
            ssdk.authHandler = OnAuthResultHandler;
            mobsdk.submitPolicyGrantResult(true);
            // 云盾一键登录初始化
            QuickpassHandler.Init("eb7d1bca556b484cb8c51d3616d0f10f");
            //预加载手机号码
            QuickpassHandler.PreFetchNumber();
#endif
            SetLoginModeState();
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            EventCenter.inst.AddEventListener<string>((int)UIEventTag.EVENT_UI_DEBUG_TOGGLE_CHANGE, onCookieChange);
            EventCenter.inst.AddEventListener((int)UIEventTag.EVENT_UI_LOGIN_SUCCESS, OnEditorModeButtonClick);
        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            EventCenter.inst.RemoveEventListener<string>((int)UIEventTag.EVENT_UI_DEBUG_TOGGLE_CHANGE, onCookieChange);
            EventCenter.inst.RemoveEventListener((int)UIEventTag.EVENT_UI_LOGIN_SUCCESS, OnEditorModeButtonClick);

            base.OnClose(isShutdown, userData);
        }
        private void onCookieChange(string cookie)
        {
            Store.Config.CookieValue = cookie;
            Login.SyncCookie();
            //获取房间信息
            Login.GetRoomInfo(() =>
            {
                //获取房间信息后进入大厅
                OnEditorModeButtonClick();
            });
        }
        public void OnWechatButtonClick()
        {
            ssdk.Authorize(PlatformType.WeChat);
            Debug.Log("微信");

        }
        public void OnQQButtonClick()
        {
            ssdk.Authorize(PlatformType.QQ);

            Debug.Log("QQ");
        }

        public void OnMobileLoginButtonClick()
        {
            Close();
            GameEntry.UI.OpenUIForm(UIFormId.MobileLoginForm);
        }

        public void OnAppleButtonClick()
        {
            Debug.Log("Apple");
#if UNITY_IPHONE
            ssdk.Authorize(PlatformType.Apple);
#endif
        }
        public void OnQuickLoginButtonClick()
        {
            Debug.Log("OnPassLogin");
            QuickpassHandler.showQuickLogin();
        }
        public void OnEditorModeButtonClick()
        {
            Debug.Log("OnEditorModeButtonClick");
            GameEntry.Event.Fire(this, SwitchProcedureSuccessEventArgs.Create("ProcedureLogin", "ProcedureHome"));
        }
        public void OnTravellerButtonClick()
        {
            GameEntry.UI.OpenUIForm(UIFormId.DebugForm);
        }
        private void OnAuthResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
        {
            if (state == ResponseState.Success)
            {
                if (result != null && result.Count > 0)
                {
                    print("authorize success !" + "Platform :" + type + "result:" + MiniJSON.jsonEncode(result));
                }
                else
                {
                    print("authorize success !" + "Platform :" + type);
                }
#if UNITY_ANDROID
                Hashtable authInfo = ssdk.GetAuthInfo(type);
                Login.ThirdPartyLogin(type, authInfo);
#elif UNITY_IPHONE
                Login.ThirdPartyLogin(type, result);
#endif

            }
            else if (state == ResponseState.Fail)
            {
#if UNITY_ANDROID
                print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
                print("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
            }
            else if (state == ResponseState.Cancel)
            {
                print("cancel !");
            }
        }
        private void SetLoginModeState()
        {
            //临时设置为true
            bool isInEditorMode = true;
            bool isInQQMode = true;
            bool isInWechatMode = true;
            bool isInTravelMode = false;
            bool isInApple = false;
#if UNITY_EDITOR
            isInEditorMode = true;
            isInTravelMode = true;
            isInQQMode = false;
            isInWechatMode = false;

#elif UNITY_IPHONE || UNITY_IOS
            isInApple = false;
            isInTravelMode = false;
#endif
            AppleButton.gameObject.SetActive(isInApple);
            QQButton.gameObject.SetActive(isInQQMode);
            WeChatButton.gameObject.SetActive(isInWechatMode);
            TravelButton.gameObject.SetActive(isInTravelMode);
            EditorModeButton.gameObject.SetActive(isInEditorMode);
        }

    }


}
