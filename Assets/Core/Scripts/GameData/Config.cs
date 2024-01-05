using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tuwan.Proto;

namespace Tuwan
{
    public class Config
    {
        public string CookieValue { get; set; }
        public LoginRoomInfoData RoomInfo { get; set; }
        public UserInfoResponsedData UserInfo { get; set; }
        public MoneyResponse MoneyInfo { get; set; }
        public int Cid = 41607;
        public bool AutoLogin { get; set; }



    }
}
