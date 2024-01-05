
using Tuwan.Const;
using Tuwan.Lobby.Entity;
using Tuwan.Proto;
using Tuwan.Script.Logic;
using UnityEngine;


namespace Tuwan.Lobby.Logic
{
    public class WheatCtrl
    {
        //申请上麦
        public static void ApplyWheat()
        {
            string cookie = Store.Config.CookieValue.Replace("Tuwan_Passport=", "");
            NetManager.inst.Emit(SocketResquestName.Setorder, new
            {
                state = 0,
                passport = cookie,
                typeid = Store.Config.RoomInfo.Typeid,
                uid = Store.Config.UserInfo.uid,
                cid = Store.Config.Cid,
                channel = Store.Config.RoomInfo.Channel,
            });
        }
        //拒绝上麦
        public static void UnAgreeWheat(int toUid)
        {
            string cookie = Store.Config.CookieValue.Replace("Tuwan_Passport=", "");
            NetManager.inst.Emit(SocketResquestName.Setorder, new
            {
                state = -1,
                passport = cookie,
                typeid = Store.Config.RoomInfo.Typeid,
                uid = toUid,
                cid = Store.Config.Cid,
                channel = Store.Config.RoomInfo.Channel,
            });
        }
        //上麦
        public static void AgreeWheat(int toUid, int pos = 3)
        {
            NetManager.inst.Emit(SocketResquestName.Setwheat, new
            {
                state = 0,
                typeid = Store.Config.RoomInfo.Typeid,
                uid = toUid,
                cid = Store.Config.Cid,
                position = pos,
            });
        }
        //下麦
        public static void DownWheat()
        {
            NetManager.inst.Emit(SocketResquestName.Setwheat, new
            {
                state = -1,
                uid = Store.Config.UserInfo.uid,
                cid = Store.Config.Cid,
                position = 0,
            });
        }
        //获取排麦列表
        public static void GetOrderList()
        {
            if (LobbyData.inst.JudgeSelfHoster())
            {
                string request = string.Format("cid={0}&format=json", Store.Config.Cid);
                string json = HttpRequestUtil.GET(API.GET_ORDERLIST, request);
                SocketResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<SocketResponse>(json);
                SocketManger.inst.onMessage(response);
            }

        }


    }
}
