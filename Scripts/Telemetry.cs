using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Telemetry : MonoBehaviour
{
    private static string url = "https://hts-db.vercel.app/telemetry";
    private static Telemetry instance;
    private static string userId;

    private void Awake() {
        if (instance == null) {
            userId = Guid.NewGuid().ToString();
            instance = this;
        }
    }

    public static void Send(string name, string value, double choiceTime) {
        Debug.Log("choice time is: " + choiceTime);
        instance.InstanceSend(name, value, choiceTime);
    }

    private void InstanceSend(string name, string value, double choiceTime) {
        StartCoroutine(SendRequest(new TelemetryData() {
            name = name,
            value = value + " " + choiceTime,
            choiceTime = choiceTime,
            userId = userId
        }));
    }
    private static IEnumerator SendRequest(TelemetryData data) {
        string jsonData = JsonUtility.ToJson(data);
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
         request.Dispose();
    }
}

[System.Serializable]
public class TelemetryData {
    public string name;
    public string value;
    public double choiceTime;
    public string userId;
}
