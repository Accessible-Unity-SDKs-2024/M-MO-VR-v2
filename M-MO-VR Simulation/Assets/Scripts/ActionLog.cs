using PlasticGui.WebApi.Responses;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionLog
{
    public string loggedAction;
    public DateTime loggedTime;
    public Vector3 loggedPosition;
    public  static Dictionary<int, string> ACTIONS = new Dictionary<int, string>()
        {
            { 0, "SonarCast" },
            { 1, "SonarHit" },
            { 2, "HapticImpulse" }
        };

    public ActionLog(string loggedAction, Vector3 loggedPosition)
    {
        this.loggedAction = loggedAction;
        this.loggedTime = DateTime.Now;
        this.loggedPosition = loggedPosition;
    }

    public static void LogAction(string loggedAction, Vector3 loggedPosition)
    {
        ActionLog action = new(loggedAction, loggedPosition);

        string json = JsonUtility.ToJson(action);
        System.IO.File.AppendAllText("Assets/Tests/log.json", json + "\n");
    }
}
