using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardPanelController : MonoBehaviour
{
    
    Card[] cards;
    public event Action onLoadIsDoneEvent;
    private void Start()
    {
        cards = gameObject.GetComponentsInChildren<Card>();
    }

    public void AllAtOnceFlip()
    {
        FlipBackCards();
        StartCoroutine(AllAtOnceFlipEnum());
    }

    public void OneByOneFlip()
    {
        FlipBackCards();
        StartCoroutine(OneByOneFlipEnum());
    }

    public void WhenImageReadyFlip()
    {
        FlipBackCards();
        StartCoroutine(WhenImageReadyFlipEnum());
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
            yield return new WaitForSeconds(0.5f);
        }

        onLoadIsDoneEvent?.Invoke();
    }

    IEnumerator LoadAllCards()
    {
        LoadImages();

        bool isReady = false;
        while (!isReady)
        {
            isReady = true;
            foreach (Card card in cards)
            {
                if (!card.isReady) isReady = false;
            }
            if (!isReady) yield return null;
        }
    }

    
    IEnumerator WhenImageReadyFlipEnum() 
    {
        LoadImages();

        var cardStatusList = new List<bool>();
        for (int i = 0; i < cards.Length; i++) cardStatusList.Add(false);

        bool isAllCardReady = false;
        while (!isAllCardReady)
        {
            isAllCardReady = CheckReadinessAndFlipReadyCard(ref cardStatusList);
            if (!isAllCardReady) yield return null;
        }

        onLoadIsDoneEvent?.Invoke();
    }
    private void LoadImages()
    {
        foreach (Card card in cards) card.LoadCard();
    }

    private bool CheckReadinessAndFlipReadyCard(ref List<bool> cardStatusList) 
    {
        bool isAllCardReady = true;
        for (int i = 0; i < cardStatusList.Count; i++)
        {
            if (cards[i].isReady)
            {
                if (cardStatusList[i] == false)
                {
                    cards[i].Flip();
                    cardStatusList[i] = true;
                }
            }
            else isAllCardReady = false;
        }
        return isAllCardReady;
    }
   

    public void Stop() 
    {
        foreach (Card card in cards) card.StopAllCoroutines();
        StopAllCoroutines();

        onLoadIsDoneEvent?.Invoke();
    }
}
