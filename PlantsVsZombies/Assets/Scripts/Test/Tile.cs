using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //Ÿ�Ͽ� Ÿ���� �Ǽ��Ǿ� �ִ��� �˻��ϴ� ����
    public bool IsBuildTower;

    private void Awake()
    {
        IsBuildTower = false;
    }

    private void Update()
    {
        if (IsBuildTower)
        {
            if(transform.childCount == 0)
            {
                IsBuildTower = false;
            }
        }
    }

}

//Ÿ�� ��ġ�� ������ TileWall ������Ʈ�� ����
