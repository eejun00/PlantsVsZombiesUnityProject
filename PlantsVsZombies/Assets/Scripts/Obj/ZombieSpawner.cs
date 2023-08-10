//using System.Collections;
//using UnityEngine;

//public class ZombieSpawner : MonoBehaviour
//{
//    public GameObject zombiePrefab; // 좀비 프리팹
//    public float spawnInterval = 5f; // 좀비 생성 간격

//    private void Start()
//    {
//        // spawnInterval마다 SpawnZombie 함수를 호출
//        StartCoroutine(SpawnZombieRoutine());
//    }

//    IEnumerator SpawnZombieRoutine()
//    {
//        while (true)
//        {
//            // 좀비 프리팹을 현재 스포너 위치에 생성
//            Instantiate(zombiePrefab, transform.position, Quaternion.identity);

//            // 다음 생성 대기
//            yield return new WaitForSeconds(spawnInterval);
//        }
//    }
//}

using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // 좀비 프리팹
    public float spawnInterval = 5f; // 좀비 생성 간격
    public float[] allowedYPositions; // 좀비 생성 허용 y좌표 배열
    public int zombieCount = 5;

    private void Start()
    {
        // spawnInterval마다 SpawnZombie 함수를 호출
        GameManager.instance.zombieDeathCount = zombieCount;
        StartCoroutine(SpawnZombieRoutine());
    }

    private void Update()
    {
        if(zombieCount <= 0 && GameManager.instance.isStageClear == false)
        {
            GameManager.instance.isStageClear = true;
        }
    }

    IEnumerator SpawnZombieRoutine()
    {
        while (zombieCount > 0)
        {
            // 랜덤한 인덱스 선택
            int randomIndex = Random.Range(0, allowedYPositions.Length);

            // 선택된 인덱스에 해당하는 y좌표 가져오기
            float spawnYPosition = allowedYPositions[randomIndex];

            // 좀비 생성 위치 설정
            Vector3 spawnPosition = new Vector3(transform.position.x, spawnYPosition, transform.position.z);

            // 좀비 프리팹을 생성 위치에 생성
            Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            zombieCount -= 1;
            // 다음 생성 대기
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}