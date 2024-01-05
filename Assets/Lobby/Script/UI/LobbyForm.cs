using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tuwan;
using UnityEngine.UI;
using System.Linq;
using World;
using GameFramework.Event;
using Tuwan.Const;
using Tuwan.Proto;
using Tuwan.Lobby.Logic;

namespace Lobby
{
    public enum ELobbyUserType
    {
        Audience,   //观众
        Guest,      //嘉宾
        Host,       //主持人
    }

    public class LobbyForm : UGuiForm
    {
        [Header("Common")]
        public Text LobbyNameText;
        public Text LobbyIdText;
        public Button LobbyBGMButton;
        public Button UpWheatButton;
        public Button DownWheatButton;
        public Button OpenWheatButton;
        public Button CloseWheatButton;
        public Button SettingsButton;
        public Button ExitButton;
        public Button StartDanceButton;
        public Button StopDanceButton;
        public Button ChatButton;
        public GameObject ChatNode;
        public Button GiftButton;
        public Button TestGiftButton;
        public Button HostManageMicButton;
        public GameObject OpenButtons;
        //public Rocker Roc;

        [Header("SVGA")]
        public GameObject SVGAGiftGO;

        [Header("Audience")]
        public Text RequestForbidCountDownText;

        //迪厅用户数据，后期转到model层
        public ELobbyUserType userType = ELobbyUserType.Audience;

        private Dictionary<string, int> UidsPosition = new Dictionary<string, int>();

        private bool isChatUIOpen = false;

        private List<UserInfoResponsedData> ApplyList = new List<UserInfoResponsedData>();

        //移动组件
        private PlayerMovement movement;
        public PlayerMovement Movement
        {
            get
            {
                if (movement == null)
                {
                    movement = FindObjectsOfType<PlayerMovement>().Where(c => c.IsOwner).FirstOrDefault();
                }
                return movement;
            }
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            OnOpenLobbyForm(userData as string);

            //EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_ENTER_WORLD_INIT_LOBBY_FORM, Roc);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            
            EventCenter.inst.AddEventListener<string>((int)UIEventTag.EVENT_UI_ENTER_LOBBY_FORM, OnOpenLobbyForm);

            RegisterButtonListener();
            RegisterSocketListener();

            WheatCtrl.GetOrderList();
            //GameEntry.UI.CloseUIForm((int)UIFormId.SceenLoading);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            EventCenter.inst.RemoveEventListener<string>((int)UIEventTag.EVENT_UI_ENTER_LOBBY_FORM, OnOpenLobbyForm);

            UnregisterButtonListener();
            UnRegisterSocketListener();
        }

        void RegisterSocketListener()
        {
            EventCenter.inst.AddSocketEventListener<string, string>((int)SocketIoType.APPLY_WHEAT, OnReceiveApplyWheat);

            EventCenter.inst.AddSocketEventListener<int, string>((int)SocketIoType.WHEATLIST, OnWheatInfoChange);
        }
        void OnWheatInfoChange(int type, string data)
        {
            UpdateBgmButtonState();
        }
        void UnRegisterSocketListener()
        {
            EventCenter.inst.RemoveSocketEventListener<string, string>((int)SocketIoType.APPLY_WHEAT, OnReceiveApplyWheat);
            EventCenter.inst.RemoveSocketEventListener<int, string>((int)SocketIoType.WHEATLIST, OnWheatInfoChange);
        }

        private void OnReceiveApplyWheat(string uid, string data)
        {
            Debug.Log("data===" + data);
            if (LobbyData.inst.JudgeSelfHoster())
            {
                //主持人
                ApplyList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserInfoResponsedData>>(data);
            }
            else
            {
                if (uid == Store.Config.UserInfo.uid.ToString())
                {
                    Debug.Log("申请成功");
                }
            }
        }
        void RegisterButtonListener()
        {
            LobbyBGMButton.onClick.AddListener(OnLobbyBGMButtonClick);
            SettingsButton.onClick.AddListener(OnSettingsButtonClick);
            ExitButton.onClick.AddListener(OnExitButtonClick);
            StartDanceButton.onClick.AddListener(OnStartDanceButtonClick);
            StopDanceButton.onClick.AddListener(OnStopDanceButtonClick);
            ChatButton.onClick.AddListener(OnChatButtonClick);
            GiftButton.onClick.AddListener(OnGiftButtonClick);
            TestGiftButton.onClick.AddListener(OnTestGiftButtonClick);

            OpenWheatButton.onClick.AddListener(OnHostTurnOnMicButtonClick);
            CloseWheatButton.onClick.AddListener(OnHostTurnOffMicButtonClick);
            HostManageMicButton.onClick.AddListener(OnHostManageMicButtonClick);
            UpWheatButton.onClick.AddListener(OnAudienceRequestMicButtonClick);
            DownWheatButton.onClick.AddListener(OnRequestDownMicButtonClick);
        }

