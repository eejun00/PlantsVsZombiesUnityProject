using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SeedCardSelect : MonoBehaviour
{
    public Transform[] cardSlots = new Transform[6];
    public GameObject[] placedCards = new GameObject[6];

    private GameObject uiCanvas;
    private GameObject seedChooser;
    private GameObject equalCard;

    private void Start()
    {
        uiCanvas = GFunc.GetRootObject("UiCanvas");
        seedChooser = uiCanvas.FindChildObject("SeedChooser");
    }

    public void HandleCardClick(GameObject card)
    {
        int slotIndex = GetSlotIndex(card);

        if (slotIndex >= 0)
        {
            RemoveCard(card);
        }
        else
        {
            int emptySlotIndex = GetEmptySlotIndex();
            if (emptySlotIndex >= 0)
            {
                PlaceCard(card, emptySlotIndex);
            }
        }
    }

    private int GetSlotIndex(GameObject card)
    {
        for (int i = 0; i < placedCards.Length; i++)
        {
            if (placedCards[i] == card)
            {
                return i;
            }
        }
        return -1;
    }

    private void PlaceCard(GameObject card, int slotIndex)
    {
        placedCards[slotIndex] = card;
        card.transform.SetParent(cardSlots[slotIndex]);
        card.transform.localPosition = Vector3.zero;
    }

    private void RemoveCard(GameObject card)
    {
        int cardIndex = GetSlotIndex(card);
        if (cardIndex >= 0)
        {
            equalCard = seedChooser.FindChildObject(card.name);
            placedCards[cardIndex] = null;
            card.transform.SetParent(equalCard.transform);
            card.transform.localPosition = Vector3.zero;
            //card.transform.position = equalCard.transform.position;
        }        
    }

    private int GetEmptySlotIndex()
    {
        for (int i = 0; i < placedCards.Length; i++)
        {
            if (placedCards[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
}
