using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
    private float bulletSpeed = 6f; // 총알 이동 속도
    public float bulletDamage = 1f;
    private Zombies zombies;    // 데미지를 받을 좀비 저장 변수

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
            zombies = collision.GetComponent<Zombies>();
            if(zombies != null)
            {
                zombies.TakeDamage(bulletDamage);
                Destroy(gameObject);
            }
        }
    }
}