        void UnregisterButtonListener()
        {
            LobbyBGMButton.onClick.RemoveListener(OnLobbyBGMButtonClick);
            SettingsButton.onClick.RemoveListener(OnSettingsButtonClick);
            ExitButton.onClick.RemoveListener(OnExitButtonClick);
            StartDanceButton.onClick.RemoveListener(OnStartDanceButtonClick);
            StopDanceButton.onClick.RemoveListener(OnStopDanceButtonClick);
            ChatButton.onClick.RemoveListener(OnChatButtonClick);
            GiftButton.onClick.RemoveListener(OnGiftButtonClick);
            TestGiftButton.onClick.RemoveListener(OnTestGiftButtonClick);

            OpenWheatButton.onClick.RemoveListener(OnHostTurnOnMicButtonClick);
            CloseWheatButton.onClick.RemoveListener(OnHostTurnOffMicButtonClick);
            HostManageMicButton.onClick.RemoveListener(OnHostManageMicButtonClick);
            UpWheatButton.onClick.RemoveListener(OnAudienceRequestMicButtonClick);
            DownWheatButton.onClick.RemoveListener(OnRequestDownMicButtonClick);
        }

        public void InitLobbyForm()
        {
            ChatNode.SetActive(isChatUIOpen);
            UpdateDanceButtonState();
            UpdateBgmButtonState();
        }
        void UpdateBgmButtonState()
        {
            bool isHoster = LobbyData.inst.JudgeSelfHoster();
            LobbyBGMButton.gameObject.SetActive(isHoster);
            HostManageMicButton.gameObject.SetActive(isHoster);
            bool isOnWheat = LobbyData.inst.JudgeSelfOnWheat();
            UpWheatButton.gameObject.SetActive(!isOnWheat);
            DownWheatButton.gameObject.SetActive(isOnWheat);
            OpenWheatButton.gameObject.SetActive(isOnWheat && !LobbyData.inst.IsOpenMic);
            CloseWheatButton.gameObject.SetActive(isOnWheat && LobbyData.inst.IsOpenMic);
            if (!isOnWheat)
            {
                LobbyData.inst.IsOpenMic = false;
                AgoraManager.inst.RtcEngine.MuteLocalAudioStream(false);
            }

        }
        private void InitRoomInfo()
        {
            LobbyNameText.text = Store.Config.RoomInfo.Title;
            LobbyIdText.text = "ID:" + Store.Config.RoomInfo.Cid;
        }

        void UpdateDanceButtonState()
        {
            if (Movement == null)
            {
                StartDanceButton.gameObject.SetActive(true);
                StopDanceButton.gameObject.SetActive(false);
                return;
            }
            StartDanceButton.gameObject.SetActive(!Movement.isDancing);
            StopDanceButton.gameObject.SetActive(Movement.isDancing);
        }

        #region ButtonCallback
        public void OnLobbyBGMButtonClick()
        {
            GameEntry.UI.OpenUIForm(UIFormId.LobbyBGMForm);
        }

        public void OnSettingsButtonClick()
        {
            GameEntry.UI.OpenUIForm(UIFormId.LobbySettingsForm);
        }


        public void OnExitButtonClick()
        {

        }

        public void OnStartDanceButtonClick()
        {
            Movement.isDancing = true;
            UpdateDanceButtonState();
        }

        public void OnStopDanceButtonClick()
        {
            Movement.isDancing = false;
            UpdateDanceButtonState();
        }

        public void OnChatButtonClick()
        {
            isChatUIOpen = !isChatUIOpen;
            ChatNode.SetActive(isChatUIOpen);
        }

        public void OnGiftButtonClick()
        {
            GameEntry.UI.OpenUIForm(UIFormId.GiftForm);
        }

