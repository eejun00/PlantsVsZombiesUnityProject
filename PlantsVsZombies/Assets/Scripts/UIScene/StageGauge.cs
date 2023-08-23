using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageGauge : MonoBehaviour
{
    public GameObject fillImageObj; // 꽉 찬 게이지 이미지
    public GameObject zombieHead;   // 좀비 머리 오브젝트
    private Image fillImage;
    public float fillDuration = 45.0f; // 채워지는 시간
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
            }   // if: 채워지기 전 딜레이 시간이 지나지 않은 경우
            else
            {
                hasStartedFilling = true;
                fillImageObj.SetActive(true);
                currentTime = 0.0f;
            }   // else: 딜레이 시간이 모두 지난경우
        }
        else if (currentTime < fillDuration)
        {           
            currentTime += Time.deltaTime;
            Vector3 increaseAmount = new Vector3(230f / fillDuration, 0.0f, 0.0f); // 로컬 포지션 증가량
            zombieHead.transform.localPosition -= increaseAmount * Time.deltaTime;
            float fillAmount = Mathf.Lerp(0, 1, currentTime / fillDuration);
            fillImage.fillAmount = fillAmount;
        }
    }
}
