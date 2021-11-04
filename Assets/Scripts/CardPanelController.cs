using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPanelController : MonoBehaviour
{
    public List<CardLoader> cards;

    public void FlipAllCards() 
    {
        StartCoroutine(LoadAllCards());
    }

    IEnumerator LoadAllCards()
    {
        foreach (CardLoader card in cards)
        {
            StartCoroutine(card.LoadImage());
        }


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

        foreach (CardLoader card in cards)
        {
            card.FlipCard();
        }

    }

}
