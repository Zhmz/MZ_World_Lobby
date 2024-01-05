using System.Collections;
using System.Collections.Generic;
using Agora.Rtc;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AgoraManager : MonoBehaviour
{

#if UNITY_2018_3_OR_NEWER && UNITY_ANDROID
    private ArrayList permissionList = new ArrayList() { Permission.Microphone };
#endif

    [FormerlySerializedAs("APP_ID")]
    [SerializeField]
    private string _appID = "";

    [FormerlySerializedAs("TOKEN")]
    [SerializeField]
    private string _token = "";

    [FormerlySerializedAs("CHANNEL_NAME")]
    [SerializeField]
    private string _channelName = "";

    public PlayerAgoraView AgoraView;


    //public uint remoteUid;
    //internal VideoSurface LocalView;
    //internal VideoSurface RemoteView;
    internal IRtcEngine RtcEngine;
    private ILocalSpatialAudioEngine localSpatial;
    private static AgoraManager m_instance;
    public uint m_SelfAgoraUId;
    //public GameObject m_SelfCharacter;
    //Dictionary<uint, GameObject> m_RemoteCharacterDic = new Dictionary<uint, GameObject>();

    private int curMicValue = 100;
    private int curSoundValue = 100;
    public int maxMicValue = 400;
    public int maxSoundValue = 400;

    public int CurMicValue
    {
        get { return curMicValue; }
        set
        {
            curMicValue = value;
            RtcEngine.AdjustRecordingSignalVolume(CurMicValue);
            AgoraView.MicValueText.text = curMicValue.ToString();
        }
    }

    public int CurSoundValue
    {
        get { return curSoundValue; }
        set
        {
            curSoundValue = value;
            RtcEngine.AdjustPlaybackSignalVolume(CurSoundValue);
            AgoraView.SoundValueText.text = curSoundValue.ToString();
        }
    }
    //单例初始化
    public static AgoraManager inst
    {
        get
        {
            return m_instance;
        }
    }
    private void Awake()
    {
        m_instance = this;
    }
    void Start()
    {
        CheckPermissions();
        SetupVideoSDKEngine();
        configureSpatialAudioEngine();

        InitEventHandler();
    }

    private void OnEnable()
    {
        AgoraView.JoinButton.onClick.AddListener(JoinChannel);
        AgoraView.LeaveButton.onClick.AddListener(LeaveChannel);

        AgoraView.MicSlider.onValueChanged.AddListener(OnMicValueChange);
        AgoraView.SoundSlider.onValueChanged.AddListener(OnSoundValueChange);
    }

    private void OnDisable()
    {
        AgoraView.JoinButton.onClick.RemoveListener(JoinChannel);
        AgoraView.LeaveButton.onClick.RemoveListener(LeaveChannel);

        AgoraView.MicSlider.onValueChanged.RemoveListener(OnMicValueChange);
        AgoraView.SoundSlider.onValueChanged.RemoveListener(OnSoundValueChange);
    }

    private void CheckPermissions()
    {
#if UNITY_2018_3_OR_NEWER && UNITY_ANDROID
        foreach (string permission in permissionList)
        {
            if (!Permission.HasUserAuthorizedPermission(permission))
            {
                Permission.RequestUserPermission(permission);
            }
        }
#endif
    }

    void OnApplicationQuit()
    {
        if (RtcEngine != null)
        {
            LeaveChannel();
            RtcEngine.Dispose();
            RtcEngine = null;
        }
    }

    private void SetupVideoSDKEngine()
    {
        // Create an instance of the video SDK.
        RtcEngine = Agora.Rtc.RtcEngine.CreateAgoraRtcEngine();
        // Specify the context configuration to initialize the created instance.
        RtcEngineContext context = new RtcEngineContext(_appID, 0,
                                    CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING,
                                    AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_GAME_STREAMING, AREA_CODE.AREA_CODE_GLOB, new LogConfig("./log.txt"));
        RtcEngine.Initialize(context);

        // By default Agora subscribes to the audio streams of all remote users.
        // Unsubscribe all remote users; otherwise, the audio reception range you set
        // is invalid.
        //先不管空间音频，测试对话
        //关闭全通道音频，测试空间音频
        RtcEngine.MuteLocalAudioStream(true);
        RtcEngine.MuteAllRemoteAudioStreams(true);

        //调整音量
        CurMicValue = 100;
        CurSoundValue = 100;
        //更新slider
        AgoraView.UpdateMicSlider(CurMicValue, 0, maxMicValue);
        AgoraView.UpdateSoundSlider(CurSoundValue, 0, maxSoundValue);
    }

    private void configureSpatialAudioEngine()
    {
        RtcEngine.EnableAudio();

        // RtcEngine.EnableSpatialAudio(true);
        LocalSpatialAudioConfig localSpatialAudioConfig = new LocalSpatialAudioConfig();
        localSpatialAudioConfig.rtcEngine = RtcEngine;
        localSpatial = RtcEngine.GetLocalSpatialAudioEngine();
        localSpatial.Initialize();

        // Doing this here is wrong, see SetupVideoSDKEngine()
        //localSpatial.MuteLocalAudioStream(true);
        //localSpatial.MuteAllRemoteAudioStreams(true);

        // Set the audio reception range, in meters, of the local user
        localSpatial.SetAudioRecvRange(100);
        // Set the length, in meters, of unit distance
        localSpatial.SetDistanceUnit(1);

        // Update self position
        //float[] pos = new float[] { 0.0F, 0.0F, 0.0F };
        //float[] forward = new float[] { 1.0F, 0.0F, 0.0F };
        //float[] right = new float[] { 0.0F, 1.0F, 0.0F };
        //float[] up = new float[] { 0.0F, 0.0F, 1.0F };

        //打开本地和订阅全部的远程空间音效
        localSpatial.MuteLocalAudioStream(false);
        localSpatial.MuteAllRemoteAudioStreams(false);
    }
    public void CloseSpatialAudio()
    {
        localSpatial.MuteLocalAudioStream(true);
        localSpatial.MuteAllRemoteAudioStreams(true);
    }

    public int UpdateSelfPosition(Transform selfTrans)
    {
        float[] pos = TuwanAgoraUtils.FormatAgoraVec3(selfTrans.position);
        float[] forward = TuwanAgoraUtils.FormatAgoraVec3(selfTrans.forward);
        float[] right = TuwanAgoraUtils.FormatAgoraVec3(selfTrans.right);
        float[] up = TuwanAgoraUtils.FormatAgoraVec3(selfTrans.up);
        //Debug.LogErrorFormat("pos = {0},{1},{2}", pos[1],pos[2],pos[0]);
        //Debug.LogErrorFormat("pos = {0}, forward = {1}, right = {2}, up = {3}", selfTrans.position, selfTrans.forward, selfTrans.right, selfTrans.up);
        return localSpatial.UpdateSelfPosition(pos, forward, right, up);
    }

    public void OnMicValueChange(float value)
    {
        CurMicValue = (int)value;
    }

    public void OnSoundValueChange(float value)
    {
        CurSoundValue = (int)value;
    }

    //public void OnOtherJoinChannel(uint uid)
    //{
    //    if (uid == m_SelfUId || m_RemoteCharacterDic.ContainsKey(uid))
    //    {
    //        return;
    //    }

    //    GameObject[] characters = GameObject.FindGameObjectsWithTag("RuntimeCharacter");
    //    GameObject curRemoteCharacter = null;
    //    for (int i = 0; i < characters.Length; i++)
    //    {
    //        if (characters[i] == m_SelfCharacter)
    //        {
    //            continue;
    //        }
    //        else if (m_RemoteCharacterDic.ContainsValue(characters[i]))
    //        {
    //            continue;
    //        }
    //        else
    //        {
    //            curRemoteCharacter = characters[i];
    //            m_RemoteCharacterDic.Add(uid, characters[i]);
    //        }
    //    }

    //    UpdateRemotePosition(uid, curRemoteCharacter.transform.position, curRemoteCharacter.transform.forward);
    //}

    //这里的数组全部是按照zxy的顺序填值
    //返回0调用成功
    public int UpdateRemotePosition(uint uid, Vector3 position, Vector3 forward)
    {
        RemoteVoicePositionInfo posInfo = new RemoteVoicePositionInfo(
            TuwanAgoraUtils.FormatAgoraVec3(position),
            TuwanAgoraUtils.FormatAgoraVec3(forward));
        return localSpatial.UpdateRemotePosition(uid, posInfo);
    }

    private void InitEventHandler()
    {
        // Creates a UserEventHandler instance.
        UserEventHandler handler = new UserEventHandler(this);
        RtcEngine.InitEventHandler(handler);
    }

    public void JoinChannel()
    {
        // Enable the video module.
        RtcEngine.EnableVideo();
        // Set the user role as broadcaster.
        RtcEngine.SetClientRole(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);
        // Set the local video view.
        //LocalView.SetForUser(0, "", VIDEO_SOURCE_TYPE.VIDEO_SOURCE_CAMERA);
        //// Start rendering local video.
        //LocalView.SetEnable(true);

        ChannelMediaOptions options = new ChannelMediaOptions();
        options.publishMicrophoneTrack.SetValue(true);
        // 自动订阅所有音频流。
        options.autoSubscribeAudio.SetValue(true);
        // 将频道场景设为直播。
        options.channelProfile.SetValue(CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING);
        // 将用户角色设为主播。
        options.clientRoleType.SetValue(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);
        // Join a channel.
        RtcEngine.JoinChannel(_token, _channelName, 0, options);
    }

    public void LeaveChannel()
    {
        // Leaves the channel.
        RtcEngine.LeaveChannel();
        // Disable the video modules.
        RtcEngine.DisableVideo();
        // Stops rendering the remote video.
        //RemoteView.SetEnable(false);
        //// Stops rendering the local video.
        //LocalView.SetEnable(false);
    }

    public void TurnOnGlobalMic()
    {
        //var options = new ChannelMediaOptions();
        //options.publishMicrophoneTrack.SetValue(true);
        //var nRet = RtcEngine.UpdateChannelMediaOptions(options);
        ////this.Log.UpdateLog("UpdateChannelMediaOptions: " + nRet);
        //AgoraView.LogText.text = "UpdateChannelMediaOptions: " + nRet;
        RtcEngine.MuteLocalAudioStream(false);
        RtcEngine.MuteAllRemoteAudioStreams(false);
        AgoraView.LogText.text = "打开全局麦";
    }

    public void TurnOffGlobalMic()
    {
        //var options = new ChannelMediaOptions();
        //options.publishMicrophoneTrack.SetValue(false);
        //var nRet = RtcEngine.UpdateChannelMediaOptions(options);
        ////this.Log.UpdateLog("UpdateChannelMediaOptions: " + nRet);
        //AgoraView.LogText.text = "UpdateChannelMediaOptions: " + nRet;
        RtcEngine.MuteLocalAudioStream(true);
        RtcEngine.MuteAllRemoteAudioStreams(true);
        AgoraView.LogText.text = "关闭全局麦";
    }

    //public void updateSpatialAudioPosition(float sourceDistance)
    //{
    //    // Set the coordinates in the world coordinate system.
    //    // This parameter is an array of length 3
    //    // The three values represent the front, right, and top coordinates
    //    float[] position = new float[] { sourceDistance, 4.0F, 0.0F };
    //    // Set the unit vector of the x axis in the coordinate system.
    //    // This parameter is an array of length 3,
    //    // The three values represent the front, right, and top coordinates
    //    float[] forward = new float[] { sourceDistance, 0.0F, 0.0F };
    //    // Update the spatial position of the specified remote user
    //    RemoteVoicePositionInfo remotePosInfo = new RemoteVoicePositionInfo(position, forward);
    //    var rc = localSpatial.UpdateRemotePosition((uint)remoteUid, remotePosInfo);
    //    Debug.Log("Remote user spatial position updated, rc = " + rc);
    //}

    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
        if (RtcEngine == null) return;
        RtcEngine.InitEventHandler(null);
        RtcEngine.LeaveChannel();
        RtcEngine.Dispose();
    }

    //返回0调用成功
    public int SetAudioRecvRange(float range)
    {
        return localSpatial.SetAudioRecvRange(range);
    }

    //返回0调用成功
    public int UpdatePlayerPositionInfo(int playerId, RemoteVoicePositionInfo positionInfo)
    {
        return localSpatial.UpdatePlayerPositionInfo(playerId, positionInfo);
    }

    //返回0调用成功
    public int EnableSpatialAudio(bool enabled)
    {
        return RtcEngine.EnableSpatialAudio(enabled);
    }

}

