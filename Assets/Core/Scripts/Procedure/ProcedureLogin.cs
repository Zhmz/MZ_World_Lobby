using GameFramework.Event;
using GameFramework.Fsm;
using UnityEngine.SceneManagement;
using UnityGameFramework.Runtime;

namespace Tuwan
{
    public class ProcedureLogin : ProcedureBase
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

            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.Event.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            GameEntry.Event.Subscribe(SwitchProcedureSuccessEventArgs.EventId, OnSwitchProcedureSuccess);

            //OpenLoginForm();

            LoadLoginScene();
        }

        protected override void OnLeave(IFsm<GameFramework.Procedure.IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.Event.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            GameEntry.Event.Unsubscribe(SwitchProcedureSuccessEventArgs.EventId, OnSwitchProcedureSuccess);
        }

        protected override void OnUpdate(IFsm<GameFramework.Procedure.IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
        }

        private void LoadLoginScene()
        {
            TuwanSceneUtils.LoadScene("Login");
        }

        private void OnLoadSceneSuccess(object sender, GameEventArgs e)
        {
            LoadSceneSuccessEventArgs ne = (LoadSceneSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Info("Load scene '{0}' OK.", ne.SceneAssetName);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Login"));
        }

        private void OnSwitchProcedureSuccess(object sender, GameEventArgs e)
        {
            SwitchProcedureSuccessEventArgs switchProcedureEvent = (SwitchProcedureSuccessEventArgs)e;
            string curProcedureName = switchProcedureEvent.CurProcedure;
            switch (curProcedureName)
            {
                case "ProcedureHome":
                    ChangeState<ProcedureHome>(m_ProcedureOwner);
                    break;
            }
        }
    }
}
