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
            GameManager.instance.PassStage();
            GFunc.LoadScene("Stage1-4Scene");
        }
        else if(stageNum == 4)
        {
            GameManager.instance.PassStage();
            GFunc.LoadScene("Stage1-5Scene");
        }
        else if(stageNum == 5)
        {
            GameManager.instance.isStageOneEnd = true;
            GameManager.instance.PassStage();
            GFunc.LoadScene("TitleSceneLJY");           
        }
        else if(stageNum == 6)
        {
            GameManager.instance.PassStage();
            GFunc.LoadScene("Stage2-2Scene");
        }
        else if(stageNum == 7)
        {
            GameManager.instance.PassStage();
            GFunc.LoadScene("Stage2-3Scene");
        }
        else if (stageNum == 8)
        {
            GameManager.instance.PassStage();
            GFunc.LoadScene("Stage2-4Scene");
        }
        else if (stageNum == 9)
        {
            GameManager.instance.PassStage();
            GFunc.LoadScene("Stage2-5Scene");
        }
        else if (stageNum == 10)
        {
            GameManager.instance.PassStage();
            GFunc.LoadScene("EndingScene");
        }
    }
}
