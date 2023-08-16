using System.Collections;
using System.Linq;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // ���� ������
    public GameObject flagZombiePrefab; // ��� ���� ������

    //�Ʒ��� �ΰ��� ����Ʈ�� ������ ������ �¾ƾ���
    public GameObject[] otherZombieList;
    public int[] otherZombieCount;
    
    public float spawnInterval = 5f; // ���� ���� ����
    public float[] allowedYPositions; // ���� ���� ��� y��ǥ �迭
    public int zombieCount = 5;
    public int waveCount = 3;

    private bool coroutineFlag = false;


    private void Start()
    {
        if(GameManager.instance.stageOneNum < 3)
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
        if (waveCount <= 0 && GameManager.instance.isStageClear == false)
        {
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

            // ���� �������� ���� ��ġ�� ����
            Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            zombieCount -= 1;
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

                // ���� �������� ���� ��ġ�� ����
                Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
                waveCount -= 1;
                // ���� ���� ���
                yield return new WaitForSeconds(2.0f);
            }
        }
    }
}