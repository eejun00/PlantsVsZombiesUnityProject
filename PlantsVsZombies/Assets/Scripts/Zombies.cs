using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombies : MonoBehaviour
{
    [SerializeField]
    private float maxHP;                     // �ִ� ü��
    private float currentHP;                 // ���� ü��
    private bool isDie = false;              // ���� ��� �����̸� isDie�� true�� ����
    public int zombieAD = 1;             // ������ ���� ������ ��

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;   //���� ü���� �ִ� ü�°� ���� ����
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Plant"))
        {
            Plant plant = other.GetComponent<Plant>(); // �浹�� ������Ʈ�� Plant ������Ʈ ��������

            if (plant != null)
            {
                plant.TakeDamage(zombieAD); // ������ ������
            }
        }
    }
}
