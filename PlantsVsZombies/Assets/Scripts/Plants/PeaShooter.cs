using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Plant
{
    public GameObject bulletPrefab;

    private float maxHp = 5f;
    private float attackSpeed = 2f;
    private float shootAfter = default;
    private Transform firePoint;


    private void Awake()
    {
        firePoint = gameObject.FindChildObject("FirePoint").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        shootAfter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        shootAfter += Time.deltaTime;
        if(shootAfter > attackSpeed)
        {
            shootAfter = 0f;
            GameObject bullet = Instantiate(bulletPrefab,firePoint.position
                , Quaternion.identity);
        }
    }
}
