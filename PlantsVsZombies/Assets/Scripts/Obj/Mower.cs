using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Mower : MonoBehaviour
{
    private bool isMeetZombie = false;  // ���� ���ʱ⸦ �ǵ�ȴ��� Ȯ���ϴ� ����
    private Zombies zombie;             // ���ʱⰡ �о���� ������ ��ũ��Ʈ�� �޾ƿ�
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
        }   // if: ���ʱ⿡ ���� ����� ���
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            isMeetZombie = true;
            zombie = collision.GetComponent<Zombies>();
            zombie.isMeetMower = true;
        }   // if: ���ʱ��� �ݶ��̴��� ���� �±��� �ݶ��̴��� ����� ���
    }
}
