using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using DG.Tweening;


public class Card : MonoBehaviour
{
    const string URL = "https://picsum.photos/200";
    public bool isReady = false;
    private bool isFlipped = false;

    public RawImage cardImage;
    public GameObject cardFront;
    public Image cardBack;

    private Sequence flipCardAnim;
    private Sequence flipBackCardAnim;


    private void Awake()
    {
        DOTween.Init();
        DOTween.defaultAutoPlay = AutoPlay.None;
        DOTween.defaultAutoKill = false;
    }
    private void Start()
    {
        InitialFlipCardAnimation();
        InitialBackFlipCardAnimation();
    }

    private void InitialFlipCardAnimation() 
    {
        flipCardAnim = DOTween.Sequence();
        flipCardAnim.Append(transform.DORotate(new Vector3(0, transform.rotation.y + 90, 0), 0.5f, RotateMode.Fast))
            .AppendCallback(() => {
                cardBack.enabled = false;
                cardFront.transform.SetAsLastSibling();
            });
        flipCardAnim.Join(transform.DORotate(new Vector3(0, transform.rotation.y + 180, 0), 0.5f, RotateMode.Fast));
        
        
    }
    private void InitialBackFlipCardAnimation() 
    {
        flipBackCardAnim = DOTween.Sequence();
        flipBackCardAnim.Append(transform.DORotate(new Vector3(0, transform.rotation.y - 90, 0), 0.5f, RotateMode.Fast)).AppendCallback(() => {
            cardBack.enabled = true;
            cardBack.transform.SetAsLastSibling();
        });
        flipBackCardAnim.Join(transform.DORotate(new Vector3(0, transform.rotation.y, 0), 0.5f, RotateMode.Fast));
    }

    public IEnumerator LoadImage() 
    {
        isReady = false;


        UnityWebRequest www = UnityWebRequestTexture.GetTexture(URL);
        var fakeCertificate = new SLLBypass();
        www.certificateHandler = fakeCertificate;
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            if (flipBackCardAnim.IsPlaying())
            {
                yield return flipBackCardAnim.WaitForCompletion();
            }

            cardImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            isReady = true;
        }
    }

    
    public void FlipCard() 
    {
        flipCardAnim.Restart();
        isFlipped = true;
    }

    public void FlipBack() 
    {   
        if (this.isFlipped)
        {
            flipBackCardAnim.Restart();
            isFlipped = false;
        }
    }

    

}
