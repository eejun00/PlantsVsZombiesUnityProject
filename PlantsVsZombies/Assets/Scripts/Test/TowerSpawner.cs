using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TowerSpawner : MonoBehaviour
{
    //[SerializeField]
    //private TowerTemplate[] towerTemplate;         // 타워 정보 (공격력, 공격속도 등)
    //[SerializeField]
    //private EnemySpawner enemySpawner;           // 현재 맵에 존재하는 적 리스트 정보를 얻기 위해..
    //[SerializeField]
    //private PlayerGold playerGold;               // 타워 건설 시 골드 감소를 위해..
    //[SerializeField]
    //private SystemTextViewer systemTextViewer;   // 돈 부족, 건설 불가와 같은 시스템 메시지 출력
    public bool isOnTowerButton = false;        // 타워 건설 버튼을 눌렀는지 체크
    private GameObject followTowerClone = null;  // 임시 타워 사용 완료 시 삭제를 위해 저장하는 변수
    private int towerType;                       // 타워 속성
    private SeedCard seedCard;
    private ObjectFollowMousePosition followMousePosition;

    public GameObject PlantPrefab;
    public GameObject PlantImgPrefab;
    private int plantingCost = default;

    

    public void ReadyToSpawnTower(GameObject plantPrefab)
    {
        // 버튼을 중복해서 누르는 것을 방지하기 위해 필요
        if (isOnTowerButton == true)
        {
            return;
        }

        // 타워 건설 가능 여부 확인
        // 타워를 건설할 만큼 돈이 없으면 타워 건설 x
        Plant plantscript = plantPrefab.GetComponent<Plant>();
        plantingCost = plantscript.cost;
        if (plantingCost > GameManager.instance.cost)
        {
            // 골드가 부족해서 타워 건설이 불가능하다고 출력
            //systemTextViewer.PrintText(SystemType.Money);
            return;
        }


        // 타워 건설 버튼을 눌렀다고 설정
        isOnTowerButton = true;
        // 마우스를 따라나니는 임시 타워 생성
        followTowerClone = Instantiate(plantPrefab);
        Collider2D cloneCollider = followTowerClone.GetComponent<Collider2D>();
        cloneCollider.enabled = false;
        PlantPrefab = plantPrefab;
        // 타워 건설을 취소할 수 있는 코루틴 함수 시작
        StartCoroutine("OnTowerCancelSystem");
    }
    public void SpawnTower(Transform tileTransform)
    {
        // 타워 건설 버튼을 눌렀을 때만 타워 건설 가능
        if (isOnTowerButton == false)
        {
            return;
        }

        Tile tile = tileTransform.GetComponent<Tile>();

        // 2.현재 타일의 위치에 이미 타워가 건설되어 있으면 타워 건설x
        if (tile.IsBuildTower == true)
        {
            // 현재 위치에 타워 건설이 불가능하다고 출력
            //systemTextViewer.PrintText(SystemType.Build);
            return;
        }

        // 다시 타워 건설 버튼을 눌러서 타워를 건설하도록 변수 설정
        isOnTowerButton = false;

        // 타워가 건설되어 있으므로 설정
        tile.IsBuildTower = true;
        // 타워 건설에 필요한 골드만큼 감소
        //playerGold.CurrentGold -= towerTemplate[towerType].weapon[0].cost;
        // 선택한 타일의 위치에 타워 건설 (타일보다 z축 -1의 위치에 배치)
        Vector3 position = tileTransform.position;
        GameObject clone = Instantiate(PlantPrefab, position, Quaternion.identity);
        clone.transform.SetParent(tileTransform);
        followMousePosition = clone.GetComponent<ObjectFollowMousePosition>();
        GameManager.instance.AddCost(-plantingCost);
        if (followMousePosition != null)
        {
            followMousePosition.enabled = false; // ObjectFollowMousePosition 스크립트 비활성화
        }
        // 타워 무기에 enemySpawner 정보 전달
        //TowerWeapon towerWeapon = clone.GetComponent<TowerWeapon>();
        //if (towerWeapon != null)
        //{
        //    towerWeapon.Setup(enemySpawner, playerGold, tile);
        //}

        // 타워를 배치했기 때문에 마우스를 따라다니는 임시 타워 삭제
        Destroy(followTowerClone);
        // 타워 건설을 취소할 수 있는 코루틴 함수 중지
        StopCoroutine("OnTowerCancelSystem");
    }

    private IEnumerator OnTowerCancelSystem()
    {
        while (true)
        {
            // ESC키 또는 마우스 오른쪽 버튼을 눌렀을 때 타워 건설 취소
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                isOnTowerButton = false;
                // 마우스를 따라다니는 임시 타워 삭제
                Destroy(followTowerClone);
                break;
            }

            yield return null;
        }
    }
}
