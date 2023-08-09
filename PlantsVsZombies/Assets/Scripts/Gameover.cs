using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zombie"))
        {
            GameManager.instance.EndGame(); // 좀비와 충돌하면 GameManager의 EndGame() 호출하여 게임 오버
        }
    }
}
