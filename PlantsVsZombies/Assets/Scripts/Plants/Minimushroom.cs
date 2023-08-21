using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimushroom : Plant
{
    public GameObject smallSunPrefab;
    public GameObject sunPrefab;

    private float sunSpawnSpeed = 5f;   // 햇빛 생산 스피드
    private float sunRespawnTime = default; //햇빛 생산 후의 시간
    private Transform sunSpawnPosition = null;
    private float growTime = 17.5f;
    private float growsec = 0f;
    private bool isGrow = false;

    private Vector2 lastPosition;
    private float timeSinceLastMove = 0f;
    private float maxIdleTime = 1f; // 1초

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
        // 현재 위치와 이전 위치를 비교하여 이동했는지 검사
        if (Vector2.Distance(transform.position, lastPosition) > 0.1f)
        {
            timeSinceLastMove = 0f; // 이동이 감지되면 시간을 초기화
        }
        else
        {
            timeSinceLastMove += Time.deltaTime; // 이동이 없으면 시간 증가
        }

        lastPosition = transform.position;

        // 1초 이상 움직이지 않았을 경우 특정 동작 실행
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
