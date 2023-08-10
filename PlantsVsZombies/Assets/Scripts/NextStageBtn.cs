using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageBtn : MonoBehaviour
{
    public void OnClickNextStageOneBtn()
    {
        int stageNum = GameManager.instance.stageOneNum;
        if(stageNum == 0)
        {
            GFunc.LoadScene("Ending");
        }
        else if(stageNum == 1)
        {
            GFunc.LoadScene("Stage1-2Scene");
            GameManager.instance.PassStage();
        }
        else if(stageNum == 2)
        {
            GFunc.LoadScene("Stage1-3Scene");
            GameManager.instance.PassStage();
        }
        else if(stageNum == 3)
        {
            GameManager.instance.isStageOneEnd = true;
            GFunc.LoadScene("Ending");
        }
    }
}
