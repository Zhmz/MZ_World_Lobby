using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Managing.Scened;
using FishNet.Object;
using GameFramework.Event;
using GameKit.Utilities.Types;
using Tuwan;
using Tuwan.Const;
using Tuwan.SceneLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace World
{
    public class WorldStarter : NetworkBehaviour
    {
        //public Scene WorldScene;
        [SerializeField, Scene]
        private string WorldScene;


        TuwanNetworkInit networkInit;

        private void Start()
        {
            networkInit = FindObjectOfType<TuwanNetworkInit>();
            AutoStartType StartType = networkInit.StartType;

            Debug.LogError("WorldStarter Start");
            if (!Application.isBatchMode)
            {
                if (StartType == AutoStartType.Host)
                {
                    Debug.LogError("Server SwitchClient True");
                    networkInit.SwitchClient(true);
                    //提前显示LobbyForm
                    GameEntry.UI.OpenUIForm(UIFormId.LobbyForm, "WorldOther");
                }
                else if (StartType == AutoStartType.Client)
                {
                    TuwanSceneUtils.LoadScene("WorldOther", OnLoadSceneWorldOther);
                }
            }

            TuwanSceneUtils.AdjustScreenToLandscape();

#if UNITY_EDITOR
            ScreenUtils.SetLandscapeResolution();
#endif
        }

        private void OnLoadSceneWorldOther(AsyncOperation obj)
        {
            /*var progress = obj.progress;
            var lodingForm =  GameEntry.UI.GetUIForm(UIFormId.SceenLoading);
            if(lodingForm) {
                var sceneLoaderCanvas = lodingForm.GetComponent<SceneLoaderCanvas>();
                if(sceneLoaderCanvas) {
                    sceneLoaderCanvas.SetProgress(progress);
                }
            }*/
            if (obj.isDone)
            {
                
                // TuwanSceneUtils.UnloadScene("World");


                Debug.LogError("Client SwitchClient True");
                networkInit.SwitchClient(true);

                //提前显示LobbyForm
                GameEntry.UI.OpenUIForm(UIFormId.LobbyForm, "WorldOther");
                
            }
        }


        public override void OnStartClient()
        {
            base.OnStartClient();

            Debug.LogError("OnStartClient");

            //if (base.IsServer)
            //{
            //    /* Create a lookup handle using this objects scene.
            //     * This is one of many ways FishNet knows what scene to load
            //     * for the clients. */
            //    SceneLookupData lookupData = new SceneLookupData(WorldScene);
            //    SceneLoadData sld = new SceneLoadData(lookupData)
            //    {
            //        /* Set automatically unload to false
            //         * so the server does not unload this scene when
            //         * there are no more connections in it. */
            //        Options = new LoadOptions()
            //        {
            //            AutomaticallyUnload = false
            //        },
            //        /* Also move the client object to the new scene. 
            //        * This step is not required but may be desirable. */
            //        //MovedNetworkObjects = new NetworkObject[] { player.NetworkObject },
            //        //Load scenes as additive.
            //        ReplaceScenes = ReplaceOption.None,
            //        //Set the preferred active scene so the client changes active scenes.
            //        PreferredActiveScene = lookupData,
            //    };

            //    base.SceneManager.LoadConnectionScenes(this.Owner, sld);
            //}
        }
    }
}
