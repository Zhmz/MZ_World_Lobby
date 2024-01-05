
using Tuwan.Const;
using Tuwan.Lobby.Entity;
using Tuwan.Proto;
using Tuwan.Script.Logic;
using UnityEngine;


namespace Tuwan.Lobby.Logic
{
    public class GiftBag
    {
        public static GiftResponse RequestGiftList()
        {
            string request = string.Format("&cid={0}", Store.Config.Cid);
            string json = HttpRequestUtil.GET(API.GIFT_BOX_GIFT_LIST, request);
            GiftResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<GiftResponse>(json);
            if (response.error == 0)
            {
                return response;
            }
            return null;
        }
        public static void SendGift(int giftid, int num, string uids)
        {
            string request = string.Format("&cid={0}&giftid={1}&id={2}&num={3}&uids={4}", Store.Config.Cid, giftid, giftid, num, uids);
            string json = HttpRequestUtil.GET(API.SEND_GIFT, request);
            SendGiftResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<SendGiftResponse>(json);
            if (response.error == 0)
            {
                Login.GetMoney();
            }
            else
            {
                GameEntry.UI.OpenDialog(new DialogParams()
                {
                    Mode = 1,
                    Title = "提示",
                    Message = response.error_msg
                });
            }

        }
    }
}
