using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using Easy.MessageHub;

public class InGameBridge : MonoBehaviour
{
    bool isAuth = false;
    IMessageHub hub;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Init(IMessageHub hub)
    {
        this.hub = hub;
    }

    /*
        public void ReceiveFromFrontEnd(string json)
        {
            UnityEngine.Debug.Log($"[InGameBridge] {json}");
            SendAuth(json);
            if (!isAuth)
            {
                hub.Publish<ContinueEvent>(new ContinueEvent());
            }
        }
    */

    public void ReceiveAuthFromFrontEnd(string json)
    {
        UnityEngine.Debug.Log($"[InGameBridge] {json}");
        SendAuth(json);
        if (!isAuth)
        {
            hub.Publish<ContinueEvent>(new ContinueEvent());
        }
    }

    public void ReceiveSaveFromFrontEnd(string json)
    {
        UnityEngine.Debug.Log($"[InGameBridge] {json}");
        //ParseDailySave(json);             //No Save in Daily
    }

    [Conditional("AUTH")]
    public void SendAuth(string json)
    {
        UnityEngine.Debug.Log("[InGameBridge] sending auth request");
        isAuth = true;
        ParseDailyRequest(json);
    }

    public void ParseDailyRequest(string json)
    {
        var dailyRequest = JsonUtility.FromJson<DailyRequest>(json);
        UnityEngine.Debug.Log($"[InGameBridge] accessToken: {dailyRequest.accessToken}");
        UnityEngine.Debug.Log($"[InGameBridge] authUrl: {dailyRequest.authUrl}");

        if (dailyRequest.accessToken == "") return;
        var evt = new AuthEvent(dailyRequest.authUrl, dailyRequest.accessToken);
        hub.Publish<AuthEvent>(evt);
    }
}

[System.Serializable]
public class AuthEvent
{
    public string endpoint;
    public string token;

    public AuthEvent(string endpoint, string token)
    {
        this.endpoint = endpoint;
        this.token = token;
    }
}
