using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacement : MonoBehaviour
{
    public Tilemap tilemap;         // 타일맵 레이어를 가리키는 참조
    public TileBase towerTile;      // 타워를 표현하는 타일
    public GameObject towerPrefab;  // 타워 프리팹
    public LayerMask buildLayer;    // 건설 가능한 영역을 결정하기 위한 레이어 마스크

    private Camera mainCamera;      // 메인 카메라의 참조

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

            // 마우스 클릭 위치에 타워를 설치할 수 있는지 확인
            if (CanBuildTower(cellPos))
            {
                // 타일맵에 타워 타일을 배치
                tilemap.SetTile(cellPos, towerTile);

                // 타워 프리팹을 해당 위치에 생성
                Vector3 towerWorldPos = tilemap.GetCellCenterWorld(cellPos);
                Instantiate(towerPrefab, towerWorldPos, Quaternion.identity);
            }
        }
    }

    bool CanBuildTower(Vector3Int cellPos)
    {
        // 해당 위치에 이미 타워 타일이 있으면 건설 불가능
        if (tilemap.GetTile(cellPos) != null)
        {
            return false;
        }

        // 해당 위치에 다른 레이어의 콜라이더가 있다면 건설 불가능
        Collider2D collider = Physics2D.OverlapPoint(tilemap.GetCellCenterWorld(cellPos), buildLayer);
        if (collider != null)
        {
            return false;
        }

        return true;
    }
}