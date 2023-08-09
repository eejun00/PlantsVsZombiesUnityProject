using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Plant
{
    public GameObject sunPrefab;

    private float sunSpawnSpeed = 5f;   // �޺� ���� ���ǵ�
    private float sunRespawnTime = default; //�޺� ���� ���� �ð�
    private Transform sunSpawnPosition = null;

    private Vector2 lastPosition;
    private float timeSinceLastMove = 0f;
    private float maxIdleTime = 1f; // 1��

    // Start is called before the first frame update
    void Start()
    {
        sunSpawnPosition = gameObject.FindChildObject("Head").transform;
        sunSpawnPosition.position = new Vector3(sunSpawnPosition.position.x, sunSpawnPosition.position.y, 1f); 
        lastPosition = transform.position;
        sunRespawnTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ��ġ�� ���� ��ġ�� ���Ͽ� �̵��ߴ��� �˻�
        if (Vector2.Distance(transform.position, lastPosition) > 0.1f)
        {
            timeSinceLastMove = 0f; // �̵��� �����Ǹ� �ð��� �ʱ�ȭ
        }
        else
        {
            timeSinceLastMove += Time.deltaTime; // �̵��� ������ �ð� ����
        }

        lastPosition = transform.position;

        // 1�� �̻� �������� �ʾ��� ��� Ư�� ���� ����
        if (timeSinceLastMove >= maxIdleTime)
        {
            sunRespawnTime += Time.deltaTime;
            if(sunRespawnTime > sunSpawnSpeed)
            {
                sunRespawnTime = 0f;
                GameObject sun = Instantiate(sunPrefab, sunSpawnPosition.position, transform.rotation);                
            }
        }
    }
}
