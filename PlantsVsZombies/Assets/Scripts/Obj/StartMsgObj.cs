using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMsgObj : MonoBehaviour
{
    public GameObject readyMsg;
    public GameObject setMsg;
    public GameObject startMsg;
    public GameObject finalWaveMsg;
    private Image readyImg;
    private Image setImg;
    private bool waveMsgFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        readyImg = readyMsg.GetComponent<Image>();
        setImg = setMsg.GetComponent<Image>();
        StartCoroutine(ShowCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isWave == true && !waveMsgFlag)
        {
            StartCoroutine(ShowWaveMsg());
            waveMsgFlag = true;
        }
    }

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
        GameManager.instance.stagePlaying = true;
    }

    IEnumerator ShowWaveMsg()
    {
        finalWaveMsg.SetActive(true);
        finalWaveMsg.transform.DOShakePosition(1f,50f);
        yield return new WaitForSeconds(1.0f);
        finalWaveMsg.SetActive(false);
    }
}