#region -- Agora Event ---

public class UserEventHandler : IRtcEngineEventHandler
{
    private readonly AgoraManager _sample;

    internal UserEventHandler(AgoraManager sample)
    {
        _sample = sample;
    }

    public override void OnError(int err, string msg)
    {
        //_sample.Log.UpdateLog(string.Format("OnError err: {0}, msg: {1}", err, msg));
        _sample.AgoraView.LogText.text = string.Format("OnError err: {0}, msg: {1}", err, msg);
    }

    public override void OnJoinChannelSuccess(RtcConnection connection, int elapsed)
    {
        _sample.m_SelfAgoraUId = connection.localUid;
        _sample.AgoraView.LogText.text = "you joined in channel, channelId = " + connection.channelId + ", uid = " + _sample.m_SelfAgoraUId;
    }

    public override void OnRejoinChannelSuccess(RtcConnection connection, int elapsed)
    {
        _sample.m_SelfAgoraUId = connection.localUid;
        _sample.AgoraView.LogText.text = "you rejoined in channel, channelId = " + connection.channelId + ", uid = " + _sample.m_SelfAgoraUId;
    }

    public override void OnLeaveChannel(RtcConnection connection, RtcStats stats)
    {
        //_audioSample.Log.UpdateLog("OnLeaveChannel");
        _sample.AgoraView.LogText.text = "OnLeaveChannel";
    }

    public override void OnClientRoleChanged(RtcConnection connection, CLIENT_ROLE_TYPE oldRole, CLIENT_ROLE_TYPE newRole, ClientRoleOptions newRoleOptions)
    {
        //_audioSample.Log.UpdateLog("OnClientRoleChanged");
        _sample.AgoraView.LogText.text = "OnClientRoleChanged";
    }

    public override void OnUserJoined(RtcConnection connection, uint uid, int elapsed)
    {
        // Setup remote view.
        //_sample.RemoteView.SetForUser(uid, connection.channelId, VIDEO_SOURCE_TYPE.VIDEO_SOURCE_REMOTE);
        // Save the remote user ID in a variable.
        _sample.AgoraView.LogText.text = "local uid = " + _sample.m_SelfAgoraUId + ", other joined uid = " + uid;
    }

    public override void OnUserOffline(RtcConnection connection, uint uid, USER_OFFLINE_REASON_TYPE reason)
    {
        //_sample.RemoteView.SetEnable(false);
        _sample.AgoraView.LogText.text = "user left, uid = " + uid;
    }
}
#endregion -- Agora Event ---