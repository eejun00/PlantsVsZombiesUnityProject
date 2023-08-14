using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuffShroom : Plant
{
    public GameObject bulletPrefab;

    private float attackSpeed = 2f;
    private float shootAfter = default;
    private Transform firePoint;

    private Vector2 lastPosition;
    private float timeSinceLastMove = 0f;
    private float maxIdleTime = 1f; // 1��

    private void Awake()
    {
        firePoint = gameObject.FindChildObject("FirePoint").transform;
        currentHP = maxHP;
    }
    // Start is called before the first frame update
    void Start()
    {
        shootAfter = 0f;
        lastPosition = transform.position;
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
            shootAfter += Time.deltaTime;
            if (shootAfter > attackSpeed)
            {
                shootAfter = 0f;
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position
                    , Quaternion.identity);
            }
        }
    }
}
