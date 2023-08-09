using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int cost = 50;
    public float maxHP;                     // 최대 체력
    private float currentHP;                 // 현재 체력
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;
    private void Awake()
    {
        currentHP = maxHP; // 게임 시작 시 현재 체력을 최대 체력으로 초기화
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage; // 데미지만큼 체력 감소

        if (currentHP <= 0)
        {
            Die(); // 체력이 0 이하면 식물 파괴 처리
        }
    }
}
