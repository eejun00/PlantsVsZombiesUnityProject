using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
    private float bulletSpeed = 6f; // �Ѿ� �̵� �ӵ�
    public int bulletDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Zombie"))
        {
            //������ ������ ����
            Destroy(gameObject);
        }
    }
}
