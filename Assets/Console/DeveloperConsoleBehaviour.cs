using SlowDev.Console;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace SlowDev.Console
{
    public class DeveloperConsoleBehaviour : MonoBehaviour
    {
        [SerializeField] private string prefix = string.Empty;
        [SerializeField] private ConsoleCommand[] commands = new ConsoleCommand[0];

        [Header("UI")]
        [SerializeField] private GameObject uiCanvas = null;
        [SerializeField] private TMP_InputField inputField = null;
        [SerializeField] private TMP_Text log;

        [Header("Hotkey")]
        [SerializeField] InputAction consoleInput;

        public static DeveloperConsoleBehaviour instance;


        private DeveloperConsole developerConsole;

        private DeveloperConsole DeveloperConsole
        {
            get
            {
                if (developerConsole != null) { return developerConsole; }
                return developerConsole = new DeveloperConsole(prefix, commands);
            }
        }

        private void OnEnable()
        {
            consoleInput.Enable();
            consoleInput.performed += Toggle;
        }

        private void OnDisable()
        {
            consoleInput.Disable();
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public void Toggle(InputAction.CallbackContext context)
        {
            if(!context.action.triggered) { return; }

            if (uiCanvas.activeSelf)
            {
                uiCanvas.SetActive(false);
            }
            else
            {
                uiCanvas.SetActive(true);
                inputField.ActivateInputField();
            }
        }

        public void LogMessage(string sender, string msg)
        {
            if (log.text != string.Empty) log.text += "\n";

            log.text += $"{sender}: {msg}";
        }

        public void ProcessCommand(string inputValue)
        {
            DeveloperConsole.ProcessCommand(inputValue);

            inputField.text = string.Empty;
        }
    }
}