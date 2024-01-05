using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FishNet.Managing.Scened;
using FishNet.Object;
using GameFramework.Event;
using Tuwan;
using Tuwan.Const;
using UnityEngine;
using World;

public class TransportTrigger : NetworkBehaviour
{
    private Player curPlayer;

    private void OnTriggerEnter(Collider other)
    {
        //Server
        curPlayer = GetPlayerOwnedObject(other);
        if (curPlayer == null)
        {
            return;
        }

        if (IsClient)
        {
            if (curPlayer.IsOwner)
            {
                this.SceneManager.OnLoadEnd += SceneManager_OnLoadEnd;
            }
        }

        if (IsServer)
        {
            SceneLookupData lookupData = new SceneLookupData("Lobby");
            SceneLoadData sld = new SceneLoadData(lookupData)
            {
                /* Set automatically unload to false
                 * so the server does not unload this scene when
                 * there are no more connections in it. */
                Options = new LoadOptions()
                {
                    AutomaticallyUnload = false
                },
                /* Also move the client object to the new scene. 
                * This step is not required but may be desirable. */
                MovedNetworkObjects = new NetworkObject[] { curPlayer.NetworkObject },
                //Load scenes as additive.
                ReplaceScenes = ReplaceOption.None,
                //Set the preferred active scene so the client changes active scenes.
                PreferredActiveScene = lookupData,
            };

            base.SceneManager.LoadConnectionScenes(curPlayer.Owner, sld);

            base.SceneManager.OnLoadEnd += OnLoadEmptyLobby;
        }
    }

    void OnLoadEmptyLobby(SceneLoadEndEventArgs obj)
    {
        base.SceneManager.OnLoadEnd -= OnLoadEmptyLobby;

        SceneUnloadData sceneUnloadData = new SceneUnloadData("WorldOther")
        {
            Options = new UnloadOptions()
            {
                Mode = UnloadOptions.ServerUnloadMode.KeepUnused
            }
        };

        base.SceneManager.UnloadConnectionScenes(curPlayer.Owner, sceneUnloadData);

        base.SceneManager.OnUnloadEnd += OnUnloadedWorldOther;
    }


    void OnUnloadedWorldOther(SceneUnloadEndEventArgs obj)
    {
        base.SceneManager.OnUnloadEnd -= OnUnloadedWorldOther;

        SceneLookupData lookupData = new SceneLookupData("LobbyOther");
        SceneLoadData sld = new SceneLoadData(lookupData)
        {
            /* Set automatically unload to false
             * so the server does not unload this scene when
             * there are no more connections in it. */
            Options = new LoadOptions()
            {
                AutomaticallyUnload = false
            },
            /* Also move the client object to the new scene. 
            * This step is not required but may be desirable. */
            MovedNetworkObjects = new NetworkObject[] { curPlayer.NetworkObject },
            //Load scenes as additive.
            ReplaceScenes = ReplaceOption.None,
            //Set the preferred active scene so the client changes active scenes.
            PreferredActiveScene = lookupData,
        };

        base.SceneManager.LoadConnectionScenes(curPlayer.Owner, sld);
    }

    private void SceneManager_OnLoadEnd(SceneLoadEndEventArgs obj)
    {
        if (curPlayer == null || curPlayer.CurPlayerInfo == null)
        {
            return;
        }

        Debug.LogError("SceneManager_OnLoadEnd isOwner = " + curPlayer.IsOwner);

        //每个客户端都要移动每个player的位置
        EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_FROM_WORLD_ENTER_LOBBY, curPlayer.CurPlayerInfo.uid);
        //但是每个客户端只处理自己的UI展示
        EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_ENTER_LOBBY_FORM, "LobbyOther");
    }

    private Player GetPlayerOwnedObject(Collider other)
    {
        /* When an object exits this trigger unload the level for the client. */
        Player player = other.GetComponent<Player>();
        //Not the player object.
        if (player == null)
            return null;
        //No owner??
        if (!player.Owner.IsActive)
            return null;

        return player;
    }

    private void OnTriggerExit(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {

    }
}
