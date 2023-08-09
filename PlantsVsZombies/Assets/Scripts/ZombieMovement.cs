using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // 좀비 이동 속도

    private void Update()
    {
        // 좀비를 왼쪽으로 이동시킴
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // 좀비가 왼쪽으로 벗어났을 때 제거
        if (transform.position.x < -10.3f)
        {
            Destroy(gameObject);
        }
    }
}