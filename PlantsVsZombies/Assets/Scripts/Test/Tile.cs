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
    
}

//Ÿ�� ��ġ�� ������ TileWall ������Ʈ�� ����
