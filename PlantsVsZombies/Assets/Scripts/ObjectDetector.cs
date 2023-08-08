using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private TowerSpawner towerSpawner;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    private void Awake()
    {
        // MainCamera �±׸� ������ �ִ� ������Ʈ Ž�� �� Camera ������Ʈ ���� ����
        // GameObject.FindGameObjectWithtag("MainCamera").GetComponent<Camera>(); �� ����
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            // ī�޶� ��ġ���� ȭ���� ���콺 ��ġ�� �����ϴ� ���� ����
            // ray.origin : ������ ������ġ = ī�޶� ��ġ
            // ray.direction : ������ �������
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // 2d ����͸� ���� 3d ������ ������Ʈ�� ���콺�� �����ϴ� ���
            // ������ �ε����� ������Ʈ�� �����ؼ� hit�� ����
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // ������ �ε��� ������Ʈ�� �±װ� "Tile" �̸�
                if(hit.transform.CompareTag("Tile"))
                {
                    // Ÿ���� �����ϴ� SpawnTower() ȣ��
                    towerSpawner.SpawnTower(hit.transform);
                }
            }
        }
    }
}
