using SlowDev.Console;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SlowDev.Console
{
    [CreateAssetMenu(fileName = "New Ip Command", menuName = "Utilities/Developer/Ip Command")]
    public class IpCommand : ConsoleCommand
    {
        public override bool Process(string[] args)
        {
            switch (args[0])
            {
                case "server": GameManager.instance.StartServer();
                    break;
                case "client": GameManager.instance.StartClient();
                    break;
            }

            return true;
        }
    }
}
