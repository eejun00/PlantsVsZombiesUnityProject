using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shovel : MonoBehaviour
{
    private Image buttonImage; // ��ư �̹���
    private bool isButtonImageActive = false; // ��ư �̹����� Ȱ��ȭ�Ǿ� �ִ��� Ȯ���ϴ� ����

    private Transform shovelBankTransform; // ShovelBank ������Ʈ�� Transform ������Ʈ

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        shovelBankTransform = transform.parent; // �� �̹����� �θ� ������Ʈ�� ShovelBank�� ����
    }

    private void Update()
    {
        // ���콺 ��Ŭ�� �� �Ĺ� ����
        if (Input.GetMouseButtonDown(0) && isButtonImageActive)
        {
            RemovePlant();

        }

        // �ƹ� Ű�� �Է��ϸ� ��ư �̹����� ShovelBank�� ��ġ�� ���ư��� ��
        if (isButtonImageActive && Input.anyKeyDown)
        {
            buttonImage.rectTransform.position = shovelBankTransform.position; // �ƹ� Ű�� �Է��ϸ� �� �̹����� ShovelBank ��ġ�� ���ư��� ��
            isButtonImageActive = false;
        }

        // ��ư �̹����� ���콺 ��ġ�� ���� �̵�
        if (isButtonImageActive)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            buttonImage.rectTransform.position = mousePos;
        }
    }

    public void OnButtonClick()
    {
        // ��ư �̹��� Ȱ��ȭ �� ��ġ ����
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        buttonImage.rectTransform.position = mousePos;
        isButtonImageActive = true;
    }

    private void RemovePlant()
    {
        // ���콺 ��ġ���� Ray�� �߻��Ͽ� �浹�ϴ� ������Ʈ�� ã��
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // Ray�� �浹�� ������Ʈ�� �ְ� �� ������Ʈ�� �±װ� "Tile"�� ���
        if (hit.collider != null && hit.collider.CompareTag("Tile"))
        {
            // �ش� ������Ʈ�� ��� �ڽ� ������Ʈ �ı�
            foreach (Transform child in hit.collider.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}