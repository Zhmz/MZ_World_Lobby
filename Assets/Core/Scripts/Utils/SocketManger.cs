using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using Tuwan;
using Tuwan.Const;
using Tuwan.Proto;
using UnityEngine;
using UnityEngine.UI;
public class SocketManger : MonoBehaviour
{
    private volatile static SocketManger m_instance;
    //线程锁。当多线程访问时，同一时刻仅允许一个线程访问
    private static object m_locker = new object();

    public Queue<SocketResponse> receiveMsg = new Queue<SocketResponse>();
    private void Awake()
    {
        m_instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //单例初始化
    public static SocketManger inst
    {
        get
        {
            //线程锁。防止同时判断为null时同时创建对象
            return m_instance;
        }
    }
    private void Update()
    {
        if (receiveMsg.Count > 0)
        {
            SocketResponse response = receiveMsg.Dequeue();
            onMessage(response);
        }
    }
    public void onMessage(SocketResponse response)
    {
        switch (response.TypeId)
        {
            case (int)SocketIoType.LOGIN:
                Tuwan.Script.Logic.Login.SetChannel();
                break;
            case (int)SocketIoType.WHEATLIST:
                LobbyData.inst.initWheatInfo(response.data.ToString());
                EventCenter.inst.EventSocketTrigger(response.TypeId, response.type, response.data.ToString());
                break;
            case (int)SocketIoType.APPLY_WHEAT:
                if (response.error == 0)
                {
                    EventCenter.inst.EventSocketTrigger(response.TypeId, response.uid, response.data.ToString());
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
                break;
            default:
                if (response.data != null)
                {
                    EventCenter.inst.EventSocketTrigger(response.TypeId, response.type, response.data.ToString());
                }
                break;

        }
    }
}

