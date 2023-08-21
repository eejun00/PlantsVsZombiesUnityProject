using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    //�����տ��� ���� ������ �����
    public int cost = 50;                   // �⺻ �ڽ�Ʈ
    public float maxHP;                     // �ִ� ü��
    public float currentHP;                 // ���� ü��
    public float coolTime = 5f;             // �⺻ ��Ÿ��
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;
    private void Awake()
    {
        
    }

    protected virtual void Start()
    {
        currentHP = maxHP; // ���� ���� �� ���� ü���� �ִ� ü������ �ʱ�ȭ
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        currentHP -= damage; // ��������ŭ ü�� ����

        if (currentHP <= 0)
        {
            Die(); // ü���� 0 ���ϸ� �Ĺ� �ı� ó��
        }
    }
}
