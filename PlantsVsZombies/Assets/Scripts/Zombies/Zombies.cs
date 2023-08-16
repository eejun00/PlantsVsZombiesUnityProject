using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombies : MonoBehaviour
{
    [SerializeField]
    private float maxHP;                     // �ִ� ü��
    public float currentHP;                 // ���� ü��
    private bool isDie = false;              // ���� ��� �����̸� isDie�� true�� ����
    public float zombieAD = 1f;             // ������ ���� ������ ��
    private bool isMeetPlant = false;
    public float moveSpeed = 2f; // ���� �̵� �ӵ�
    public float attackSpeed = 1f; // ���� ���� �ӵ�
    private float attackAfter = default; //���� �����ϰ� �� �帥 �ð�
    private Plant plant; // ���� ����ģ �Ĺ��� �޾ƿ� ����

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;   //���� ü���� �ִ� ü�°� ���� ����
    }

    private void Start()
    {
        attackAfter = 0f;
    }

    private void Update()
    {
        if(isMeetPlant == true)
        {
            if(plant != null)
            {
                attackAfter += Time.deltaTime;
                if (attackAfter > attackSpeed)
                {
                    plant.TakeDamage(zombieAD); // ������ ������
                    attackAfter = 0f;
                }
            }
        }
        else
        {
            // ���� �������� �̵���Ŵ
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            // ���� �������� ����� �� ����
            if (transform.position.x < -10.3f)
            {
                Destroy(gameObject);
            }
        }       
        
        if(isDie == true)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        // Tip. ���� ü���� damage ��ŭ �����ؼ� ���� ��Ȳ�� �� ���� Ÿ���� ������ ���ÿ� ������
        // enemy.OnDie() �Լ��� ���� �� ����� �� �ִ�.

        // ���� ���� ���°� ��� �����̸� �Ʒ� �ڵ带 �������� �ʴ´�.
        if (isDie == true) return;

        // ���� ü���� damage ��ŭ ���� 
        currentHP -= damage;

        // ü���� 0���� = �� ĳ���� ���
        if (currentHP <= 0)
        {
            isDie = true;
            // �� ĳ���� ���
            //enemy.OnDie(EnemyDestroyType.Kill);
        }
    }
    private void Die()
    {
        // ���� ����� ���� ó��, ���� ��� ��� �ִϸ��̼� ��� ���� ������ �� ����
        Destroy(gameObject); // �ϴ� ������Ʈ �ı��� ó��
        GameManager.instance.zombieDeathCount -= 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Plant"))
        {
            plant = other.GetComponent<Plant>(); // �浹�� ������Ʈ�� Plant ������Ʈ ��������
            if (plant != null)
            {
                isMeetPlant = true;                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag.Equals("Plant"))
        {
            isMeetPlant = false;
            plant = null;
        }
    }
}
