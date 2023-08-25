using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurviveMode : MonoBehaviour
{
    public GameObject[] zombiePrefabs; // 좀비 프리팹들
    // 좀비 프리팹들의 생성 비율을 설정하는 가중치
    public float[] zombieSpawnWeights = { 6f, 3f, 3f, 3f, 1f,};
    public float initialSpawnInterval = 5f; // 초기 생성 간격
    public float minSpawnInterval = 2f; // 최소 생성 간격
    public float spawnIntervalDecreaseRate = 0.9f; // 생성 간격 감소 비율


    private float currentSpawnInterval;
    private int waveCount = 0;

    private float[] allowedYPositions; // y 좌표 허용 배열
    private void Start()
    {
        currentSpawnInterval = initialSpawnInterval; // 초기 생성 간격 설정
        StartCoroutine(SpawnZombieRoutine()); // 좀비 생성 루틴 시작

        waveCount = 1; // 웨이브 카운트 초기화


        allowedYPositions = new float[]
        {
            2f,
            0.5f,
            -1.3f,
            -2.7f,
            -4.6f
        };
    }

    private void Update()
    {
        // 모든 웨이브가 끝났을 때
        if (waveCount <= 0 && GameManager.instance.isStageClear == false)
        {
            GameManager.instance.isWave = false;
            GameManager.instance.isStageClear = true;
        }
    }

    IEnumerator SpawnZombieRoutine()
    {
        yield return new WaitForSeconds(4.0f); // 시작 후 4초 대기

        while (true)
        {
            // 각 프리팹마다 좀비 생성
            for (int i = 0; i < zombiePrefabs.Length; i++)
            {
                SpawnZombie(zombiePrefabs[i]); // 좀비 생성 함수 호출
                yield return new WaitForSeconds(currentSpawnInterval); // 생성 간격 대기
            }

            waveCount += 1; // 웨이브 카운트 증가
            //GameManager.instance.isWave = true; // 웨이브 시작을 게임 매니저에 알림

            yield return new WaitForSeconds(4.0f); // 웨이브 시작 후 2초 대기

            // 생성 간격을 감소시키고, 최소 간격에 도달하지 않도록 조정
            currentSpawnInterval *= spawnIntervalDecreaseRate;
            currentSpawnInterval = Mathf.Max(currentSpawnInterval, minSpawnInterval);
        }
    }

    private void SpawnZombies()
    {
        int prefabIndex = ChooseZombiePrefabIndex(); // 좀비 프리팹 인덱스 선택
        GameObject zombiePrefab = zombiePrefabs[prefabIndex]; // 선택된 프리팹

        SpawnZombie(zombiePrefab); // 선택된 프리팹을 생성하는 함수 호출
    }

    private void SpawnZombie(GameObject prefab)
    {
        int randomIndex = Random.Range(0, allowedYPositions.Length);
        float spawnYPosition = allowedYPositions[randomIndex];
        Vector3 spawnPosition = new Vector3(transform.position.x, spawnYPosition, transform.position.z);

        Instantiate(prefab, spawnPosition, Quaternion.identity); // 선택된 프리팹을 생성
    }

    private int ChooseZombiePrefabIndex()
    {
        float totalWeight = 0f;
        foreach (float weight in zombieSpawnWeights)
        {
            totalWeight += weight;
        }

        float randomValue = Random.Range(0f, totalWeight);
        float weightSum = 0f;
        for (int i = 0; i < zombieSpawnWeights.Length; i++)
        {
            weightSum += zombieSpawnWeights[i];
            if (randomValue <= weightSum)
            {
                return i;
            }
        }

        // 만약 선택이 실패한 경우 첫 번째 프리팹 반환 (에러 처리)
        return 0;
    }
}
