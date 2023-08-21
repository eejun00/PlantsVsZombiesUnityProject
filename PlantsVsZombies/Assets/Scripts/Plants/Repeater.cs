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
    private float maxIdleTime = 1f; // 1��

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
