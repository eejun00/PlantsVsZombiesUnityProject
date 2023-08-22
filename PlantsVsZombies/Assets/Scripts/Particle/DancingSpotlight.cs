using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingSpotlight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeColor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ChangeColor()
    {
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(1.5f);
            ChangeColorsRecursively(transform, new Color(1f, 0.5f, 1f, 0.65f));
            yield return new WaitForSeconds(1.5f);
            ChangeColorsRecursively(transform, new Color(1f, 0.37f, 0.58f, 0.65f));
            yield return new WaitForSeconds(1.5f);
            ChangeColorsRecursively(transform, new Color(0.5f, 0.5f, 0.5f, 0.65f));
            yield return new WaitForSeconds(1.5f);
            ChangeColorsRecursively(transform, new Color(1f, 0.39f, 0.39f, 0.65f));
            yield return new WaitForSeconds(1.0f);
            ChangeColorsRecursively(transform, new Color(1f, 1f, 1f, 0.65f));
        }
    }

    // 자식 오브젝트 컬러 바꿔주는 함수
    private void ChangeColorsRecursively(Transform parent, Color color_)
    {
        foreach (Transform child in parent)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color_;
            }

            // 자식 오브젝트에 대해 재귀적으로 실행
            ChangeColorsRecursively(child, color_);
        }
    }
}
