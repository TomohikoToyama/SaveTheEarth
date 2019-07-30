using UnityEngine;
using UnityEngine.UI;

public class CatchDebugLog : MonoBehaviour {

    private Text debugText;

    private void Awake()
    {
        debugText = this.GetComponent<Text>();
        if (debugText == null)
        {
            gameObject.AddComponent<Text>();
        }
    }
    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }
    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }
    private void HandleLog(string logText, string stackTrace, LogType logType)
    {
        if (string.IsNullOrEmpty(logText))
            return;
        if (logType == LogType.Log)
        {
            //debugText.text += logText + System.Environment.NewLine;
        }
        else if (logType == LogType.Error || logType == LogType.Exception)
        {
            debugText.text += string.Format("<color=red>{0}</color>", logText + System.Environment.NewLine + stackTrace);
            Application.logMessageReceived -= HandleLog;
        }

    }

    private void CloseLog()
    {

    }


}