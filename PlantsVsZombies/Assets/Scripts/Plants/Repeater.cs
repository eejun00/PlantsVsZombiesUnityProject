using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeater : Plant
{
    public GameObject bulletPrefab;

    private float attackSpeed = 2f;
    private float shootAfter = default;
    private Transform firePoint;

    private Vector2 lastPosition;
    private float timeSinceLastMove = 0f;
    private float maxIdleTime = 1f; // 1초

    private void Awake()
    {
        firePoint = gameObject.FindChildObject("FirePoint").transform;
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        shootAfter = 0f;
        lastPosition = transform.position;
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
            shootAfter += Time.deltaTime;
            if (shootAfter > attackSpeed)
            {
                shootAfter = 0f;
                StartCoroutine(DoubleShoot());
            }
        }
    }

    IEnumerator DoubleShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position
                    , Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position
            , Quaternion.identity);
        yield break;
    }
}
