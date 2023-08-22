using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shovel : MonoBehaviour
{
    private Image buttonImage; // 버튼 이미지
    private Vector3 originalPosition; // 원래 위치 저장
    private bool isButtonImageActive = false; // 버튼 이미지가 활성화되어 있는지 확인하는 변수

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        originalPosition = buttonImage.rectTransform.position;
    }

    private void Update()
    {
        // 마우스 좌클릭 시 식물 제거
        if (Input.GetMouseButtonDown(0) && isButtonImageActive)
        {
            RemovePlant();
        }

        // 버튼 이미지를 마우스 위치에 따라 이동
        if (isButtonImageActive)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            buttonImage.rectTransform.position = mousePos;
        }

        // 마우스 우클릭 시 버튼 이미지를 원래 위치로 돌아가게 설정
        if (Input.GetMouseButtonDown(1) && isButtonImageActive)
        {
            buttonImage.rectTransform.position = originalPosition;
            isButtonImageActive = false;
        }
    }

    public void OnButtonClick()
    {
        // 버튼 이미지 활성화 및 위치 설정
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        buttonImage.rectTransform.position = mousePos;
        isButtonImageActive = true;
    }

    private void RemovePlant()
    {
        Debug.Log("RemoveObjectsInTile() called."); // 함수가 호출되었는지 확인용 메시지 출력


        // 마우스 위치에서 Ray를 발사하여 충돌하는 오브젝트를 찾음
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // Ray에 충돌한 오브젝트가 있고 그 오브젝트의 태그가 "Tile"인 경우
        if (hit.collider != null && hit.collider.CompareTag("Tile"))
        {
            // 해당 오브젝트의 모든 자식 오브젝트 파괴
            foreach (Transform child in hit.collider.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}