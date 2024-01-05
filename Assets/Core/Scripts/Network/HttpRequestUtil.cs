using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


namespace Tuwan
{
    public class HttpRequestUtil
    {

        public static bool SecurityValidate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;//直接确认，不然打不开，会出现超时错误
        }

        public static string GET(string requestUrl, string paramData = "")
        {
            return GET(requestUrl, paramData, Encoding.UTF8, ref Const.API.TUWAN_COOKIES);
        }


        public static string GET(string requestUrl, string paramData, WebHeaderCollection headers)
        {
            return GET(requestUrl, paramData, Encoding.UTF8, headers);
        }

        public static string GET(string requestUrl, string paramData, Encoding dataDecode, WebHeaderCollection headers)
        {

            return GET(requestUrl, paramData, dataDecode, ref Const.API.TUWAN_COOKIES, headers);
        }

        public static string GET(string requestUrl, string paramData, Encoding dataDecode, ref CookieContainer cc)
        {
            return GET(requestUrl, paramData, dataDecode, ref cc, new WebHeaderCollection());
        }

        public static string GET(string requestUrl, string paramData, Encoding dataDecode, ref CookieContainer cc, WebHeaderCollection headers)
        {
            int errCount = 0;

            ServicePointManager.DefaultConnectionLimit = Int32.MaxValue;
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(SecurityValidate);

        RETRY:

            string ret = string.Empty;
            try
            {
                string addStr = "";
                if (!requestUrl.Contains("?") && paramData != "")
                {
                    addStr = "?";
                }
                string reqURL = requestUrl + addStr + paramData;

                Debug.Log("reqURL===" + reqURL);
                //byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(reqURL));
                webReq.Method = "GET";
                webReq.Proxy = null;
                webReq.AllowAutoRedirect = true;
                webReq.Accept = "application/json";
                webReq.KeepAlive = true;
                webReq.CookieContainer = InitCookie(cc, new Uri(reqURL).Host);


                webReq.Headers.Add("clientid", Const.Store.FingerPrint);
                webReq.Headers.Add("appcode", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
                //webReq.Headers.Add("appcode", "1.0.0");
                webReq.Headers.Add("platform", "6");
                webReq.Headers.Add("phonemodel", "lolhelp");
                webReq.Headers.Add("clientmac", Const.Store.MacAddress);

                webReq.Headers.Add(headers);

                //foreach (var header in headers)
                //{

                //    webReq.Headers.Add(header.Key, header.Value);

                //}

                //webReq.ContentType = "application/x-www-form-urlencoded";

                //webReq.ContentLength = byteArray.Length;
                //Stream newStream = webReq.GetRequestStream();
                //newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                //newStream.Close();
                using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
                {
                    foreach (Cookie c in response.Cookies)
                    {
                        cc.Add(c);
                    }

                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), dataDecode))
                    {
                        ret = sr.ReadToEnd();
                        sr.Close();
                    }

                    response.Close();
                }

            }
            catch (WebException ex)
            {
                using (HttpWebResponse response = (HttpWebResponse)ex.Response)
                {

                    if ((int)response.StatusCode == 429)
                    {

                        errCount++;

                        if (!string.IsNullOrEmpty(response.GetResponseHeader("Retry-After")))
                        {
                            try
                            {

                                int interval = 0;

                                if (!int.TryParse(response.GetResponseHeader("Retry-After"), out interval))
                                {
                                    interval = (DateTime.Parse(response.GetResponseHeader("Retry-After")) - DateTime.Now).Milliseconds;
                                }
                                else
                                {
                                    interval = interval * 1000;
                                }


                                Thread.Sleep(interval);

                                if (errCount < 3)
                                {

                                    goto RETRY;
                                }
                            }
                            catch
                            {

                            }


                        }
                        else if (!string.IsNullOrEmpty(response.GetResponseHeader("Ratelimit-Reset")))
                        {
                            try
                            {
                                int destTimestamp;

                                if (int.TryParse(response.GetResponseHeader("Ratelimit-Reset"), out destTimestamp))
                                {

                                    long epoch = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;

                                    int interval = (int)(destTimestamp - epoch);

                                    interval = interval * 1000;

                                    if (interval > 0)
                                    {
                                        Thread.Sleep(interval);
                                    }

                                    if (errCount < 3)
                                    {

                                        goto RETRY;
                                    }
                                }
                            }
                            catch
                            {

                            }
                        }

                    }

                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), dataDecode))
                    {
                        ret = sr.ReadToEnd();
                        sr.Close();
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return ret;
        }


        //public static byte[] Download(string reqURL)
        //{


        //    //byte[] byteArray = dataEncode.GetBytes(paramData); //转化
        //    HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(reqURL));
        //    webReq.Method = "GET";

        //    //webReq.CookieContainer = InitCookie(cc, new Uri(reqURL).Host);

        //    //webReq.ContentType = "application/x-www-form-urlencoded";

        //    //webReq.ContentLength = byteArray.Length;
        //    //Stream newStream = webReq.GetRequestStream();
        //    //newStream.Write(byteArray, 0, byteArray.Length);//写入参数
        //    //newStream.Close();
        //    using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
        //    {

        //        using (Stream resstream = response.GetResponseStream())
        //        {


        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                IoUtils.WriteTo(resstream, ms);

        //                return ms.ToArray();
        //            }


        //        }

        //    }


        //}


        //public static byte[] Download(string reqURL,string paramData)
        //{

        //    byte[] byteArray = Encoding.UTF8.GetBytes(paramData); //转化
        //    HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(reqURL));
        //    webReq.Method = "POST";


        //    webReq.ContentType = "application/json";


        //    webReq.ContentLength = byteArray.Length;

        //    using (Stream newStream = webReq.GetRequestStream())
        //    {
        //        newStream.Write(byteArray, 0, byteArray.Length);//写入参数
        //        newStream.Close();
        //    }

        //    using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
        //    {

        //        using (Stream resstream = response.GetResponseStream())
        //        {


        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                IoUtils.WriteTo(resstream, ms);

        //                return ms.ToArray();
        //            }


        //        }

        //    }


        //}

        public static Task<string> POSTAsync(string requestUrl, object paramData)
        {
            return Task.Run(() => { return POST(requestUrl, paramData); });

        }

        public static string POST(string requestUrl, object paramData)
        {
            return POST(requestUrl, Newtonsoft.Json.JsonConvert.SerializeObject(paramData), "application/x-www-form-urlencoded");
        }

        public static string POST(string requestUrl, string paramData)
        {
            return POST(requestUrl, paramData, "application/x-www-form-urlencoded");
        }

        public static string POST(string requestUrl, string paramData, string contentType)
        {

            return POST(requestUrl, paramData, contentType, Encoding.UTF8, ref Const.API.TUWAN_COOKIES);
        }


        public static string POST(string requestUrl, string paramData, string contentType, Encoding dataDecode, ref CookieContainer cc)
        {
            string ret = string.Empty;
            try
            {

                string reqURL = requestUrl;

                byte[] byteArray = dataDecode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(requestUrl));
                webReq.Method = "POST";

                webReq.CookieContainer = InitCookie(cc, new Uri(reqURL).Host);

                webReq.Headers.Add("clientid", Const.Store.FingerPrint);
                webReq.Headers.Add("appcode", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
                webReq.Headers.Add("platform", "6");
                webReq.Headers.Add("phonemodel", "lolhelp");
                webReq.Headers.Add("clientmac", Const.Store.MacAddress);


                webReq.ContentType = contentType;

                webReq.ContentLength = byteArray.Length;
                using (Stream newStream = webReq.GetRequestStream())
                {
                    newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                    newStream.Close();
                }

                using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
                {

                    foreach (Cookie c in response.Cookies)
                    {
                        cc.Add(c);
                    }

                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), dataDecode))
                    {
                        ret = sr.ReadToEnd();
                        sr.Close();
                    }

                    response.Close();
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return ret;
        }




        public static string POST(string requestUrl, string paramData, Encoding dataDecode, WebHeaderCollection headers)
        {
            string ret = string.Empty;
            try
            {
                //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

                string reqURL = requestUrl;// + "?" + paramData;

                byte[] byteArray = dataDecode.GetBytes(paramData); //转化


                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(reqURL));
                webReq.Method = "POST";

                // webReq.AllowAutoRedirect = true;

                webReq.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

                webReq.Headers.Add("clientid", Const.Store.FingerPrint);
                webReq.Headers.Add("appcode", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
                webReq.Headers.Add("platform", "6");
                webReq.Headers.Add("phonemodel", "lolhelp");
                webReq.Headers.Add("clientmac", Const.Store.MacAddress);

                //    webReq.ContentLength = byteArray.Length;

                webReq.KeepAlive = true;

                webReq.Headers.Add(headers);

                using (Stream newStream = webReq.GetRequestStream())
                {
                    newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                    newStream.Close();
                }

                using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), dataDecode))
                    {
                        ret = sr.ReadToEnd();
                        sr.Close();
                    }

                    response.Close();
                }

            }
            catch (WebException ex)
            {
                using (HttpWebResponse response = (HttpWebResponse)ex.Response)
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), dataDecode))
                    {
                        ret = sr.ReadToEnd();

                        sr.Close();

                        return ret;
                    }

                    response.Close();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                //MessageBox.Show(ex.Message);
            }
            return ret;
        }

        private static CookieContainer InitCookie(CookieContainer cc, string Domain)
        {
            CookieContainer coo = new CookieContainer();

            try
            {
                Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance, null, cc, new object[] { });
                foreach (object pathList in table.Values)
                {
                    SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                    foreach (CookieCollection colCookies in lstCookieCol.Values)
                        foreach (Cookie c in colCookies)
                        {
                            try
                            {
                                c.Domain = Domain;
                                coo.Add(c);
                            }
                            catch
                            {

                            }


                        }
                }
            }
            catch
            {

            }


            return coo;
        }



    }
}
