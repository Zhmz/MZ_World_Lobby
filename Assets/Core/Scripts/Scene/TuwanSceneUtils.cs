using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tuwan
{
    public static class TuwanSceneUtils
    {

        public static Dictionary<string, string> SceneNameToPathDic = new Dictionary<string, string>()
        {
            {"Launch","Assets/Launch.unity"},
            {"Login","Assets/Home/Scene/Login.unity"},
            {"Home","Assets/Home/Scene/Home.unity"},
            {"World","Assets/World/Scene/World.unity"},
            {"WorldOther","Assets/World/Suntail Village/Demo/WorldOther.unity"},
            {"Lobby","Assets/Lobby/Scene/Lobby.unity"},
            {"LobbyOther","Assets/Lobby/Scene/Scenes/LobbyOther.unity"}
        };

        public static void UnloadScene(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }

        public static void LoadScene(string sceneName, Action<AsyncOperation> action = null)
        {
            if (!SceneNameToPathDic.ContainsKey(sceneName))
            {
                return;
            }

            AsyncOperation loadedScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            loadedScene.completed += (AsyncOperation operation) =>
            {

                foreach (string scene in SceneNameToPathDic.Keys)
                {
                    if (scene == "WorldOther" || scene == "LobbyOther")
                    {
                        continue;
                    }
                    if (scene != sceneName)
                    {
                        Scene activeScene = SceneManager.GetSceneByName(scene);
                        if (activeScene != null && activeScene.isLoaded)
                        {
                            SceneManager.UnloadSceneAsync(activeScene);
                        }
                    }
                }

                if (action != null)
                {
                    action(operation);
                };
            };




        }

        public static void AdjustScreenToLandscape()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;

            //调整UI的尺寸
            CanvasScaler scaler = GameObject.Find("Canvas").GetComponent<CanvasScaler>();
            scaler.referenceResolution = new Vector2(1334, 750);
        }

        public static void AdjustScreenToPortrait()
        {
            Screen.orientation = ScreenOrientation.Portrait;
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;

            //调整UI的尺寸
            CanvasScaler scaler = GameObject.Find("Canvas").GetComponent<CanvasScaler>();
            scaler.referenceResolution = new Vector2(750, 1334);
        }
    }
}
