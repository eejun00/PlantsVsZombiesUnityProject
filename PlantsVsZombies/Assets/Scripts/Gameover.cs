using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zombie"))
        {
            GameManager.instance.EndGame(); // ����� �浹�ϸ� GameManager�� EndGame() ȣ���Ͽ� ���� ����
        }
    }
}
