using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GameFramework.Event;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using Tuwan.Const;
using Tuwan.Proto;
using UnityEngine;
namespace Tuwan
{
    public class NetManager
    {
        //单例本身。使用volatile关键字修饰，禁止优化，确保多线程访问时访问到的数据都是最新的
        private volatile static NetManager m_instance;
        //线程锁。当多线程访问时，同一时刻仅允许一个线程访问
        private static object m_locker = new object();
        //客户端webSocket
        private SocketIOUnity socket;
        //私有化构造
        private NetManager() { }
        //单例初始化
        public static NetManager inst
        {
            get
            {
                //线程锁。防止同时判断为null时同时创建对象
                lock (m_locker)
                {
                    if (m_instance == null)
                    {
                        m_instance = new NetManager();
                    }
                }
                return m_instance;
            }
        }
        /// <summary>
        /// Connect 与服务器建立连接。
        /// </summary>
        /// <param name="uriStr"></param>
        public void Connect(string uriStr)
        {
            var uri = new Uri(uriStr);
            socket = new SocketIOUnity(uri, new SocketIOOptions
            {
                EIO = 3,
                Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
            });
            socket.JsonSerializer = new NewtonsoftJsonSerializer();
            socket.OnConnected += (sender, e) =>
            {
                onConnected();
            };
            socket.OnDisconnected += (sender, e) =>
            {
                onDisConnected();
            };
            socket.OnReconnectAttempt += (sender, e) =>
            {
                Debug.Log($"{DateTime.Now} Reconnecting: attempt = {e}");
            };
            socket.OnAnyInUnityThread((name, response) =>
            {
                string json = response.GetValue().GetRawText();
                Debug.Log("Received On " + name + " : " + Regex.Unescape(json).Trim('"'));
            });
            socket.OnPing += (sender, e) =>
            {
                onPing();
            };
            socket.On("new_msg", (response) =>
            {
                SocketResponse obj = response.GetValue<SocketResponse>();
                if (obj.error_msg != null)
                {
                    Debug.LogError("error_msg : " + Regex.Unescape(obj.error_msg));
                }
                SocketManger.inst.receiveMsg.Enqueue(obj);
            });
            socket.Connect();

        }
        /// <summary>
        /// onConnected 连接成功回调。
        /// </summary>
        private void onConnected()
        {
            Debug.Log("socket.OnConnected");
            Tuwan.Script.Logic.Login.OnLogin();
        }
        /// <summary>
        /// onDisConnected 断开连接回调。
        /// </summary>
        private void onDisConnected()
        {
            Debug.Log("socket.OnDisConnected");
        }
        /// <summary>
        /// onPing Ping值回调。
        /// </summary>
        private void onPing()
        {
        }
        /// <summary>
        /// DisConnect 关闭socketIO。
        /// </summary>
        public void DisConnect()
        {
            socket.Disconnect();
        }
        /// <summary>
        /// Emit 发送消息。
        /// </summary>
        public void Emit(string eventName, object data)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            Debug.Log("socket.request:" + eventName + "===" + json);
            socket.EmitStringAsJSON(eventName, json);
        }

    }
}
