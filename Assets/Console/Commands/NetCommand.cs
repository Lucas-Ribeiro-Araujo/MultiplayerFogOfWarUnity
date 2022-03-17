﻿using SlowDev.Console;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SlowDev.Console
{
    [CreateAssetMenu(fileName = "New Net Command", menuName = "Utilities/Developer/Net Command")]
    public class NetCommand : ConsoleCommand
    {
        public override bool Process(string[] args)
        {
            switch (args[0])
            {
                case "server": GameManager.instance.StartServer();
                    break;
                case "client": GameManager.instance.StartClient();
                    break;
                case "host": NetworkManager.Singleton.StartHost();
                    break;
            }

            return true;
        }
    }
}
