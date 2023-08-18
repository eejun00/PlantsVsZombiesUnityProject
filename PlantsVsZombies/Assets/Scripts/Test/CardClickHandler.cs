using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private SeedCardSelect cardSlotManager;

    private void Awake()
    {
        // CardSlotManager�� ��ũ��Ʈ ��������
        cardSlotManager = GFunc.GetRootObject("CardSlotManager").GetComponent<SeedCardSelect>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.instance.isSelectSeed == false) { return; }
        else
        {
            cardSlotManager.HandleCardClick(gameObject);
            // SeedCardSelect ��ũ��Ʈ���� ó��, CardSlotManager�� �������
        }
    }
}