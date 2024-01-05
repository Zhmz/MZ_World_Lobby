
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using FishNet.Managing;
using FishNet.Transporting;
using FishNet.Transporting.Multipass;
using FishNet.Transporting.Tugboat;
using GameFramework.Event;
using TMPro;
using Tuwan;
using Tuwan.Const;
using Tuwan.Proto;

//using Unity.Netcode;
//using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using World;

//轨迹类型
public enum ETraceType
{
    Line,//直线
    Arc,//弧形
    Bezier,//贝塞尔曲线
}

public class NetworkInit : MonoBehaviour
{
    private TuwanNetworkInit networkInit;

    public GameObject svgaGift;
    GameObject poolParent;

    public ETraceType traceType = ETraceType.Line;
    private float speed = 10;
    private float curveHeight = 3;

    public Slider speedSlider;
    public Text speedValueText;
    public Slider curveHeightSlider;
    public Text curveHeightVaueText;

    public Dropdown traceDropDown;


    //麦位
    public GameObject HostSlot;
    public ParticleSystem HostParticle;
    public GameObject GuestSlotParent;
    public GameObject[] GuestSlotList;
    public ParticleSystem[] GuestParticleList;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        speedSlider.onValueChanged.AddListener(OnSpeedSliderValueChange);
        curveHeightSlider.onValueChanged.AddListener(OnParabolicHeightSliderValueChange);
        traceDropDown.onValueChanged.AddListener(OnTraceTypeChange);
        EventCenter.inst.AddSocketEventListener<int, string>((int)SocketIoType.CHAT_TEXT, OnReceiveGift);
        //ventCenter.inst.AddEventListener<int>((int)UIEventTag.EVENT_UI_TUWAN_PLAYER_INFO_SYNCED, OnTuwanUIdSynced);
        EventCenter.inst.AddEventListener<int>((int)UIEventTag.EVENT_UI_FROM_WORLD_ENTER_LOBBY, OnFromWorldEnterLobby);
        EventCenter.inst.AddEventListener<Dictionary<string, int>>((int)UIEventTag.EVENT_UI_REFRESH_WHEAT_STATE, OnRefreshWheatState);
        EventCenter.inst.AddEventListener<GiftData>((int)UIEventTag.EVENT_UI_SEND_GIFT, OnSendGift);
        EventCenter.inst.AddEventListener<RawImage>((int)UIEventTag.EVENT_UI_OPEN_SVGA_ON_UI_FORM, OnOpenSVGAOnUIForm);
    }

    private void OnDisable()
    {
        speedSlider.onValueChanged.RemoveListener(OnSpeedSliderValueChange);
        curveHeightSlider.onValueChanged.RemoveListener(OnParabolicHeightSliderValueChange);
        traceDropDown.onValueChanged.AddListener(OnTraceTypeChange);
        EventCenter.inst.RemoveSocketEventListener<int, string>((int)SocketIoType.CHAT_TEXT, OnReceiveGift);
        //EventCenter.inst.RemoveEventListener<int>((int)UIEventTag.EVENT_UI_TUWAN_PLAYER_INFO_SYNCED, OnTuwanUIdSynced);
        EventCenter.inst.RemoveEventListener<int>((int)UIEventTag.EVENT_UI_FROM_WORLD_ENTER_LOBBY, OnFromWorldEnterLobby);
        EventCenter.inst.RemoveEventListener<Dictionary<string, int>>((int)UIEventTag.EVENT_UI_REFRESH_WHEAT_STATE, OnRefreshWheatState);
        EventCenter.inst.RemoveEventListener<GiftData>((int)UIEventTag.EVENT_UI_SEND_GIFT, OnSendGift);
        EventCenter.inst.RemoveEventListener<RawImage>((int)UIEventTag.EVENT_UI_OPEN_SVGA_ON_UI_FORM, OnOpenSVGAOnUIForm);
    }

    void OnSpeedSliderValueChange(float value)
    {
        speed = value;
        speedValueText.text = speed.ToString("F2");
    }

    void OnParabolicHeightSliderValueChange(float value)
    {
        curveHeight = value;
        curveHeightVaueText.text = curveHeight.ToString("F2");
    }

    void OnTraceTypeChange(int value)
    {
        traceType = (ETraceType)value;
    }


    // Start is called before the first frame update
    void Start()
    {
        speedSlider.minValue = 1;
        speedSlider.maxValue = 20;
        speedSlider.value = speed;
        speedValueText.text = speed.ToString("F2");

        curveHeightSlider.minValue = 0;
        curveHeightSlider.maxValue = 5;
        curveHeightSlider.value = curveHeight;
        curveHeightVaueText.text = curveHeight.ToString("F2");

        traceDropDown.value = (int)traceType;

        poolParent = new GameObject("PoolParent");

        networkInit = FindObjectOfType<TuwanNetworkInit>();


        //if (networkInit.StartType == AutoStartType.Host)
        //{
        //    networkInit.SwitchServer(true);
        //    networkInit.SwitchClient(true);
        //}
        //else if (networkInit.StartType == AutoStartType.Server)
        //{
        //    networkInit.SwitchServer(true);
        //}
        //else if (networkInit.StartType == AutoStartType.Client)
        //{
        //    networkInit.SwitchClient(true);
        //}
    }

    private void OnReceiveGift(int type, string data)
    {
        ShowGiftResponse giftInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ShowGiftResponse>(data);
        Debug.Log("OnReceiveGift0===" + type);
        if (giftInfo.type == (int)SocketDataType.CHAT_SHOW_GIFT)
        {
            Debug.Log("OnReceiveGift");

            string svgaPath = TuwanUtils.ReplacePath(giftInfo.pathsvga);
            List<int> toUids = new List<int>();
            for (var i = 0; i < giftInfo.touserinfo.Count; i++)
            {
                toUids.Add(int.Parse(giftInfo.touserinfo[i].userid));
            }
            OnSendGift(new GiftData()
            {
                path = svgaPath,
                formUid = giftInfo.from,
                receiverIdList = toUids,
            });
            // EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_MOVE_AUDIENCE_TO_LOBBY_RANDOM_POSITION, new GiftData()
            // {
            //     path = svgaPath,
            //     formUid = giftInfo.from,
            //     receiverIdList = toUids,
            // });
            // SendGift(svgaPath, giftInfo.from, toUids);
        }
    }

    public void OnClientClick()
    {
        networkInit.SwitchClient(true);
    }

    public void OnHostClick()
    {
        networkInit.SwitchServer(true);
        networkInit.SwitchClient(true);
    }

    public void OnServerClick()
    {
        networkInit.SwitchServer(true);
    }

    public void OnDanceClick()
    {
        PlayerMovement playerMovement = GameObject.FindObjectsOfType<PlayerMovement>().Where(c => c.IsOwner).FirstOrDefault();
        playerMovement?.ChangeDance();
    }

    public void OnGiftClick()
    {
        //播放起来
        PlaySvga playSvga = GameObject.FindFirstObjectByType<PlaySvga>();
        playSvga.Play();

        Player currentPlayer = GameObject.FindObjectsOfType<Player>().Where(c => c.IsOwner).FirstOrDefault();

        GameObject[] hosts = GameObject.FindGameObjectsWithTag("Host");

        GameObject host = hosts[Random.Range(0, hosts.Length)];

        //Vector3 position = currentPlayer.gameObject.transform.position;

        //position.y = position.y + 2;

        //普通创建
        //GameObject svgaGiftObj = GameObject.Instantiate(svgaGift, position, currentPlayer.gameObject.transform.rotation);

        List<Transform> receiverList = new List<Transform>();
        receiverList.Add(host.transform);
        Transform sender = currentPlayer.gameObject.transform;
        string giftUrl = "https://img3.tuwandata.com/uploads/play/1916771563790770.svga";
        SVGAUtils.SendGiftAnim(giftUrl, playSvga, sender, receiverList, svgaGift, poolParent);

        ////对象池创建
        //GameObject svgaGiftObj = PoolManager.CreateGameObject(svgaGift, poolParent);
        //svgaGiftObj.transform.position = position;
        //svgaGiftObj.transform.rotation = currentPlayer.gameObject.transform.rotation;

        //BaseTrace trace = svgaGiftObj.GetComponent<BaseTrace>();
        //if (trace == null)
        //{
        //    if (traceType == ETraceType.Line)
        //    {
        //        trace = svgaGiftObj.AddComponent<LineTrace>();
        //    }
        //    else if (traceType == ETraceType.Arc)
        //    {
        //        trace = svgaGiftObj.AddComponent<ArcTrace>();
        //        (trace as ArcTrace).SetArcHeight(curveHeight);
        //    }
        //    else if (traceType == ETraceType.Bezier)
        //    {
        //        trace = svgaGiftObj.AddComponent<BezierTrace>();
        //        (trace as BezierTrace).SetBezierParamPosSimplely(position, host.transform.position, curveHeight);
        //    }
        //}
        //trace.SetDestination(host.transform, new Vector3(0, 2, 0));
        //trace.SetSpeed(speed);
    }


    //private int ownerTuwanUId;
    //private void UpdateMicSlots()
    //{
    //    WheatResponse thisWheat = null;
    //    var wheatMap = LobbyData.inst.WheatMap;
    //    foreach (WheatResponse wheatRes in wheatMap.Values)
    //    {
    //        if (wheatRes.uid == ownerTuwanUId)
    //        {
    //            thisWheat = wheatRes;
    //        }
    //    }

    //    //普通观众
    //    if (thisWheat == null)
    //    {
    //        Bounds spawnArea = GameObject.Find("LobbySpawnArea").GetComponent<MeshRenderer>().bounds;
    //        Vector3 randomPos = new Vector3(Random.Range(spawnArea.min.x, spawnArea.max.x), spawnArea.center.y, Random.Range(spawnArea.min.z, spawnArea.max.z));
    //        EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_MOVE_AUDIENCE_TO_LOBBY_RANDOM_POSITION, randomPos);
    //    }
    //    //麦位主持或嘉宾
    //    else
    //    {
    //        int pos = thisWheat.position;
    //        if (pos == (int)EWheatPosition.EHost)
    //        {
    //            //关闭粒子
    //            //HostParticle.gameObject.SetActive(false);
    //            SetWheatParticleColor(Color.yellow, EWheatPosition.EHost);
    //            //发送消息给player，修改位置
    //            EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_MOVE_PLAYER_TO_WHEAT_POSITION, HostSlot.transform);
    //        }
    //        else if (pos >= (int)EWheatPosition.EGuest1 && pos <= (int)EWheatPosition.EGuest8)
    //        {
    //            //设置粒子颜色
    //            SetWheatParticleColor(Color.yellow, (EWheatPosition)pos);
    //3~10映射到0～7
    //            int guestIndex = pos - 3;
    //            if (guestIndex >= 0 && guestIndex < GuestSlotList.Length && guestIndex < GuestParticleList.Length)
    //            {
    //                //发送消息给player，修改位置
    //                EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_MOVE_PLAYER_TO_WHEAT_POSITION, GuestSlotList[guestIndex].transform);
    //            }
    //        }
    //    }
    //}

    //private void OnTuwanUIdSynced(int tuwanUId)
    //{
    //    ownerTuwanUId = tuwanUId;
    //    UpdateMicSlots();
    //}

    private void OnFromWorldEnterLobby(int uid)
    {
        int wheatPos = LobbyData.inst.GetSelfPosition();
        RefreshWheatState(true, uid, wheatPos);
    }

    void OnRefreshWheatState(Dictionary<string, int> dic)
    {
        if (dic.ContainsKey("connectMic") && dic.ContainsKey("uid") && dic.ContainsKey("pos"))
        {
            RefreshWheatState(dic["connectMic"] == 1, dic["uid"], dic["pos"]);
        }
    }

    void RefreshWheatState(bool connectMic, int uid, int pos)
    {
        //上麦
        if (connectMic)
        {
            if (pos == (int)EWheatPosition.EHost)
            {
                //关闭粒子
                //HostParticle.gameObject.SetActive(false);
                SetWheatParticleColor(Color.yellow, EWheatPosition.EHost);
                //发送消息给player，修改位置
                EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_MOVE_PLAYER_TO_WHEAT_POSITION, uid, HostSlot.transform);
            }
            else if (pos >= (int)EWheatPosition.EGuest1 && pos <= (int)EWheatPosition.EGuest8)
            {
                //设置粒子颜色
                SetWheatParticleColor(Color.yellow, (EWheatPosition)pos);
                //3~10映射到0～7
                int guestIndex = pos - 3;
                if (guestIndex >= 0 && guestIndex < GuestSlotList.Length && guestIndex < GuestParticleList.Length)
                {
                    //发送消息给player，修改位置
                    EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_MOVE_PLAYER_TO_WHEAT_POSITION, uid, GuestSlotList[guestIndex].transform);
                }
            }
            else
            {
                RandomPosForAudience(uid);
            }
        }
        //下麦
        else
        {
            RandomPosForAudience(uid);
        }
    }

    void RandomPosForAudience(int uid)
    {
        Bounds spawnArea = GameObject.Find("LobbySpawnArea").GetComponent<MeshRenderer>().bounds;
        Vector3 randomPos = new Vector3(Random.Range(spawnArea.min.x, spawnArea.max.x), spawnArea.center.y, Random.Range(spawnArea.min.z, spawnArea.max.z));
        EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_MOVE_AUDIENCE_TO_LOBBY_RANDOM_POSITION, uid, randomPos);
    }

    private void SetWheatParticleColor(Color color, EWheatPosition position)
    {
        int index = -1;
        if (position == EWheatPosition.EHost)
        {
            index = 2;
        }
        else
        {
            index = (int)position - 3;
        }
        if (index >= 0 && index < GuestParticleList.Length)
        {
            var par = GuestParticleList[index];
            ParticleSystem.MainModule mainModule = par.main;
            mainModule.startColor = color;
        }
    }

    //send gift
    private GiftData curGiftData;
    public GameObject SVGAGiftGO;
    void OnSendGift(GiftData giftData)
    {
        if (giftData == null)
        {
            return;
        }

        curGiftData = giftData;
        GameEntry.UI.OpenUIForm(UIFormId.SVGAOnUIForm, giftData.path);
    }

    private void OnOpenSVGAOnUIForm(RawImage renderTex)
    {
        if (curGiftData == null)
        {
            CloseSVGAForm();
            return;
        }

        List<Player> playerList = GameObject.FindObjectsOfType<Player>().ToList();
        Player senderPlayer = playerList.Find(x => x.CurPlayerInfo != null && x.CurPlayerInfo.uid == curGiftData.formUid);
        if (senderPlayer == null)
        {
            Debug.LogError("senderPlayer == null");
            CloseSVGAForm();
            return;
        }
        List<Player> receiverPlayerList = playerList.FindAll(x => x.CurPlayerInfo != null && curGiftData.receiverIdList.Contains(x.CurPlayerInfo.uid));
        if (receiverPlayerList.Count == 0)
        {
            Debug.LogError("receiverPlayerList.Count == 0");
            CloseSVGAForm();
            return;
        }

        List<Transform> receiverTransList = new List<Transform>();
        receiverPlayerList.ForEach((item) =>
        {
            receiverTransList.Add(item.transform);
        });
        string giftUrl = curGiftData.path;
        Transform sender = senderPlayer.transform;
        Vector3 senderPos = sender.position;
        senderPos.y += 2;

        //UI传递到3d世界
        //SVGAUtils.SendGiftAnimOnUI(giftUrl, renderTex, senderPos, receiverTransList, SVGAGiftGO, poolParent);
        //UI直接缩放到3d世界位置
        SVGAUtils.SendGiftAnimByCoordTransformation(renderTex,receiverTransList, renderTex.transform.parent.GetComponent<RectTransform>());


        curGiftData = null;
    }

    void CloseSVGAForm()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(2f).
            AppendCallback(() =>
            {
                EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_SVGA_ON_UI_ANIM_COMPLETE);
            })
            .Play();
    }
}
