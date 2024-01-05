using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Core.Scripts.Editor
{
    public class AudioListenerFinder : EditorWindow
    {
        [MenuItem("Tools/Find AudioListeners")]
        static void FindAudioListeners()
        {
            AudioListener[] audioListeners = UnityEngine.Object.FindObjectsOfType<AudioListener>();

            if (audioListeners.Length > 1)
            {
                Debug.LogError("场景中存在多个AudioListener，请确保只有一个。");

                foreach (AudioListener audioListener in audioListeners)
                {
                    Debug.LogError("AudioListener 所在 GameObject：" + audioListener.gameObject.name, audioListener.gameObject);
                }
            }
            else if (audioListeners.Length == 1)
            {
                Debug.Log("找到一个 AudioListener：" + audioListeners[0].gameObject.name, audioListeners[0].gameObject);
            }
            else
            {
                Debug.LogWarning("未找到 AudioListener");
            }
        }
    }
}
