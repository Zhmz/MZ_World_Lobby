using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using FishNet.Managing;
using FishNet.Transporting;
using FishNet.Managing.Transporting;
using FishNet.Transporting.Multipass;
using FishNet.Transporting.Tugboat;
using GameFramework.Event;
using Tuwan.Const;

namespace World
{
    public enum AutoStartType
    {
        Disabled,
        Host,
        Server,
        Client
    }

    public class SuntailStartDemo : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Image blackScreenImage;
        [SerializeField] private Text blackScreenText1;
        [SerializeField] private Text blackScreenText2;
        [SerializeField] private Text hintText;
        [SerializeField] private float blackScreenDuration = 4f;
        [SerializeField] private float hintDuration = 14f;
        [SerializeField] private float fadingDuration = 3f;

        public GameObject UIGameObject;

        public AutoStartType StartType
        {
            get
            {
                return networkInit ? networkInit.StartType : AutoStartType.Disabled;
            }
        }

        //Private variables
        private bool screenTimerIsActive = true;
        private bool hintTimerIsActive = true;


        ///// <summary>
        ///// Found NetworkManager.
        ///// </summary>
        //private NetworkManager _networkManager;
        ///// <summary>
        ///// Current state of client socket.
        ///// </summary>
        //private LocalConnectionState _clientState = LocalConnectionState.Stopped;
        ///// <summary>
        ///// Current state of server socket.
        ///// </summary>
        //private LocalConnectionState _serverState = LocalConnectionState.Stopped;

        private TuwanNetworkInit networkInit;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            blackScreenImage.gameObject.SetActive(true);
            blackScreenText1.gameObject.SetActive(true);
            blackScreenText2.gameObject.SetActive(true);
            hintText.gameObject.SetActive(true);
            _audioMixer.SetFloat("soundsVolume", -80f);

            networkInit = FindObjectOfType<TuwanNetworkInit>();

            //if (StartType == AutoStartType.Host || StartType == AutoStartType.Server)
            //{
            //    networkInit.SwitchServer(true);
            //}
            //if (!Application.isBatchMode && (StartType == AutoStartType.Host || StartType == AutoStartType.Client))
            //{
            //    networkInit.SwitchClient(true);
            //}
        }

        //        public void OnClick_Server()
        //        {
        //            if (_networkManager == null)
        //                return;

        //            Multipass mp = _networkManager.TransportManager.GetTransport<Multipass>();


        //            if (_serverState != LocalConnectionState.Stopped)
        //                mp.StopConnection(true);
        //            else
        //                mp.StartConnection(true);
        //        }


        //        public void OnClick_Client()
        //        {
        //            if (_networkManager == null)
        //                return;

        //            Multipass mp = _networkManager.TransportManager.GetTransport<Multipass>();

        //#if UNITY_WEBGL && !UNITY_EDITOR
        //            mp.SetClientTransport<Bayou>();
        //#else
        //            mp.SetClientTransport<Tugboat>();
        //#endif


        //            if (_clientState != LocalConnectionState.Stopped)
        //                mp.StopConnection(false);
        //            else
        //                mp.StartConnection(false);
        //                //_networkManager.ClientManager.StartConnection("192.168.1.245", 7770);

        //        }

        private void OnEnable()
        {
            EventCenter.inst.AddEventListener<bool>((int)UIEventTag.EVENT_UI_SHOW_OR_HIDE_WORLD_UI, ShowOrHideWorldUI);
        }

        private void OnDisable()
        {
            EventCenter.inst.RemoveEventListener<bool>((int)UIEventTag.EVENT_UI_SHOW_OR_HIDE_WORLD_UI, ShowOrHideWorldUI);
        }

        void ShowOrHideWorldUI(bool active)
        {
            UIGameObject.SetActive(active);
        }

        private void Update()
        {
            //Black screen timer
            if (screenTimerIsActive)
            {
                blackScreenDuration -= Time.deltaTime;
                if (blackScreenDuration < 0)
                {
                    screenTimerIsActive = false;
                    blackScreenImage.CrossFadeAlpha(0, fadingDuration, false);
                    blackScreenText1.CrossFadeAlpha(0, fadingDuration, false);
                    blackScreenText2.CrossFadeAlpha(0, fadingDuration, false);
                    StartCoroutine(StartAudioFade(_audioMixer, "soundsVolume", fadingDuration, 1f));

                    blackScreenImage.gameObject.SetActive(false);
                    blackScreenText1.gameObject.SetActive(false);
                    blackScreenText2.gameObject.SetActive(false);
                }
            }

            //Hint text timer
            if (hintTimerIsActive)
            {
                hintDuration -= Time.deltaTime;
                if (hintDuration < 0)
                {
                    hintTimerIsActive = false;
                    hintText.CrossFadeAlpha(0, fadingDuration, false);
                }
                }
        }

        //Sound fading
        public static IEnumerator StartAudioFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
        {
            float currentTime = 0;
            float currentVol;
            audioMixer.GetFloat(exposedParam, out currentVol);
            currentVol = Mathf.Pow(10, currentVol / 20);
            float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
                audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
                yield return null;
            }
            yield break;
        }
    }
}