//using System.Collections;
//using UnityEngine;

//public class ZombieSpawner : MonoBehaviour
//{
//    public GameObject zombiePrefab; // ���� ������
//    public float spawnInterval = 5f; // ���� ���� ����

//    private void Start()
//    {
//        // spawnInterval���� SpawnZombie �Լ��� ȣ��
//        StartCoroutine(SpawnZombieRoutine());
//    }

//    IEnumerator SpawnZombieRoutine()
//    {
//        while (true)
//        {
//            // ���� �������� ���� ������ ��ġ�� ����
//            Instantiate(zombiePrefab, transform.position, Quaternion.identity);

//            // ���� ���� ���
//            yield return new WaitForSeconds(spawnInterval);
//        }
//    }
//}

using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // ���� ������
    public float spawnInterval = 5f; // ���� ���� ����
    public float[] allowedYPositions; // ���� ���� ��� y��ǥ �迭
    public int zombieCount = 5;

    private void Start()
    {
        // spawnInterval���� SpawnZombie �Լ��� ȣ��
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
    }
}