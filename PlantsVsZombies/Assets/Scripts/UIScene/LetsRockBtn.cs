using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetsRockBtn : MonoBehaviour
{
    private SeedCardSelect seedCardSelect;

    bool IsArrayFull(GameObject[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == null) // 값이 0이라면 비어있는 상태
            {
                return false;
            }
        }
        return true; // 모든 요소가 값으로 채워져 있으면 꽉 찬 상태
    }

    public void OnClickRockBtn()
    {
        seedCardSelect = GFunc.GetRootObject("CardSlotManager").GetComponent<SeedCardSelect>();
        if (IsArrayFull(seedCardSelect.placedCards))
        {
            GameManager.instance.LetsRock();
        }
    }
}
