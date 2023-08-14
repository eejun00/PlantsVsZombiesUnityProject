using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMsgObj : MonoBehaviour
{
    public GameObject readyMsg;
    public GameObject setMsg;
    public GameObject startMsg;
    public GameObject finalWaveMsg;
    private bool waveMsgFlag = false;

    // Start is called before the first frame update
    void Start()
    {
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
        yield return new WaitForSeconds(1.0f);
        readyMsg.SetActive(false);
        setMsg.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        setMsg.SetActive(false);
        startMsg.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        startMsg.SetActive(false);
        GameManager.instance.stagePlaying = true;
    }

    IEnumerator ShowWaveMsg()
    {
        finalWaveMsg.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        finalWaveMsg.SetActive(false);
    }
}
