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
    private Transform hitTransform = null;       // ���콺 ��ŷ���� ������ ������Ʈ �ӽ� ����

    private void Awake()
    {
        // "MainCamera" �±׸� ������ �ִ� ������Ʈ Ž�� �� Camera ������Ʈ ���� ����
        //mainCamera = GameObject.FindGameObjectsWithTag("MainCamera").GetComponent<Camera>();�� ����
        mainCamera = Camera.main;
    }

    void Update()
    {
        // ���콺�� UI�� �ӹ��� ���� ���� �Ʒ� �ڵ尡 ������� �ʵ��� ��
        //if (EventSystem.current.IsPointerOverGameObject() == true)
        //{
        //    return;
        //}

        // ���콺 ���� ��ư�� ������ ��
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
            // ���콺�� ������ �� ������ ������Ʈ�� ���ų� ������ ������Ʈ�� Ÿ���� �ƴϸ�
            if(hitTransform == null)
            {
                // Ÿ�� ���� �г��� ��Ȱ��ȭ �Ѵ�.
                //towerDataViewer.OffPanel();
            }

            hitTransform = null;
        }
    }
}
