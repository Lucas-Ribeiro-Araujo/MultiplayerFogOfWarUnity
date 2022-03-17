using SlowDev.Console;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlowDev.Console
{
    [CreateAssetMenu(fileName = "New Log Command", menuName = "Utilities/Developer/Log Command")]
    public class LogCommand : ConsoleCommand
    {
        public override bool Process(string[] args)
        {
            string logText = string.Join(" ", args);

            Debug.Log(logText);

            DeveloperConsoleBehaviour.instance.LogMessage("system",$"message {logText}, logged on unity console");

            return true;
        }
    }
}
