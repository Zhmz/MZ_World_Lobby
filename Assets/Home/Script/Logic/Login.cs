//using StarForce;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using cn.sharesdk.unity3d;
using GameFramework.Event;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using Tuwan.Proto;
using Tuwan.Script.Entity;
using Tuwan;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Events;
using UnityGameFramework.Runtime;
using Tuwan.Const;

namespace Tuwan.Script.Logic
{
    public class Login
    {


        public static void RequestCode(long tel)
        {
            string json = HttpRequestUtil.GET(API.GET_KEY, "");
            LoginResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponse>(json);

            if (response.code == 1037)
            {
                string publicKey = (string)response.data;

                string encryptedData = HashEncrypt.RSAEncrypt("||tuwan|" + Newtonsoft.Json.JsonConvert.SerializeObject(new CodeRequest()
                {
                    token = "",
                    mobile = tel,
                    type = 1,
                    platform = 10,
                }), publicKey);

                json = HttpRequestUtil.POST(API.REQUEST_CODE, string.Format("data={0}", HttpUtility.UrlEncode(encryptedData)));

            }

        }

        public static bool SMSLogin(long tel, int code)
        {

            string json = HttpRequestUtil.GET(API.GET_KEY_LOGIN, "");
            LoginResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponse>(json);
            if (response.code == 1037)
            {
                string publicKey = (string)response.data;



                string encryptedData = HashEncrypt.RSAEncrypt("||tuwan|" + Newtonsoft.Json.JsonConvert.SerializeObject(new SMSLoginRequest()
                {
                    tel = tel,
                    code = code
                }), publicKey);


                json = HttpRequestUtil.POST(API.SMS_LOGIN, string.Format("data={0}", HttpUtility.UrlEncode(encryptedData)));

                response = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponse>(json);

                if (response.code == 0)
                {

                    List<Cookie> cookies = GetCookies(Const.API.TUWAN_COOKIES);

                    Cookie cookie = cookies.Where(c => c.Name == "Tuwan_Passport").FirstOrDefault();

                    if (cookie != null)
                    {
                        Store.Config.CookieValue = string.Format("{0}={1}", cookie.Name, cookie.Value);

                        SyncCookie();

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }


            }
            else
            {
                return false;
            }


        }

        public static bool OAuth(string openId, string unionId, string token, string userName, string userIcon, string appName)
        {
            Debug.Log("request:" + string.Format("openid={0}&unionid={1}&token={2}&name={3}&iconurl={4}&appname={5}&type={6}&env={7}", openId, unionId, token, userName, userIcon, appName, "applogin", Const.API.IS_DEV ? 1 : 0));

            string json = HttpRequestUtil.POST(API.OAUTH, string.Format("openid={0}&unionid={1}&token={2}&name={3}&iconurl={4}&appname={5}&type={6}&env={7}", openId, unionId, token, userName, userIcon, appName, "applogin", Const.API.IS_DEV ? 1 : 0));


            Debug.Log("json:" + json);

            LoginResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponse>(json);

            if (response.code == 0)
            {

                List<Cookie> cookies = GetCookies(API.TUWAN_COOKIES);

                Cookie cookie = cookies.Where(c => c.Name == "Tuwan_Passport").FirstOrDefault();

                if (cookie != null)
                {
                    Store.Config.CookieValue = string.Format("{0}={1}", cookie.Name, cookie.Value);

                    SyncCookie();

                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }


        public static void SyncCookie()
        {

            if (!string.IsNullOrEmpty(Store.Config.CookieValue))
            {


                try
                {
                    string[] cookieList = Store.Config.CookieValue.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string cookieStr in cookieList)
                    {
                        string[] cookie = cookieStr.Trim().Split('=');
                        API.TUWAN_COOKIES.Add(new Cookie(cookie[0], cookie[1], "/", ".tuwan.com"));
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                SaveCookie();
            }

        }


        private static void SaveCookie()
        {
            //Utils.ConfigFile.Write();

        }

        private static List<Cookie> GetCookies(CookieContainer cc)
        {
            List<Cookie> coo = new List<Cookie>();
            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance, null, cc, new object[] { });
            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies)
                    {
                        coo.Add(c);

                    }
            }
            return coo;
        }
        /// <summary>
        /// ThirdPartyLogin ，三方登录验证。
        /// </summary>
        /// <param name="type"></param> 登录平台
        /// <param name="authInfo"></param> 登录信息
        /// 
        public static void ThirdPartyLogin(PlatformType type, Hashtable authInfo)
        {
            string appName = "";
            string unionID = authInfo["unionID"]?.ToString();
            string openID = authInfo["openID"]?.ToString();
            string token = authInfo["token"]?.ToString();
            string userName = authInfo["userName"]?.ToString();
            string userIcon = authInfo["userIcon"]?.ToString();

#if UNITY_IPHONE
            unionID = authInfo["unionid"]?.ToString();
            openID = authInfo["openid"]?.ToString();
            Hashtable credential = (Hashtable)authInfo["credential"];
            token = credential["access_token"]?.ToString();
            userName = authInfo["nickname"]?.ToString();
            userIcon = authInfo["headimgurl"]?.ToString();
#endif
            string openId = !string.IsNullOrEmpty(unionID) ? unionID : openID;
            switch (type)
            {
                case PlatformType.WeChat:
                    appName = "weixin";
                    break;
                case PlatformType.QQ:
                    appName = "qq";
                    break;
                default:

                    break;
            }
            if (!string.IsNullOrEmpty(appName))
            {
                bool success = OAuth(openId, unionID, token, userName, userIcon, appName);
                if (success)
                {
                    GetRoomInfo(() =>
                    {
                        EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_LOGIN_SUCCESS);
                    });
                }
                Debug.Log("登录状态:" + success);
                Debug.Log("Cookie:" + Store.Config.CookieValue);
            }

        }
        /// <summary>
        /// GetRoomInfo，获取房间信息 ，建立socket连接。
        /// </summary>
        /// <param name="uriStr"></param>
        public static void GetRoomInfo(UnityAction callback = null)
        {
            string request = string.Format("&channel={0}", Store.Config.Cid);
            string json = HttpRequestUtil.GET(API.GET_ROOM_INFO, request);
            LoginRoomInfoResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginRoomInfoResponse>(json);
            if (response.error == 0)
            {
                LoginRoomInfoData data = response.data;
                string url = "wss://" + data.SocketDomain + ":" + data.SocketPort;
                Store.Config.RoomInfo = response.data;
                if (callback != null)
                {
                    callback();
                }
                GetUserInfo();
                GetMoney();
                NetManager.inst.Connect(url);
            }
        }
        /// <summary>
        /// GetRoomInfo，获取个人信息 ，初始化个人数据。
        /// </summary>
        public static void GetUserInfo()
        {
            string request = string.Format("&uids={0}", Store.Config.RoomInfo.Uid);
            string json = HttpRequestUtil.GET(API.GET_USER_INFO, request);
            UserInfoResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<UserInfoResponse>(json);
            if (response.error == 0)
            {
                List<UserInfoResponsedData> data = response.data;
                if (data.Count > 0)
                {
                    Store.Config.UserInfo = data[0];
                }
                else
                {
                    Debug.LogError("用户信息错误");
                }
            }

        }
        /// <summary>
        /// GetRoomInfo，获取个人信息 ，初始化个人数据。
        /// </summary>
        public static void GetMoney(UnityAction callback = null)
        {
            string json = HttpRequestUtil.GET(API.GET_MONEY, "");
            MoneyResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<MoneyResponse>(json);
            Store.Config.MoneyInfo = response;
            EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_DIAMOND_CHANGE);
        }
        /// <summary>
        /// onLogin wss登录认证。
        /// </summary>
        public static void OnLogin()
        {
            LoginSignRequest jsonStr = new LoginSignRequest()
            {
                room = 1,
                uid = Store.Config.RoomInfo.Uid,
                time = Store.Config.RoomInfo.Time,
                token = Store.Config.RoomInfo.SocketToken,
                ver = 1,
                data_statistics = new LoginPlatform() { platform = 1 },
            };
            NetManager.inst.Emit(SocketResquestName.Login, jsonStr);
        }
        /// <summary>
        /// onLogin 大厅设置频道。
        /// </summary>
        public static void SetChannel()
        {
            string[] methodList = { "setchannel", "wheatlist" };
            NetManager.inst.Emit(SocketResquestName.InitList, new
            {
                cid = Store.Config.Cid,
                uid = Store.Config.RoomInfo.Uid,
                typeid = Store.Config.RoomInfo.Typeid,
                method = methodList,
            });
        }

    }
}
