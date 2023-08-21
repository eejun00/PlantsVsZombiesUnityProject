using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimushroom : Plant
{
    public GameObject smallSunPrefab;
    public GameObject sunPrefab;

    private float sunSpawnSpeed = 5f;   // �޺� ���� ���ǵ�
    private float sunRespawnTime = default; //�޺� ���� ���� �ð�
    private Transform sunSpawnPosition = null;
    private float growTime = 17.5f;
    private float growsec = 0f;
    private bool isGrow = false;

    private Vector2 lastPosition;
    private float timeSinceLastMove = 0f;
    private float maxIdleTime = 1f; // 1��

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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
            if (sunRespawnTime > sunSpawnSpeed)
            {
                sunRespawnTime = 0f;
                if (!isGrow)
                {
                    GameObject smallSun = Instantiate(smallSunPrefab, sunSpawnPosition.position, transform.rotation);
                }
                else
                {
                    GameObject sun = Instantiate(sunPrefab, sunSpawnPosition.position, transform.rotation);
                }
            }

        }

        if(growsec < growTime)
        {
            growsec += Time.deltaTime;
        }
        
        if(growsec >= growTime && !isGrow)
        {
            gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
            isGrow = true;
        }

    }
}
