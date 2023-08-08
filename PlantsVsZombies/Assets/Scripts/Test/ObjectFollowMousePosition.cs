using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowMousePosition : MonoBehaviour
{
    private GameObject towerSpawner;
    private TowerSpawner towerSpawnerScript;
    private Camera mainCamera;

    private void Awake()
    {
        towerSpawner = GFunc.GetRootObject("TowerSpawner");
        towerSpawnerScript = towerSpawner.GetComponent<TowerSpawner>();
        mainCamera = Camera.main;
    }

     void Update()
    {
        if(towerSpawnerScript.isOnTowerButton == true)
        {
            // ȭ���� ���콺 ��ǥ�� �������� ���� ���� ���� ��ǥ�� ���Ѵ�.
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            transform.position = mainCamera.ScreenToWorldPoint(position);   // Vector3 world = Camera.ScreenToWorldPoint(Vector3 screen); ȭ�� ���� ��ǥ screen�� �������� ���� ��ǥ world�� ���ϴ� �Լ�
                                                                            // z��ġ�� 0���� ����
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

    }
}
