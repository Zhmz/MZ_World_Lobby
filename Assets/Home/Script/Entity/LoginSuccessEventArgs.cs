using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuwan.Script.Entity
{
    public class LoginSuccessEventArgs : EventArgs
    {
        private string _cookie;
        public string Cookie
        {
            get
            {
                return this._cookie;
            }
        }

        public LoginSuccessEventArgs(string key, string value)
        {
            this._cookie = string.Format("{0}={1}", key, value);
        }


    }
}
