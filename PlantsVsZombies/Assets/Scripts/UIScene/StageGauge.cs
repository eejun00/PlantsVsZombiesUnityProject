using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageGauge : MonoBehaviour
{
    public GameObject fillImageObj; // �� �� ������ �̹���
    private Image fillImage;
    public float fillDuration = 40.0f; // ä������ �ð�
    public float delayBeforeFill = 5.0f; // ä������ �� ������ �ð�

    private float currentTime = 0.0f;
    private bool hasStartedFilling = false; // ������ ä��� ���� Ȯ��

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
