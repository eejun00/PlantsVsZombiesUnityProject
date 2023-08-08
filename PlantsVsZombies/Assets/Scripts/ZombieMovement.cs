using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // ���� �̵� �ӵ�

    private void Update()
    {
        // ���� �������� �̵���Ŵ
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // ���� �������� ����� �� ����
        if (transform.position.x < -10.3f)
        {
            Destroy(gameObject);
        }
    }
}