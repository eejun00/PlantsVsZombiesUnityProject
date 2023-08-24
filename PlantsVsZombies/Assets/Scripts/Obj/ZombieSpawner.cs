using System.Collections;
using System.Linq;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;         // ���� ������
    public GameObject flagZombiePrefab;     // ��� ���� ������
    public GameObject conZombieprefab;      // �� ���� ������
    public GameObject buketZombiePrefab;    // ���� ���� ������
    public GameObject dancingZombiePrefab;  // ��� ���� ������
    public GameObject backupZombiePrefab;   // ��� ���� ������

    //�Ʒ��� �ΰ��� ����Ʈ�� ������ ������ �¾ƾ���
    public GameObject[] otherZombieList;    // 0:�⺻ 1:�� 2:���� 3:����
    public int[] otherZombieCount;

    public float spawnInterval = 5f; // ���� ���� ����
    public float[] allowedYPositions; // ���� ���� ��� y��ǥ �迭
    public int zombieCount = 5;
    public int waveCount = 3;

    private bool isSpawnBoss = false;
    private bool coroutineFlag = false;


    private void Start()
    {
        if (GameManager.instance.stageOneNum < 3)
        {
            waveCount = 0;
        }
        // spawnInterval���� SpawnZombie �Լ��� ȣ��
        GameManager.instance.zombieDeathCount = zombieCount + waveCount;
    }

    private void Update()
    {
        if (coroutineFlag == false && GameManager.instance.stagePlaying)
        {
            coroutineFlag = true;
            StartCoroutine(SpawnZombieRoutine());
        }

        if (waveCount <= 0 && GameManager.instance.isStageClear == false && GameManager.instance.stagePlaying)
        {
            if (GameManager.instance.stageOneNum == 5)
            {
                if (!isSpawnBoss)
                {
                    GameManager.instance.zombieDeathCount += 1;
                    isSpawnBoss = true;
                    GameObject stageOneBoss = Instantiate(backupZombiePrefab,
                        new Vector3(transform.position.x, -4f, transform.position.z), Quaternion.identity);
                    stageOneBoss.GetComponent<BackupZombie>().currentHP = 200f;
                    stageOneBoss.GetComponent<BackupZombie>().zombieAD = 50f;
                    stageOneBoss.GetComponent<BackupZombie>().attackSpeed = 2f;
                    stageOneBoss.transform.localScale = stageOneBoss.transform.localScale * 3f;
                    stageOneBoss.GetComponent<Animator>().SetBool("Standing", false);
                }
            }
            if (GameManager.instance.stageOneNum == 10)
            {
                if (!isSpawnBoss)
                {
                    GameManager.instance.zombieDeathCount += 2;
                    isSpawnBoss = true;
                    GameObject stageTwoBoss = Instantiate(otherZombieList[3],
                        new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);
                    GameObject stageTwoBoss_ = Instantiate(otherZombieList[3],
                        new Vector3(transform.position.x, -2.7f, transform.position.z), Quaternion.identity);
                }
            }
            GameManager.instance.isWave = false;
            GameManager.instance.isStageClear = true;
        }
    }

    IEnumerator SpawnZombieRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (zombieCount > 0)
        {
            // ������ �ε��� ����
            int randomIndex = Random.Range(0, allowedYPositions.Length);

            // ���õ� �ε����� �ش��ϴ� y��ǥ ��������
            float spawnYPosition = allowedYPositions[randomIndex];

            // ���� ���� ��ġ ����
            Vector3 spawnPosition = new Vector3(transform.position.x, spawnYPosition, transform.position.z);

            // ���� ����
            SpawnZombie(spawnPosition);

            // ���� ���� ���
            yield return new WaitForSeconds(spawnInterval);
        }


        if (waveCount > 0)
        {
            yield return new WaitForSeconds(2.0f);

            GameManager.instance.isWave = true;

            yield return new WaitForSeconds(2.0f);

            // ��� ���� �������� ���� ��ġ�� ����
            Instantiate(flagZombiePrefab, new Vector3(transform.position.x, -1.3f, transform.position.z), Quaternion.identity);
            waveCount -= 1;
            // ���� ���� ���
            yield return new WaitForSeconds(2.0f);

            while (waveCount > 0)
            {
                // ������ �ε��� ����
                int randomIndex = Random.Range(0, allowedYPositions.Length);

                // ���õ� �ε����� �ش��ϴ� y��ǥ ��������
                float spawnYPosition = allowedYPositions[randomIndex];

                // ���� ���� ��ġ ����
                Vector3 spawnPosition = new Vector3(transform.position.x, spawnYPosition, transform.position.z);

                // ���� ����
                SpawnZombie(spawnPosition);
                waveCount -= 1;
                // ���� ���� ���
                yield return new WaitForSeconds(2.0f);
            }
        }
    }

    private void SpawnZombie(Vector3 spawnPosition)
    {
        if (GameManager.instance.stageOneNum < 4)
        {
            // ���� �������� ���� ��ġ�� ����
            Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            zombieCount -= 1;
        }
        else if (GameManager.instance.stageOneNum < 5)
        {
            int randomIdx = Random.Range(0, 3);
            Instantiate(otherZombieList[randomIdx], spawnPosition, Quaternion.identity);
            zombieCount -= 1;
        }
        else if (GameManager.instance.stageOneNum < 7)
        {
            int randomIdx = Random.Range(0, 4);
            Instantiate(otherZombieList[randomIdx], spawnPosition, Quaternion.identity);
            zombieCount -= 1;
        }
        else
        {
            int randomIdx = Random.Range(0, 5);
            Instantiate(otherZombieList[randomIdx], spawnPosition, Quaternion.identity);
            zombieCount -= 1;
        }
    }
}