using System.Collections.Generic;
using UnityEngine.Events;

namespace GameFramework.Event
{
    public interface IEventInfo
    {

    }

    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;

        public EventInfo(UnityAction<T> action)
        {
            actions += action;
        }
    }

    public class EventInfo<T0, T1> : IEventInfo
    {
        public UnityAction<T0, T1> actions;

        public EventInfo(UnityAction<T0, T1> action)
        {
            actions += action;
        }
    }

    public class EventInfoSocket<T, K> : IEventInfo
    {
        public UnityAction<T, K> actions;

        public EventInfoSocket(UnityAction<T, K> action)
        {
            actions += action;
        }
    }

    public class EventInfo : IEventInfo
    {
        public UnityAction actions;

        public EventInfo(UnityAction action)
        {
            actions += action;
        }
    }


    /// <summary>
    /// 事件中心 单例模式对象
    /// 1.Dictionary
    /// 2.委托
    /// 3.观察者设计模式
    /// 4.泛型
    /// </summary>
    public class EventCenter
    {
        private volatile static EventCenter m_instance;
        //线程锁。当多线程访问时，同一时刻仅允许一个线程访问
        private static object m_locker = new object();
        //私有化构造
        private EventCenter() { }
        //单例初始化
        public static EventCenter inst
        {
            get
            {
                //线程锁。防止同时判断为null时同时创建对象
                lock (m_locker)
                {
                    if (m_instance == null)
                    {
                        m_instance = new EventCenter();
                    }
                }
                return m_instance;
            }
        }
        //key —— 事件的名字（
        //value —— 对应的是 监听这个事件 对应的委托函数们
        private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

        /// <summary>
        /// 添加事件监听
        /// </summary>
        /// <param name="name">事件的名字</param>
        /// <param name="action">准备用来处理事件 的委托函数</param>
        public void AddEventListener<T>(int eventId, UnityAction<T> action)
        {
            string name = eventId.ToString();
            //有没有对应的事件监听
            //有的情况
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T>).actions += action;
            }
            //没有的情况
            else
            {
                eventDic.Add(name, new EventInfo<T>(action));
            }
        }

        public void AddEventListener<T0, T1>(int eventId, UnityAction<T0, T1> action)
        {
            string name = eventId.ToString();
            //有没有对应的事件监听
            //有的情况
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T0, T1>).actions += action;
            }
            //没有的情况
            else
            {
                eventDic.Add(name, new EventInfo<T0, T1>(action));
            }
        }

        public void AddSocketEventListener<T, K>(int typeId, UnityAction<T, K> action)
        {
            string name = "SOCKET_RESPONSE_" + typeId.ToString();
            //有没有对应的事件监听
            //有的情况
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfoSocket<T, K>).actions += action;
            }
            //没有的情况
            else
            {
                eventDic.Add(name, new EventInfoSocket<T, K>(action));
            }
        }
        /// <summary>
        /// 监听不需要参数传递的事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public void AddEventListener(int eventId, UnityAction action)
        {
            string name = eventId.ToString();
            //有没有对应的事件监听
            //有的情况
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo).actions += action;
            }
            //没有的情况
            else
            {
                eventDic.Add(name, new EventInfo(action));
            }
        }


        /// <summary>
        /// 移除对应的事件监听
        /// </summary>
        /// <param name="name">事件的名字</param>
        /// <param name="action">对应之前添加的委托函数</param>
        public void RemoveEventListener<T>(int eventId, UnityAction<T> action)
        {
            string name = eventId.ToString();
            if (eventDic.ContainsKey(name))
                (eventDic[name] as EventInfo<T>).actions -= action;
        }

        public void RemoveEventListener<T0, T1>(int eventId, UnityAction<T0, T1> action)
        {
            string name = eventId.ToString();
            if (eventDic.ContainsKey(name))
                (eventDic[name] as EventInfo<T0, T1>).actions -= action;
        }

        public void RemoveSocketEventListener<T, K>(int typeId, UnityAction<T, K> action)
        {
            string name = "SOCKET_RESPONSE_" + typeId.ToString();
            if (eventDic.ContainsKey(name))
                (eventDic[name] as EventInfoSocket<T, K>).actions -= action;
        }

        /// <summary>
        /// 移除不需要参数的事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public void RemoveEventListener(int eventId, UnityAction action)
        {
            string name = eventId.ToString();
            if (eventDic.ContainsKey(name))
                (eventDic[name] as EventInfo).actions -= action;
        }
        public void RemoveSocketEventListener(int typeId, UnityAction action)
        {
            string name = "SOCKET_RESPONSE_" + typeId.ToString();
            if (eventDic.ContainsKey(name))
                (eventDic[name] as EventInfo).actions -= action;
        }

        /// <summary>
        /// 事件触发
        /// </summary>
        /// <param name="name">哪一个名字的事件触发了</param>
        public void EventTrigger<T>(int eventId, T info)
        {
            string name = eventId.ToString();
            //有没有对应的事件监听
            //有的情况
            if (eventDic.ContainsKey(name))
            {
                //eventDic[name]();
                if ((eventDic[name] as EventInfo<T>).actions != null)
                    (eventDic[name] as EventInfo<T>).actions.Invoke(info);
                //eventDic[name].Invoke(info);
            }
        }

        public void EventTrigger<T0, T1>(int eventId, T0 value0, T1 value1)
        {
            string name = eventId.ToString();
            //有没有对应的事件监听
            //有的情况
            if (eventDic.ContainsKey(name))
            {
                //eventDic[name]();
                if ((eventDic[name] as EventInfo<T0, T1>).actions != null)
                    (eventDic[name] as EventInfo<T0, T1>).actions.Invoke(value0, value1);
                //eventDic[name].Invoke(info);
            }
        }

        public void EventSocketTrigger<T, K>(int typeId, T type, K info)
        {
            string name = "SOCKET_RESPONSE_" + typeId.ToString();
            //有没有对应的事件监听
            //有的情况
            if (eventDic.ContainsKey(name))
            {
                //eventDic[name]();
                if ((eventDic[name] as EventInfoSocket<T, K>).actions != null)
                    (eventDic[name] as EventInfoSocket<T, K>).actions.Invoke(type, info);
                //eventDic[name].Invoke(info);
            }
        }

        /// <summary>
        /// 事件触发（不需要参数的）
        /// </summary>
        /// <param name="name"></param>
        public void EventTrigger(int eventId)
        {
            string name = eventId.ToString();
            //有没有对应的事件监听
            //有的情况
            if (eventDic.ContainsKey(name))
            {
                //eventDic[name]();
                if ((eventDic[name] as EventInfo).actions != null)
                    (eventDic[name] as EventInfo).actions.Invoke();
                //eventDic[name].Invoke(info);
            }
        }

        /// <summary>
        /// 清空事件中心
        /// 主要用在 场景切换时
        /// </summary>
        public void Clear()
        {
            eventDic.Clear();
        }
    }

}
