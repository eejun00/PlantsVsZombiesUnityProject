using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngineInternal;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private TowerSpawner towerSpawner;
    //[SerializeField]
    //private TowerDataViewer towerDataViewer;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;
    private Transform hitTransform = null;       // 마우스 픽킹으로 선택한 오브잭트 임시 저장

    private void Awake()
    {
        // "MainCamera" 태그를 가지고 있는 오브젝트 탐색 후 Camera 컴포넌트 정보 전달
        //mainCamera = GameObject.FindGameObjectsWithTag("MainCamera").GetComponent<Camera>();와 동일
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 마우스가 UI에 머물러 있을 때는 아래 코드가 실행되지 않도록 함
        //if (EventSystem.current.IsPointerOverGameObject() == true)
        //{
        //    return;
        //}

        // 마우스 왼쪽 버튼을 눌렀을 때
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit2D.collider != null)
            {
                hitTransform = hit2D.transform;

                if (hitTransform.CompareTag("Tile"))
                {
                    towerSpawner.SpawnTower(hitTransform);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // 마우스를 눌렀을 때 선택한 오브젝트가 없거나 선택한 오브젝트가 타워가 아니면
            if(hitTransform == null)
            {
                // 타워 정보 패널을 비활성화 한다.
                //towerDataViewer.OffPanel();
            }

            hitTransform = null;
        }
    }
}
