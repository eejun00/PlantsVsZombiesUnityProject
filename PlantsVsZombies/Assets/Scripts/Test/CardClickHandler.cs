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
        if(GFunc.GetRootObject("CardSlotManager") != null)
        {
            // CardSlotManager의 스크립트 가져오기
            cardSlotManager = GFunc.GetRootObject("CardSlotManager").GetComponent<SeedCardSelect>();
        }        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.instance.isSelectSeed == false || GameManager.instance.stageOneNum <= 6) { return; }
        else
        {
            cardSlotManager.HandleCardClick(gameObject);
            // SeedCardSelect 스크립트에서 처리, CardSlotManager가 들고있음
        }
    }
}