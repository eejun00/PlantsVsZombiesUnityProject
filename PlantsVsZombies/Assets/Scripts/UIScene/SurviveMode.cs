using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurviveMode : MonoBehaviour
{
    public GameObject[] zombiePrefabs; // ���� �����յ�
    // ���� �����յ��� ���� ������ �����ϴ� ����ġ
    public float[] zombieSpawnWeights = { 6f, 3f, 3f, 3f, 1f,};
    public float initialSpawnInterval = 5f; // �ʱ� ���� ����
    public float minSpawnInterval = 2f; // �ּ� ���� ����
    public float spawnIntervalDecreaseRate = 0.9f; // ���� ���� ���� ����


    private float currentSpawnInterval;
    private int waveCount = 0;

    private float[] allowedYPositions; // y ��ǥ ��� �迭
    private void Start()
    {
        currentSpawnInterval = initialSpawnInterval; // �ʱ� ���� ���� ����
        StartCoroutine(SpawnZombieRoutine()); // ���� ���� ��ƾ ����

        waveCount = 1; // ���̺� ī��Ʈ �ʱ�ȭ


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
        // ��� ���̺갡 ������ ��
        if (waveCount <= 0 && GameManager.instance.isStageClear == false)
        {
            GameManager.instance.isWave = false;
            GameManager.instance.isStageClear = true;
        }
    }

    IEnumerator SpawnZombieRoutine()
    {
        yield return new WaitForSeconds(4.0f); // ���� �� 4�� ���

        while (true)
        {
            // �� �����ո��� ���� ����
            for (int i = 0; i < zombiePrefabs.Length; i++)
            {
                SpawnZombie(zombiePrefabs[i]); // ���� ���� �Լ� ȣ��
                yield return new WaitForSeconds(currentSpawnInterval); // ���� ���� ���
            }

            waveCount += 1; // ���̺� ī��Ʈ ����
            //GameManager.instance.isWave = true; // ���̺� ������ ���� �Ŵ����� �˸�

            yield return new WaitForSeconds(4.0f); // ���̺� ���� �� 2�� ���

            // ���� ������ ���ҽ�Ű��, �ּ� ���ݿ� �������� �ʵ��� ����
            currentSpawnInterval *= spawnIntervalDecreaseRate;
            currentSpawnInterval = Mathf.Max(currentSpawnInterval, minSpawnInterval);
        }
    }

    private void SpawnZombies()
    {
        int prefabIndex = ChooseZombiePrefabIndex(); // ���� ������ �ε��� ����
        GameObject zombiePrefab = zombiePrefabs[prefabIndex]; // ���õ� ������

        SpawnZombie(zombiePrefab); // ���õ� �������� �����ϴ� �Լ� ȣ��
    }

    private void SpawnZombie(GameObject prefab)
    {
        int randomIndex = Random.Range(0, allowedYPositions.Length);
        float spawnYPosition = allowedYPositions[randomIndex];
        Vector3 spawnPosition = new Vector3(transform.position.x, spawnYPosition, transform.position.z);

        Instantiate(prefab, spawnPosition, Quaternion.identity); // ���õ� �������� ����
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

        // ���� ������ ������ ��� ù ��° ������ ��ȯ (���� ó��)
        return 0;
    }
}
