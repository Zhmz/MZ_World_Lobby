using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using GameFramework.Fsm;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityGameFramework.Runtime;

namespace Tuwan
{
    public class ProcedureHome : ProcedureBase
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
            GameEntry.Event.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);

            LoadHomeScene();
        }

        protected override void OnLeave(IFsm<GameFramework.Procedure.IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            GameEntry.Event.Unsubscribe(SwitchProcedureSuccessEventArgs.EventId, OnSwitchProcedureSuccess);
            GameEntry.Event.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
        }

        protected override void OnUpdate(IFsm<GameFramework.Procedure.IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }

        private void OnSwitchProcedureSuccess(object sender, GameEventArgs e)
        {
            SwitchProcedureSuccessEventArgs switchProcedureEvent = (SwitchProcedureSuccessEventArgs)e;
            string curProcedureName = switchProcedureEvent.CurProcedure;
            switch (curProcedureName)
            {
                case "ProcedureWorld":
                    ChangeState<ProcedureWorld>(m_ProcedureOwner);
                    break;
            }
        }

        void LoadHomeScene()
        {
            TuwanSceneUtils.LoadScene("Home");
        }

        private void OnLoadSceneSuccess(object sender, GameEventArgs e)
        {
            LoadSceneSuccessEventArgs ne = (LoadSceneSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Info("Load scene '{0}' OK.", ne.SceneAssetName);

            UnloadLoginScene();
        }

        void UnloadLoginScene()
        {
            string launchName = SceneManager.GetActiveScene().name;
            if (launchName == "Home")
            {
                SceneManager.UnloadSceneAsync("Login");
            }

        }
    }
}
