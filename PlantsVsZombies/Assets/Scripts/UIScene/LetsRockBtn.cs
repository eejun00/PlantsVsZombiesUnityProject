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
            if (arr[i] == null) // ���� 0�̶�� ����ִ� ����
            {
                return false;
            }
        }
        return true; // ��� ��Ұ� ������ ä���� ������ �� �� ����
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
