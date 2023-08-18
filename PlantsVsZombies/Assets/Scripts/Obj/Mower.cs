using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Mower : MonoBehaviour
{
    private bool isMeetZombie = false;  // 좀비가 예초기를 건드렸는지 확인하는 변수
    private Zombies zombie;             // 예초기가 밀어버릴 좀비의 스크립트를 받아옴
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMeetZombie)
        {
            animator.enabled = true;
            transform.Translate(Vector2.right * Time.deltaTime * 6.0f);
            Destroy(gameObject, 5.0f);
        }   // if: 예초기에 좀비가 닿았을 경우
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            isMeetZombie = true;
            zombie = collision.GetComponent<Zombies>();
            zombie.isMeetMower = true;
        }   // if: 예초기의 콜라이더에 좀비 태그의 콜라이더가 닿았을 경우
    }
}
