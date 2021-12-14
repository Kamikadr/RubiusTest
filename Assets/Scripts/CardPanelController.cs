using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardPanelController : MonoBehaviour
{
    
    private Card[] cards;
    public event Action onLoadIsDoneEvent;
    private int readyCardCounter;
    private bool checkOrderOfReadiness = false;
    private List<Card> orderOfReadinessCard;
    private void Start()
    {
        cards = gameObject.GetComponentsInChildren<Card>();
        orderOfReadinessCard = new List<Card>();
        foreach (Card card in cards) card.cardIsReady += PutIntoReadyState;
    }

    private void PutIntoReadyState(Card card)
    {
        if (checkOrderOfReadiness) orderOfReadinessCard.Add(card);
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
        checkOrderOfReadiness = false;
        readyCardCounter = 0;
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

        checkOrderOfReadiness = true;
        orderOfReadinessCard.Clear();

        yield return StartCoroutine(LoadAllCards());

        foreach (Card card in orderOfReadinessCard)
        {
            card.Flip();
            while (!card.isFlipped) yield return null;
        }

        onLoadIsDoneEvent?.Invoke();
    }

    IEnumerator LoadAllCards()
    {
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
