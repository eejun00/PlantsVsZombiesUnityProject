using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int cost = 50;
    public float maxHP;                     // �ִ� ü��
    private float currentHP;                 // ���� ü��
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;
    private void Awake()
    {
        currentHP = maxHP; // ���� ���� �� ���� ü���� �ִ� ü������ �ʱ�ȭ
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage; // ��������ŭ ü�� ����

        if (currentHP <= 0)
        {
            Die(); // ü���� 0 ���ϸ� �Ĺ� �ı� ó��
        }
    }
}
