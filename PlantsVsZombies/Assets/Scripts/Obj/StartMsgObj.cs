using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMsgObj : MonoBehaviour
{
    // ���� �÷��̽� �߾ӿ� ������ ���� ������Ʈ��
    public GameObject readyMsg;
    public GameObject setMsg;
    public GameObject startMsg;
    public GameObject finalWaveMsg;

    //������Ʈ���� �̹��� ������Ʈ�� �޾ƿ� ����
    private Image readyImg;
    private Image setImg;

    private bool waveMsgFlag = false; //�ѹ��� �����ϱ� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        //���� ������Ʈ�� �̹��� ������Ʈ �޾ƿ���
        readyImg = readyMsg.GetComponent<Image>();
        setImg = setMsg.GetComponent<Image>();

        //�� ���۽� �ڷ�ƾ ����
        StartCoroutine(ShowCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isWave == true && !waveMsgFlag)
        {
            StartCoroutine(ShowWaveMsg());
            waveMsgFlag = true;
        }   // if: ���̺� ������ ���ӸŴ������� �޾ƿ� Ȯ��
    }

    // ����, ��, �÷�Ʈ ���� ����ϴ� �ڷ�ƾ
    IEnumerator ShowCountdown()
    {
        yield return new WaitForSeconds(1.0f);
        readyMsg.SetActive(true);
        readyImg.transform.DOShakePosition(1f, 20f);
        readyImg.DOFade(0f, 3f);
        yield return new WaitForSeconds(1.0f);
        readyMsg.SetActive(false);
        setMsg.SetActive(true);
        setMsg.transform.DOShakePosition(1f, 20f);
        setImg.DOFade(0f, 3f);
        yield return new WaitForSeconds(1.0f);
        setMsg.SetActive(false);
        startMsg.SetActive(true);
        startMsg.transform.DOShakeScale(1f);
        yield return new WaitForSeconds(1.0f);
        startMsg.SetActive(false);
        GameManager.instance.stagePlaying = true;   // ����� ���� �� ���� ���� ������ true�� ����
    }

    //���̺� �޽��� ����ϴ� �ڷ�ƾ
    IEnumerator ShowWaveMsg()
    {
        finalWaveMsg.SetActive(true);
        finalWaveMsg.transform.DOShakePosition(1f, 50f);
        yield return new WaitForSeconds(1.0f);
        finalWaveMsg.SetActive(false);
    }
}
