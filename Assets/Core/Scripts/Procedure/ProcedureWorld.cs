using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using GameFramework.Event;
using GameFramework.Fsm;
using Tuwan.Const;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Tuwan
{
    public class ProcedureWorld : ProcedureBase
    {
        IFsm<GameFramework.Procedure.IProcedureManager> m_ProcedureOwner;
        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }

        protected override void OnEnter(IFsm<GameFramework.Procedure.IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_ProcedureOwner = procedureOwner;

            GameEntry.Event.Subscribe(SwitchProcedureSuccessEventArgs.EventId, OnSwitchProcedureSuccess);

            LoadWorldScene();
        }

        protected override void OnLeave(IFsm<GameFramework.Procedure.IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            GameEntry.Event.Unsubscribe(SwitchProcedureSuccessEventArgs.EventId, OnSwitchProcedureSuccess);
        }

        protected override void OnUpdate(IFsm<GameFramework.Procedure.IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }

        void LoadWorldScene()
        {
            TuwanSceneUtils.LoadScene("World");
        }

        void OnSwitchProcedureSuccess(object sender, GameEventArgs e)
        {
            SwitchProcedureSuccessEventArgs switchProcedureEvent = (SwitchProcedureSuccessEventArgs)e;
            string curProcedureName = switchProcedureEvent.CurProcedure;
            switch (curProcedureName)
            {
                case "ProcedureLobby":
                    ChangeState<ProcedureLobby>(m_ProcedureOwner);
                    break;
                case "ProcedureHome":
                    ChangeState<ProcedureHome>(m_ProcedureOwner);
                    break;
            }
        }
    }
}