        public void OnTestGiftButtonClick()
        {
            //Player currentPlayer = GameObject.FindObjectsOfType<Player>().Where(c => c.IsOwner).FirstOrDefault();
            //GameObject[] hosts = GameObject.FindGameObjectsWithTag("Host");
            //GameObject host = hosts[Random.Range(0, hosts.Length)];
            //string giftUrl = "https://img3.tuwandata.com/uploads/play/1916771563790770.svga";
            //Transform sender = currentPlayer.gameObject.transform;
            //PlaySvga playSvga = GameObject.FindFirstObjectByType<PlaySvga>();
            //GameObject poolParent = GameObject.Find("PoolParent");

            ////3D世界SVGA飞行
            //List<Transform> receiverList = new List<Transform>();
            //receiverList.Add(host.transform);
            //SVGAUtils.SendGiftAnim(giftUrl, playSvga, sender, receiverList, SVGAGiftGO, poolParent);



            string svgaUrl = "https://img3.tuwandata.com/uploads/play/1916771563790770.svga";
            //svgaUrl = "https://img3.tuwandata.com/uploads/play/8418431577015639.svga";

            //UI的SVGA播放，切到3D世界SVGA飞行
            GameEntry.UI.OpenUIForm(UIFormId.SVGAOnUIForm, svgaUrl);
        }

        public void OnHostTurnOnMicButtonClick()
        {
            LobbyData.inst.IsOpenMic = true;
            UpdateBgmButtonState();
            AgoraManager.inst.RtcEngine.MuteLocalAudioStream(false);
        }

        public void OnHostTurnOffMicButtonClick()
        {
            LobbyData.inst.IsOpenMic = false;
            UpdateBgmButtonState();
            AgoraManager.inst.RtcEngine.MuteLocalAudioStream(true);
        }

        public void OnHostManageMicButtonClick()
        {
            if (ApplyList.Count > 0)
            {
                GameEntry.UI.OpenUIForm(UIFormId.RequestMicForm, ApplyList);
            }
            else
            {
                GameEntry.UI.OpenDialog(new DialogParams()
                {
                    Mode = 1,
                    Title = "提示",
                    Message = "暂时没人申请上麦哦"
                });
            }
        }


        public void OnAudienceRequestMicButtonClick()
        {
            if (LobbyData.inst.JudgeSelfIsHoster())
            {
                WheatCtrl.AgreeWheat(Store.Config.UserInfo.uid, 2);
            }
            else
            {
                WheatCtrl.ApplyWheat();
            }

            // //在禁止时间内
            // if (isInRequestForbidding)
            // {
            //     GameEntry.UI.OpenDialog(new DialogParams()
            //     {
            //         Mode = 1,
            //         Title = "提示",
            //         Message = "请勿频繁申请上麦。"
            //     });
            // }
            // else
            // {
            //     isInRequestForbidding = true;
            //     SetCountDown();
            // }
        }

        public void OnRequestDownMicButtonClick()
        {
            WheatCtrl.DownWheat();
        }

        #endregion


        #region Request Forbid
        private bool isInRequestForbidding = false;
        private int timer = 0;
        private int countDownTime = 30;
        Coroutine countDownCorou = null;

        void SetCountDown()
        {
            timer = countDownTime;
            countDownCorou = StartCoroutine(RequestForbidCountDownCoroutine());
        }

        void UpdateRequestForbidCountDownText()
        {
            if (isInRequestForbidding)
            {
                RequestForbidCountDownText.text = string.Format("{0}s", timer);
            }
            else
            {
                RequestForbidCountDownText.text = string.Empty;
            }
        }

        IEnumerator RequestForbidCountDownCoroutine()
        {
            while (timer >= 0)
            {
                UpdateRequestForbidCountDownText();
                yield return new WaitForSeconds(1);
                timer -= 1;
            }

            timer = 0;
            isInRequestForbidding = false;

            UpdateRequestForbidCountDownText();

            if (countDownCorou != null)
            {
                StopCoroutine(countDownCorou);
                countDownCorou = null;
            }
        }
        #endregion


        protected void OnOpenLobbyForm(string curSceneName)
        {
            if (curSceneName == "WorldOther")
            {
                OpenButtons.SetActive(false);
            }
            else if (curSceneName == "LobbyOther")
            {
                AgoraManager.inst.CloseSpatialAudio();
                AgoraManager.inst.RtcEngine.MuteAllRemoteAudioStreams(false);
                OpenButtons.SetActive(true);
                InitLobbyForm();
                InitRoomInfo();
            }
        }


        private void OnDestroy()
        {
            if (countDownCorou != null)
            {
                StopCoroutine(countDownCorou);
            }
        }
    }
}