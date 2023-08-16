using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageGauge : MonoBehaviour
{
    public GameObject fillImageObj; // 꽉 찬 게이지 이미지
    private Image fillImage;
    public float fillDuration = 40.0f; // 채워지는 시간
    public float delayBeforeFill = 5.0f; // 채워지기 전 딜레이 시간

    private float currentTime = 0.0f;
    private bool hasStartedFilling = false; // 게이지 채우기 시작 확인

    private void Start()
    {
        fillImageObj.SetActive(false);
        fillImage = fillImageObj.GetComponent<Image>();
    }

    void Update()
    {
        if (!hasStartedFilling)
        {
            if (currentTime < delayBeforeFill)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                hasStartedFilling = true;
                fillImageObj.SetActive(true);
                currentTime = 0.0f;
            }
        }
        else if (currentTime < fillDuration)
        {
            currentTime += Time.deltaTime;
            float fillAmount = Mathf.Lerp(0, 1, currentTime / fillDuration);
            fillImage.fillAmount = fillAmount;
        }
    }
}
