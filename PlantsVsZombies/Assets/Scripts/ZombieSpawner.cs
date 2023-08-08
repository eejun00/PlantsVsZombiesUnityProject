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

    private void Start()
    {
        // spawnInterval���� SpawnZombie �Լ��� ȣ��
        StartCoroutine(SpawnZombieRoutine());
    }

    IEnumerator SpawnZombieRoutine()
    {
        while (true)
        {
            // ������ �ε��� ����
            int randomIndex = Random.Range(0, allowedYPositions.Length);

            // ���õ� �ε����� �ش��ϴ� y��ǥ ��������
            float spawnYPosition = allowedYPositions[randomIndex];

            // ���� ���� ��ġ ����
            Vector3 spawnPosition = new Vector3(transform.position.x, spawnYPosition, transform.position.z);

            // ���� �������� ���� ��ġ�� ����
            Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);

            // ���� ���� ���
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}