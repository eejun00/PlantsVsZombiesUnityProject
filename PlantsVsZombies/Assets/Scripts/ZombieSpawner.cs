using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // 좀비 프리팹
    public float spawnInterval = 5f; // 좀비 생성 간격

    private void Start()
    {
        // spawnInterval마다 SpawnZombie 함수를 호출
        StartCoroutine(SpawnZombieRoutine());
    }

    IEnumerator SpawnZombieRoutine()
    {
        while (true)
        {
            // 좀비 프리팹을 현재 스포너 위치에 생성
            Instantiate(zombiePrefab, transform.position, Quaternion.identity);

            // 다음 생성 대기
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

