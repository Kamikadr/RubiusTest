using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HTTPConnection : MonoBehaviour, IConnection
{
    const string URL = "https://picsum.photos/200";

    public event Action<Texture, object> textureReceived;

    public void GetImage(object card)
    {
        StartCoroutine(RequestImage(card));
    }


    public IEnumerator RequestImage(object card)
    {

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(URL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            textureReceived?.Invoke(((DownloadHandlerTexture)www.downloadHandler).texture, card);
        }
    }
}
