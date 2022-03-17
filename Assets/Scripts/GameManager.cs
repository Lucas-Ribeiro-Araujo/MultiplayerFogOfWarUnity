using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using SlowDev.Console;
using Unity.Netcode.Transports.UNET;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameplayManager GameplayManager;
    public Canvas Canvas;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        DontDestroyOnLoad(this.gameObject);
    }


    public void SetIp(string Ip)
    {
    }

    public void StartServer()
    {
       if (NetworkManager.Singleton.StartServer())
        {
            DeveloperConsoleBehaviour.instance.LogMessage("system", "Started as server");
        }
       else
        {
            DeveloperConsoleBehaviour.instance.LogMessage("system", "Failed to start as server");
        }
    }

    public void StartClient()
    {
        if (NetworkManager.Singleton.StartClient())
        {
            DeveloperConsoleBehaviour.instance.LogMessage("system", "Started as client");
        }
        else
        {
            DeveloperConsoleBehaviour.instance.LogMessage("system", "Failed to start as client");
        }
    }
}
