using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FishNet.Managing;
using FishNet.Transporting;
using FishNet.Transporting.Multipass;
using FishNet.Transporting.Tugboat;

namespace World
{
    public enum EServerIP
    {
        LocalHost,
        MingZhen,
        ZhiSong,
        FrostBlade,
        DevServer,
        XiangKui
    }

    public class TuwanNetworkInit : MonoBehaviour
    {
        /// <summary>
        /// Found NetworkManager.
        /// </summary>
        [HideInInspector]
        public NetworkManager _networkManager;
        NetworkManager NetworkManager
        {
            get
            {
                if (_networkManager == null)
                {
                    _networkManager = FindObjectOfType<NetworkManager>();
                    Tugboat tugboat = _networkManager.GetComponent<Tugboat>();
                    if (tugboat != null)
                    {
                        if (ServerIP == EServerIP.LocalHost)
                        {
                            ClientAddress = "localhost";
                        }

                        if (StartType == AutoStartType.Client)
                        {
                            
                            if (ServerIP == EServerIP.MingZhen)
                            {
                                ClientAddress = "172.18.2.20";
                            }
                            else if (ServerIP == EServerIP.ZhiSong)
                            {
                                ClientAddress = "172.18.2.65";
                            }
                            else if (ServerIP == EServerIP.FrostBlade)
                            {
                                ClientAddress = "192.168.1.240";
                            }
                            else if (ServerIP == EServerIP.DevServer)
                            {
                                ClientAddress = "192.168.1.245";
                            } else if(ServerIP == EServerIP.XiangKui)
                            {
                                ClientAddress = "192.168.1.29";
                            }
                            
                        }
                        tugboat.SetClientAddress(ClientAddress);
                    }
                }
                return _networkManager;
            }
        }

        [HideInInspector]
        public LocalConnectionState ClientState = LocalConnectionState.Stopped;
        [HideInInspector]
        public LocalConnectionState ServerState = LocalConnectionState.Stopped;


        public AutoStartType StartType = AutoStartType.Disabled;
        public EServerIP ServerIP = EServerIP.LocalHost;
        private string ClientAddress;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if (StartType == AutoStartType.Host || StartType == AutoStartType.Server)
            {
                SwitchServer(true);
            }
            //if (!Application.isBatchMode && (StartType == AutoStartType.Host || StartType == AutoStartType.Client))
            //{
            //    SwitchClient(true);
            //}
        }

        public void SwitchServer(bool start)
        {
            if (NetworkManager == null)
            {
                return;
            }

            Multipass mp = NetworkManager.TransportManager.GetTransport<Multipass>();

            if (start)
            {
                if (ServerState == LocalConnectionState.Stopped)
                {
                    bool result = mp.StartConnection(true);
                    if (result)
                    {
                        ServerState = LocalConnectionState.Started;
                    }
                }
            }
            else
            {
                if (ServerState == LocalConnectionState.Started)
                {
                    bool result = mp.StopConnection(true);
                    if (result)
                    {
                        ServerState = LocalConnectionState.Stopped;
                    }
                }
            }
        }

        public void SwitchClient(bool start)
        {
            if (NetworkManager == null)
            {
                return;
            }

            Multipass mp = NetworkManager.TransportManager.GetTransport<Multipass>();

#if UNITY_WEBGL && !UNITY_EDITOR
        mp.SetClientTransport<Bayou>();
#else
            mp.SetClientTransport<Tugboat>();
#endif

            if (start)
            {
                if (ClientState == LocalConnectionState.Stopped)
                {
                    Debug.Log("ClientState == Stopped");
                    bool result = mp.StartConnection(false);
                    if (result)
                    {
                        Debug.Log("Succeed to Set ClientState as Started");
                        ClientState = LocalConnectionState.Started;
                    }
                    else
                    {
                        Debug.Log("Fail to Set ClientState as Started");
                    }
                }
                else
                {
                    Debug.Log("Fail to start Client because ClientState == Started");
                }
            }
            else
            {
                if (ClientState == LocalConnectionState.Started)
                {
                    Debug.Log("ClientState == Started");
                    bool result = mp.StopConnection(false);
                    if (result)
                    {
                        Debug.Log("Succeed to Set ClientState as Stopped");
                        ClientState = LocalConnectionState.Stopped;
                    }
                    else
                    {
                        Debug.Log("Fail to Set ClientState as Stopped");
                    }
                }
                else
                {
                    Debug.LogError("Fail to stop Client because ClientState == Stopped");
                }
            }
        }
    }
}