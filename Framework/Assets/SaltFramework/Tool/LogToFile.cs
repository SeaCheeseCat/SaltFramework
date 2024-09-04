using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LogToFile : MonoBehaviour
{
    private StreamWriter logWriter;

    void Start()
    {
#if !UNITY_EDITOR
        logWriter = File.CreateText("Log.log");
        Application.logMessageReceived += LogToFileHandler;
#endif
    }

    void LogToFileHandler(string logString, string stackTrace, LogType type)
    {
#if !UNITY_EDITOR
        string currentDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        logWriter.WriteLine("[" + currentDate + "][" + type + "] " + logString);
        logWriter.WriteLine(stackTrace);
        logWriter.Flush();  
#endif
    }

    void OnDisable()
    {
#if !UNITY_EDITOR
        logWriter.Close();
        Application.logMessageReceived -= LogToFileHandler;
#endif
    }
}
