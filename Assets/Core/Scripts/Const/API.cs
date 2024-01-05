using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Tuwan.Const
{
    public static class API
    {


        public const bool IS_DEV = true;

        public static CookieContainer TUWAN_COOKIES = new CookieContainer();

        const string suffix = IS_DEV ? "-test" : "";

        public const string yapiDomain = "https://yapi" + suffix + ".tuwan.com";
        public const string papiDomain = "https://papi" + suffix + ".tuwan.com";
        public const string ylocalDomain = "https://ylocal" + suffix + ".tuwan.com";

        public const string OAUTH = "https://open.tuwan.com/api/umeng/callback.ashx";

        public const string REQUEST_CODE = "https://passport.tuwan.com/Auth/requestCode?format=json";

        public const string GET_KEY = "https://passport.tuwan.com/Auth/getKey?format=json";

        public const string GET_KEY_LOGIN = "https://user.tuwan.com/api/method/getpkey";

        public const string SMS_LOGIN = "https://user.tuwan.com/api/method/smslogin";

        public const string UPDATE_INFO = "https://apk.tuwan.com/app/game/update.json";
        // 字段为img，值为base64
        public const string UPLOAD_IMG = "https://yapi.tuwan.com/User/uploadImage?format=json";


        public const string GET_GAMEDATA = yapiDomain + "/match/getGameData?format=json&game_id={0}&platformid={1}";

        public const string LOL_DATA_REPORT = yapiDomain + "/match/report?type={0}&accountid={1}&platformid={2}";


        public const string GET_OSS_TOKEN = "https://lolrep.tuwan.com/api/sts";

        public const string CRASH_REPORT = "https://app.tuwan.com/Entrance/crashreport/?platform=dotnet";
        public const string GET_ROOM_INFO = papiDomain + "/Agora/webinfo?apiver=3&format=json";
        public const string GIFT_BOX_GIFT_LIST = papiDomain + "/Teacher/giftLists?format=json&apiver=2&platform=3";
        public const string GET_USER_INFO = papiDomain + "/Chatroom/getUserInfo?format=json&requestfrom=selflogin";
        public const string GET_MONEY = papiDomain + "/Teacher/getMoney?format=json";
        public const string SEND_GIFT = papiDomain + "/Diamond/giftMore?format=json&actid=100&platform=0&anonymous=0&question_gift=0&question=&open=0";
        public const string BUY_GIFT = papiDomain + "/Treasure/buy?format=json&actid=100&platform=0&anonymous=0&question_gift=0&question=&open=0";
        public const string GET_ORDERLIST = ylocalDomain + "/Chatroom/getOrderList";


    }
}
