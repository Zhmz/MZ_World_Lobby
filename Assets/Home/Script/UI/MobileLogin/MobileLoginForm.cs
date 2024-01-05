using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Tuwan.Script.UI.MobileLogin
{
    public class MobileLoginForm :  UGuiForm
    {

        public void OnAuthCodeClick()
        {


            string telNum = this.GetComponentsInChildren<InputField>().Where(c => c.name == "TelNum").FirstOrDefault()?.text;


            Debug.Log("»ñÈ¡ÑéÖ¤Âë:" + telNum);

            if (long.TryParse(telNum, out long tel))
            {

                Logic.Login.RequestCode(tel);
            }

        }


        public void OnMobileLoginClick()
        {
            Debug.Log("¶ÌÐÅÑéÖ¤ÂëµÇÂ¼");

            string telNum = this.GetComponentsInChildren<InputField>().Where(c => c.name == "TelNum").FirstOrDefault()?.text;

            string authCode = this.GetComponentsInChildren<InputField>().Where(c => c.name == "AuthCode").FirstOrDefault()?.text;

            Debug.Log("µÇÂ¼:" + authCode);

            if (long.TryParse(telNum, out long tel) && int.TryParse(authCode, out int code))
            {
                bool success = Logic.Login.SMSLogin(tel, code);

                Debug.Log("µÇÂ¼×´Ì¬:" + success);
                Debug.Log("Cookie:" + Const.Store.Config.CookieValue);

                Text resultText = this.GetComponentsInChildren<Text>().Where(c => c.name == "Result").FirstOrDefault();

                resultText.text = "µÇÂ¼×´Ì¬:" + success + "\r\n"+ "Cookie:" + Const.Store.Config.CookieValue;

            }
        }

        public void OnGoBackClick()
        {

            this.Close();

            GameEntry.UI.OpenUIForm(UIFormId.LoginForm);
        }
    }

}
