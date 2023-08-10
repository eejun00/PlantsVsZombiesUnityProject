using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacement : MonoBehaviour
{
    public Tilemap tilemap;         // Ÿ�ϸ� ���̾ ����Ű�� ����
    public TileBase towerTile;      // Ÿ���� ǥ���ϴ� Ÿ��
    public GameObject towerPrefab;  // Ÿ�� ������
    public LayerMask buildLayer;    // �Ǽ� ������ ������ �����ϱ� ���� ���̾� ����ũ

    private Camera mainCamera;      // ���� ī�޶��� ����

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);

            // ���콺 Ŭ�� ��ġ�� Ÿ���� ��ġ�� �� �ִ��� Ȯ��
            if (CanBuildTower(cellPos))
            {
                // Ÿ�ϸʿ� Ÿ�� Ÿ���� ��ġ
                tilemap.SetTile(cellPos, towerTile);

                // Ÿ�� �������� �ش� ��ġ�� ����
                Vector3 towerWorldPos = tilemap.GetCellCenterWorld(cellPos);
                Instantiate(towerPrefab, towerWorldPos, Quaternion.identity);
            }
        }
    }

    bool CanBuildTower(Vector3Int cellPos)
    {
        // �ش� ��ġ�� �̹� Ÿ�� Ÿ���� ������ �Ǽ� �Ұ���
        if (tilemap.GetTile(cellPos) != null)
        {
            return false;
        }

        // �ش� ��ġ�� �ٸ� ���̾��� �ݶ��̴��� �ִٸ� �Ǽ� �Ұ���
        Collider2D collider = Physics2D.OverlapPoint(tilemap.GetCellCenterWorld(cellPos), buildLayer);
        if (collider != null)
        {
            return false;
        }

        return true;
    }
}