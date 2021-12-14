using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardPanelController : MonoBehaviour
{
    
    private Card[] cards;
    public event Action onLoadIsDoneEvent;

    private int readyCardCounter;
    private bool autoFlipFlag = false;

    private int refreshedCardCounter;
    private bool allCardRefreshed = true;

    private void Start()
    {
        cards = gameObject.GetComponentsInChildren<Card>();
        foreach (Card card in cards)
        {
            card.cardIsReady += PutIntoReadyState;
            card.cardIsRefreshed += CheckAllRefreshedCard;
        }

    }

    private void CheckAllRefreshedCard()
    {
        refreshedCardCounter++;
        if (refreshedCardCounter == cards.Length) allCardRefreshed = true;
    }

    private void PutIntoReadyState(Card card)
    {
        if (autoFlipFlag) card.Flip();
        readyCardCounter++;
    }
    
    public void AllAtOnceFlip()
    {
        RefreshSettings();
        StartCoroutine(AllAtOnceFlipEnum());
    }

    public void OneByOneFlip()
    {
        RefreshSettings();
        StartCoroutine(OneByOneFlipEnum());
    }

    public void WhenImageReadyFlip()
    {
        RefreshSettings();
        StartCoroutine(WhenImageReadyFlipEnum());
    }
   
    private void RefreshSettings()
    {
        autoFlipFlag = false;
        allCardRefreshed = false;
        readyCardCounter = 0;
        refreshedCardCounter = 0;
        FlipBackCards();
    }

    private void FlipBackCards()
    {
        foreach (Card card in cards) card.FlipBack();
    }

    IEnumerator AllAtOnceFlipEnum()
    {
        yield return StartCoroutine(LoadAllCards());

        foreach (Card card in cards) card.Flip();

        onLoadIsDoneEvent?.Invoke();
    }

    IEnumerator OneByOneFlipEnum()
    {
        yield return StartCoroutine(LoadAllCards());

        foreach (Card card in cards)
        {
            card.Flip();
            while (!card.isFlipped) yield return null; 
        }

        onLoadIsDoneEvent?.Invoke();
    }
    IEnumerator WhenImageReadyFlipEnum()
    {

        autoFlipFlag = true;  

        yield return StartCoroutine(LoadAllCards());

        onLoadIsDoneEvent?.Invoke();
    }

    IEnumerator LoadAllCards()
    {
        while (!allCardRefreshed) yield return null;
        LoadImages();
        while (readyCardCounter != cards.Length) yield return null;
    }

    
    
    private void LoadImages()
    {
        foreach (Card card in cards) card.LoadCard();
    }
    public void Stop() 
    {
        foreach (Card card in cards) card.StopAllCoroutines();
        StopAllCoroutines();

        onLoadIsDoneEvent?.Invoke();
    }
}
