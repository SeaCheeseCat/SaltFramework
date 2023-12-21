using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpUtil : MonoBehaviour
{
    public static HttpUtil Instacne 
    {
        get {
            if (instacne == null) 
            {
                GameObject clone = new GameObject();
                clone.name = "HttpObj";
                instacne = clone.AddComponent<HttpUtil>();
                DontDestroyOnLoad(clone);
            }

            return instacne; }
    }

    private static HttpUtil instacne;

    private IEnumerator get(string url,Action<bool,string> callback) 
    {
        UnityWebRequest unityWeb = UnityWebRequest.Get(url);
        yield return unityWeb.SendWebRequest();
        callback?.Invoke(unityWeb.result == UnityWebRequest.Result.ProtocolError, unityWeb.downloadHandler.text);
        //if (unityWeb.result == UnityWebRequest.Result.ProtocolError)
        //{
        //    Debug.Log("alierro" + unityWeb.error);
        //}
        //else
        //{
        //    Debug.Log("ali" + unityWeb.downloadHandler.text);
        //}
    }

    private IEnumerator post<T>(string url, string postData,Action<bool, string> callback) where T :class
    {
        UnityWebRequest webRequest = new UnityWebRequest(url, "POST");
        byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postData);
        webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(postBytes);
        webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();
        
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            callback?.Invoke(false, null);
        }
        else
        {
            callback?.Invoke(true, webRequest.downloadHandler.text);
        }
    }
   
}

