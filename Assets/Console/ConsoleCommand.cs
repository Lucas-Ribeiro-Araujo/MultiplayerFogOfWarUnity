using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlowDev.Console
{
    public abstract class ConsoleCommand : ScriptableObject, IConsoleCommand
    {
        [SerializeField] private string commandWord = string.Empty;

        public string CommandWord => commandWord;


        public abstract bool Process(string[] args);
    }
}
