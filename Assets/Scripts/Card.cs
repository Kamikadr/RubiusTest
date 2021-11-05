using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    const string URL = "https://picsum.photos/200";
    public bool isReady;

    public RawImage cardImage;
    public GameObject cardFront;
    public Image cardBack;


    public IEnumerator LoadImage() 
    {
        isReady = false;


        UnityWebRequest www = UnityWebRequestTexture.GetTexture(URL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            cardImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            isReady = true;
        }
    }

    public void FlipCard() 
    {
        cardFront.transform.SetAsLastSibling();
    }
    
}
