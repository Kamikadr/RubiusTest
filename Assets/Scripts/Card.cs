using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Card : MonoBehaviour
{

    public event Action<Card> cardIsReady;
    public event Action cardIsRefreshed;

    
    public bool isFlipped { get; private set;}

    [SerializeField] private RawImage cardImage;
    [SerializeField] private GameObject cardFront;
    [SerializeField] private Image cardBack;

    private Sequence flipCardAnim;
    private Sequence flipBackCardAnim;

    private IConnection connection;


    private void Awake()
    {
        connection = GameObject.Find("Controller").GetComponent<IConnection>();
        connection.textureReceived += PrepareCard;

        DOTween.Init();
        DOTween.defaultAutoPlay = AutoPlay.None;
        DOTween.defaultAutoKill = false;

        isFlipped = false;
    }
    private void Start()
    {
        InitialFlipCardAnimation();
        InitialFlipBackCardAnimation();
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
        flipCardAnim.OnComplete(() => { isFlipped = true; });
    }

    private void InitialFlipBackCardAnimation() 
    {
        flipBackCardAnim = DOTween.Sequence();
        flipBackCardAnim.Append(transform.DORotate(new Vector3(0, transform.rotation.y - 90, 0), 0.5f, RotateMode.Fast))
            .AppendCallback(() => {
                cardBack.enabled = true;
                cardBack.transform.SetAsLastSibling();
            });
        flipBackCardAnim.Join(transform.DORotate(new Vector3(0, transform.rotation.y, 0), 0.5f, RotateMode.Fast));
        flipBackCardAnim.OnComplete(() => { 
            isFlipped = false;
            cardIsRefreshed?.Invoke();
        });
    }

    public void LoadCard()
    {
        connection.GetImage(this);
    }

    

    private void PrepareCard(Texture texture , object card) 
    {
        if((Card) card == this)
            StartCoroutine(WaitBackAnimationAndChangeCard(texture));
    }
    IEnumerator WaitBackAnimationAndChangeCard(Texture texture) 
    {
        if(flipBackCardAnim.IsPlaying())
            yield return flipBackCardAnim.WaitForCompletion();
        cardImage.texture = texture;
        cardIsReady?.Invoke(this);
    }
    public void Flip() 
    {
        flipCardAnim.Restart();
    }

    public void FlipBack() 
    {
        if (isFlipped)
        {
            flipBackCardAnim.Restart();
        }
        else cardIsRefreshed?.Invoke();
    }
}
