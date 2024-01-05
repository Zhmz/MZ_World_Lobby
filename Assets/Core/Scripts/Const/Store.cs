using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tuwan.Const
{
    public static class Store
    {
        public const string ConfigFileName = "config.cfg";

        public static Config Config = new Config();

        public static string FingerPrint = "";

        public static string MacAddress = "";

        public static Dictionary<string, string> UserAvatarUrls = new Dictionary<string, string>
        {
            { "#u1#", "https://img3.tuwandata.com/uploads/repayment/" },
            { "#u2#", "https://res.tuwan.com/templet/play/images/app/" },
            { "#u3#", "https://img3.tuwandata.com/uploads/play/" },
            { "#u4#", "https://ucavatar.tuwan.com/data/avatar/" },
            { "#u5#", "https://ucavatar2.tuwan.com/data/avatar/" },
            { "#u6#", "https://ucavatar3.tuwan.com/data/avatar/" },
            { "#u7#", "https://img3.tuwandata.com/uploads/play2/banner/" },
            { "#u8#", "https://img3.tuwandata.com/uploads/chatroom/" },
            { "#u9#", "https://img1.tuwandata.com/im/" },
            { "#u10#", "https://ucavatar1.tuwan.com/data/avatar/" }
        };

        public static List<Dictionary<string, string>> BgmList = new List<Dictionary<string, string>>
        {
        new Dictionary<string, string>{{"path","Bgm/bgm1"},{"musicName","Foksya-Detective"}},
        new Dictionary<string, string>{{"path","Bgm/bgm2"},{"musicName","叮叮当当"}},
        new Dictionary<string, string>{{"path","Bgm/bgm3"},{"musicName","凌晨两点半"}},
        new Dictionary<string, string>{{"path","Bgm/bgm4"},{"musicName","我是真的真的爱你"}},
        new Dictionary<string, string>{{"path","Bgm/bgm5"},{"musicName","下雨天(韩文版Live)"}},
        new Dictionary<string, string>{{"path","Bgm/bgm6"},{"musicName","多幸运"}},
        new Dictionary<string, string>{{"path","Bgm/bgm7"},{"musicName","遇上你是我的缘"}},
        };
    }
}
