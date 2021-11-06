using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardPanelController : MonoBehaviour
{
    public List<Card> cards;
    public float time;

    public event Action onLoadIsDoneEvent;
   
    private void LoadImages()
    {
        foreach (Card card in cards)
        {
            StartCoroutine(card.LoadImage());
        }
    }
    private void FlipBackCards() 
    {
        foreach (Card card in cards) card.FlipBack();
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

    IEnumerator AllAtOnceFlipEnum()
    {
        yield return StartCoroutine(LoadAllCards());


        foreach (Card card in cards)
        {
            card.FlipCard();
        }

        onLoadIsDoneEvent?.Invoke();
    }


    IEnumerator OneByOneFlipEnum() 
    {
        yield return StartCoroutine(LoadAllCards());

        foreach (Card card in cards)
        {
            card.FlipCard();
            yield return new WaitForSeconds(time);
        }

        onLoadIsDoneEvent?.Invoke();
    }

    IEnumerator WhenImageReadyFlipEnum() 
    {
        LoadImages();

        bool isReady = false;
        while (!isReady)
        {
            isReady = true;
            foreach (Card card in cards)
            {
                if (card.isReady) 
                { 
                    card.FlipCard();
                }
                else isReady = false;
            }
            if (!isReady) yield return null;
        }

        onLoadIsDoneEvent?.Invoke();
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
    public void Stop() 
    {
        foreach (Card card in cards) card.StopAllCoroutines();
        StopAllCoroutines();

        onLoadIsDoneEvent?.Invoke();
    }
}
