using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPanelController : MonoBehaviour
{
    public List<CardLoader> cards;
    public float time;



    private void LoadImages()
    {
        foreach (CardLoader card in cards)
        {
            StartCoroutine(card.LoadImage());
        }
    }

    IEnumerator LoadAllCards()
    {
        LoadImages();

        bool isReady = false;
        while (!isReady)
        {
            isReady = true;
            foreach (CardLoader card in cards)
            {
                if (!card.isReady) isReady = false;
            }
            if (!isReady) yield return null;
        }
    }

    IEnumerator AllAtOnceFlipEnum()
    {
        yield return StartCoroutine(LoadAllCards());


        foreach (CardLoader card in cards)
        {
            card.FlipCard();
        }
    }


    IEnumerator OneByOneFlipEnum() 
    {
        yield return StartCoroutine(LoadAllCards());

        foreach (CardLoader card in cards)
        {
            card.FlipCard();
            yield return new WaitForSeconds(time);
        }
    }

    IEnumerator WhenImageReadyFlipEnum() 
    {
        LoadImages();

        bool isReady = false;
        while (!isReady)
        {
            isReady = true;
            foreach (CardLoader card in cards)
            {
                if (card.isReady) 
                { 
                    card.FlipCard();
                }
                else isReady = false;
            }
            if (!isReady) yield return null;
        }
    }
   

    public void AllAtOnceFlip()
    {
        StartCoroutine(AllAtOnceFlipEnum());
    }
    public void OneByOneFlip()
    {
        StartCoroutine(OneByOneFlipEnum());
    }
    public void WhenImageReadyFlip()
    {
        StartCoroutine(WhenImageReadyFlipEnum());
    }
}
