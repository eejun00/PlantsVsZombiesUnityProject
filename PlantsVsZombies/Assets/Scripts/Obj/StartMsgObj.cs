using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMsgObj : MonoBehaviour
{
    // 게임 플레이시 중앙에 나오는 문구 오브젝트들
    public GameObject readyMsg;
    public GameObject setMsg;
    public GameObject startMsg;
    public GameObject finalWaveMsg;

    //오브젝트들의 이미지 컴포넌트를 받아올 변수
    private Image readyImg;
    private Image setImg;

    private bool waveMsgFlag = false; //한번만 실행하기 위한 변수

    // Start is called before the first frame update
    void Start()
    {
        //게임 오브젝트의 이미지 컴포넌트 받아오기
        readyImg = readyMsg.GetComponent<Image>();
        setImg = setMsg.GetComponent<Image>();

        //씬 시작시 코루틴 시작
        StartCoroutine(ShowCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isWave == true && !waveMsgFlag)
        {
            StartCoroutine(ShowWaveMsg());
            waveMsgFlag = true;
        }   // if: 웨이브 시작을 게임매니저에서 받아와 확인
    }

    // 레디, 셋, 플랜트 문구 출력하는 코루틴
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
        GameManager.instance.stagePlaying = true;   // 출력이 끝날 시 게임 시작 변수를 true로 변경
    }

    //웨이브 메시지 출력하는 코루틴
    IEnumerator ShowWaveMsg()
    {
        finalWaveMsg.SetActive(true);
        finalWaveMsg.transform.DOShakePosition(1f, 50f);
        yield return new WaitForSeconds(1.0f);
        finalWaveMsg.SetActive(false);
    }
}
