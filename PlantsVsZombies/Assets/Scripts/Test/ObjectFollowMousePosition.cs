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
            // 화면의 마우스 좌표를 기준으로 게임 월드 상의 자표를 구한다.
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            transform.position = mainCamera.ScreenToWorldPoint(position);   // Vector3 world = Camera.ScreenToWorldPoint(Vector3 screen); 화면 상의 좌표 screen을 바탕으로 월드 좌표 world를 구하는 함수
                                                                            // z위치를 0으로 설정
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

    }
}
