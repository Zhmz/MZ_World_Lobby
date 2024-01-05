using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tuwan;
using Tuwan.Const;
using Tuwan.Proto;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    //当前模型缓存的玩家信息
    [SyncVar(OnChange = nameof(CurPlayerInfo_OnChange))]
    public UserInfoResponsedData CurPlayerInfo;
    [ServerRpc]
    public void SetCurPlayerInfo(UserInfoResponsedData info)
    {
        //客户端调服务器，服务器端执行下面这句，服务器端数据修改
        //再由SyncObject装饰器，服务器的修改会同步给每一个客户端
        CurPlayerInfo = info;
    }

    public LayerMask clickableLayer;

    RaycastHit hit;
    //客户端功能
    public List<AssetReference> prefabList = new List<AssetReference>() { };

    public List<AssetReference> avatarList = new List<AssetReference>() { };

    [SyncObject]
    private readonly SyncDictionary<NetworkConnection, int> prefabIndexs = new SyncDictionary<NetworkConnection, int>();

    private bool _haveInitPrefab = false;

    [SyncVar]
    private uint agoraUId;

    [ServerRpc]
    public void SetAgoraUid(uint uid)
    {
        this.agoraUId = uid;
    }

    private AgoraManager agoraMgr;


    //指定玩家模型编号
    public static int OwnerAssignedPlayerIndex = -1;


    public Canvas PlayerInfoCanvas;
    public Text PlayerNameText;
    public Image PlayerLevelImage;

    private Color OwnerPlayerNameColor = new Color(1, 200.0f / 255, 0, 1);
    private Color OtherPlayerNameColor = new Color(1, 1, 1, 1);

    public void Awake()
    {
        this.prefabIndexs.OnChange += PrefabIndexs_OnChange;

        GameObject agoraGO = GameObject.Find("Controller");
        if (agoraGO != null)
        {
            agoraMgr = agoraGO.GetComponent<AgoraManager>();
        }
    }

    private void CurPlayerInfo_OnChange(UserInfoResponsedData value1, UserInfoResponsedData value2, bool asServer)
    {
        Debug.Log((asServer ? "Server: " : "Client: ") + value2.nickname);
        UpdatePlayerInfoView(value2);

        //因为现在两个场景同时加载了，所以先不向lobby传递消息修改位置
        //EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_TUWAN_PLAYER_INFO_SYNCED, CurPlayerInfo.uid);
    }

    private void OnEnable()
    {
        EventCenter.inst.AddEventListener<int, Transform>((int)UIEventTag.EVENT_UI_MOVE_PLAYER_TO_WHEAT_POSITION, OnMovePlayerToWheatPosition);
        EventCenter.inst.AddEventListener<int, Vector3>((int)UIEventTag.EVENT_UI_MOVE_AUDIENCE_TO_LOBBY_RANDOM_POSITION, MoveAudiencePosition);
    }

    private void OnDisable()
    {
        EventCenter.inst.RemoveEventListener<int, Transform>((int)UIEventTag.EVENT_UI_MOVE_PLAYER_TO_WHEAT_POSITION, OnMovePlayerToWheatPosition);
        EventCenter.inst.RemoveEventListener<int, Vector3>((int)UIEventTag.EVENT_UI_MOVE_AUDIENCE_TO_LOBBY_RANDOM_POSITION, MoveAudiencePosition);
    }

    private void PrefabIndexs_OnChange(SyncDictionaryOperation op, NetworkConnection key, int value, bool asServer)
    {
        if (op == SyncDictionaryOperation.Add || op == SyncDictionaryOperation.Set)
        {
            this.InitPrefab(value);
        }
    }

    private void InitPrefab(int prefabIndex)
    {
        if (!this._haveInitPrefab)
        {
            this._haveInitPrefab = true;

            Transform characterBase = this.transform.Find("CharacterBase");

            this.prefabList[prefabIndex].InstantiateAsync(characterBase).Completed += (AsyncOperationHandle<GameObject> obj) =>
            {

                this.prefabList[prefabIndex].ReleaseAsset();

                this.avatarList[prefabIndex].LoadAssetAsync<Avatar>().Completed += (AsyncOperationHandle<Avatar> obj) =>
                {
                    Animator animator = this.GetComponent<Animator>();

                    animator.avatar = obj.Result;

                    animator.Rebind();

                    this.avatarList[prefabIndex].ReleaseAsset();

                    Debug.Log("InitPrefab:" + this.IsOwner + "," + prefabIndex);

                    SyncTuwanCurInfo();
                };
            };
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void randomPrefab(NetworkConnection connection)
    {
        //没有加载过的话，随机一个
        if (this.IsOwner)
        {
            if (OwnerAssignedPlayerIndex < 0)
            {
                OwnerAssignedPlayerIndex = Random.Range(0, this.prefabList.Count);
            }
            Debug.Log("Owner Index = " + OwnerAssignedPlayerIndex);
            this.prefabIndexs[connection] = OwnerAssignedPlayerIndex;
        }
        else
        {
            int index = Random.Range(0, this.prefabList.Count);
            this.prefabIndexs[connection] = index;
            Debug.Log("OtherClient Index = " + index);
        }
    }


    public override void OnStartClient()
    {
        base.OnStartClient();

        if (this.IsOwner)
        {
            this.randomPrefab(this.Owner);


            this.AddComponent<CameraController>();


            //临时方案，避免报异常·应该有更合理的方法
            AudioListener audioListener = GameObject.Find("TuwanFramework")?.GetComponent<AudioListener>();
            if (audioListener != null)
            {
                audioListener.enabled = false;
            }


            this.AddComponent<AudioListener>();


            Bounds spawnArea = GameObject.Find("SpawnArea").GetComponent<MeshRenderer>().bounds;

            Rigidbody rigidbody = this.GetComponent<Rigidbody>();

            rigidbody.MovePosition(new Vector3(Random.Range(spawnArea.min.x, spawnArea.max.x), spawnArea.center.y, Random.Range(spawnArea.min.z, spawnArea.max.z)));
        }
    }


    private void Update()
    {
        // if (Input.GetMouseButton(0))
        // {
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     if (Physics.Raycast(ray, out hit, 50, clickableLayer.value)) //如果碰撞检测到物体
        //     {
        //         Debug.Log("111");//点击指定模型后需要进行的操作
        //         GameEntry.UI.OpenUIForm(UIFormId.PlayerInfoForm, CurPlayerInfo);
        //     }
        // }



        SyncAgoraUId();
    }

    void SyncAgoraUId()
    {
        if (agoraMgr == null || agoraMgr.m_SelfAgoraUId == 0)
        {
            return;
        }
        //自己就绪
        if (IsOwner)
        {
            //且player未赋值过
            if (this.agoraUId == 0)
            {
                SetAgoraUid(agoraMgr.m_SelfAgoraUId);
            }
            else
            {
                agoraMgr.UpdateSelfPosition(this.transform);
            }
        }
        else
        {
            if (this.agoraUId > 0)
            {
                agoraMgr.UpdateRemotePosition(agoraUId, this.transform.position, this.transform.forward);
            }
        }
    }

    void SyncTuwanCurInfo()
    {
        if (IsOwner)
        {
            if (CurPlayerInfo == null)
            {
                //同步tuwan的uid
                var userInfo = Store.Config.UserInfo;
                SetCurPlayerInfo(userInfo);
            }
        }
        else
        {

        }
    }

    void UpdatePlayerInfoView(UserInfoResponsedData info)
    {
        PlayerInfoCanvas.worldCamera = Camera.main;
        PlayerInfoCanvas.GetOrAddComponent<LookAtCamera>();

        if (info != null)
        {
            PlayerNameText.color = this.IsOwner ? OwnerPlayerNameColor : OtherPlayerNameColor;
            PlayerNameText.text = info.nickname;
        }
    }
    public static bool Raycast(bool isCheckHitUI = true)
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        {
            if (isCheckHitUI)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return false;
                }
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#else
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (isCheckHitUI)
                {
                    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    {
                        return false;
                    }
                }

                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // or hit.collider.transform;
                return true;

            }
        }
        return false;
    }
    private void OnMouseUpAsButton()
    {
        // if (!Raycast(true))
        // {
        //     return;
        // }
        GameEntry.UI.OpenUIForm(UIFormId.PlayerInfoForm, CurPlayerInfo);
        // Debug.LogError("this player nickname = " + CurPlayerInfo.nickname);
    }

    private void OnMovePlayerToWheatPosition(int uid, Transform wheatTrans)
    {
        if (CurPlayerInfo == null)
        {
            return;
        }

        if (IsClient)
        {
            Debug.LogError("OnMovePlayerToWheatPosition CurPlayerUid = " + CurPlayerInfo.uid + ", uid = " + uid);
        }

        if (CurPlayerInfo.uid == uid)
        {
            transform.position = wheatTrans.position;
            transform.rotation = wheatTrans.rotation;
        }
    }

    void MoveAudiencePosition(int uid, Vector3 pos)
    {
        if (CurPlayerInfo == null)
        {
            return;
        }

        if (IsClient)
        {
            Debug.LogError("MoveAudiencePosition CurPlayerUid = " + CurPlayerInfo.uid + ", uid = " + uid);
        }

        if (CurPlayerInfo.uid == uid)
        {
            Rigidbody rigidbody = this.GetComponent<Rigidbody>();
            rigidbody.MovePosition(pos);
        }
    }
}
