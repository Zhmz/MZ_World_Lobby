using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

namespace Tuwan
{
    public class SwitchProcedureSuccessEventArgs : GameEventArgs
    {
        /// <summary>
        /// 加载数据表成功事件编号。
        /// </summary>
        public static readonly int EventId = typeof(SwitchProcedureSuccessEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public string PreProcedure
        {
            get;
            private set;
        }

        public string CurProcedure
        {
            get;
            private set;
        }

        public override void Clear()
        {
            PreProcedure = string.Empty;
            CurProcedure = string.Empty;
        }

        public static SwitchProcedureSuccessEventArgs Create(string pre,string cur)
        {
            SwitchProcedureSuccessEventArgs eventArgs = ReferencePool.Acquire<SwitchProcedureSuccessEventArgs>();
            eventArgs.PreProcedure = pre;
            eventArgs.CurProcedure = cur;
            return eventArgs;
        }
    }
}
