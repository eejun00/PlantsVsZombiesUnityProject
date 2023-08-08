using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // ���� ������
    public float spawnInterval = 5f; // ���� ���� ����

    private void Start()
    {
        // spawnInterval���� SpawnZombie �Լ��� ȣ��
        StartCoroutine(SpawnZombieRoutine());
    }

    IEnumerator SpawnZombieRoutine()
    {
        while (true)
        {
            // ���� �������� ���� ������ ��ġ�� ����
            Instantiate(zombiePrefab, transform.position, Quaternion.identity);

            // ���� ���� ���
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

