using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using Easy.MessageHub;

public class APIManager : MonoBehaviour
{
    private IMessageHub hub;

    public void Init(IMessageHub messageHub)
    {
        hub = messageHub;
        hub.Subscribe<AuthEvent>(OnAuthenticationEvent);
    }

    public void Send(string cmd, string json)
    {
        CallAPI(cmd, json);
    }

    [DllImport("__Internal")]
    private static extern void CallAPI(string cmd, string json);

    private void OnAuthenticationEvent(AuthEvent evt)
    {
        UnityEngine.Debug.Log("Trying to authenticate ...");
        StartCoroutine(_GetRequest(evt.endpoint, evt.token));
    }

    IEnumerator _GetRequest(string uri, string token)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.SetRequestHeader("Authorization", token);
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            DailyResponse response = null;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    response = new DailyResponse(false, false, false, false);
                    Debug.LogError($"=====\n{uri}\nError: {webRequest.error}\n=====");
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    response = new DailyResponse(false, false, false, false);
                    Debug.LogError($"=====\n{uri}\nHTTP Error: {webRequest.error}\n=====");
                    break;
                case UnityWebRequest.Result.Success:
                    response = new DailyResponse(false, false, false, true);
                    Debug.Log($"=====\n{uri}\nReceived: {webRequest.downloadHandler.text}\n=====");
                    hub.Publish<ContinueEvent>(new ContinueEvent());
                    break;
            }
            Send("Auth", JsonUtility.ToJson(response));
        }
    }
}

[System.Serializable]
public class DailyRequest
{
    public string accessToken = ""; // Access token
    public string authUrl = ""; // Auth url
}

[System.Serializable]
public class DailyResponse
{
    public bool onClose = false; // On Close button pressed
    public bool onShare = false; // Share button being pressed
    public bool onLoadEnd = false; // A page has been loaded
    public bool onAuthSuccess = false; // Retry when false

    public DailyResponse(bool onClose, bool onShare, bool onLoadEnd, bool onAuthSuccess)
    {
        this.onClose = onClose;
        this.onShare = onShare;
        this.onLoadEnd = onLoadEnd;
        this.onAuthSuccess = onAuthSuccess;
    }
}

public class ContinueEvent { }

