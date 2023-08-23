using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuButton : MonoBehaviour
{
    public void OnClickStage1Button(int stageNum)
    {
        if(GameManager.instance.stageOneNum >= 1)
        {
            stageNum = GameManager.instance.stageOneNum;
        }
        if(stageNum == 0 || stageNum == 1)
        {
            GFunc.LoadScene("Stage1-1Scene");
        }
        else if(stageNum == 2)
        {
            GFunc.LoadScene("Stage1-2Scene");
        }
        else if(stageNum == 3)
        {
            GFunc.LoadScene("Stage1-3Scene");
        }
        else if(stageNum == 4)
        {
            GFunc.LoadScene("Stage1-4Scene");
        }
        else if (stageNum == 5)
        {
            GFunc.LoadScene("Stage1-5Scene");
        }
    }

    public void OnClickStage2Button()
    {
        if (GameManager.instance.isStageOneEnd)
        {
            if (GameManager.instance.stageOneNum == 5 || GameManager.instance.stageOneNum == 6)
            {
                GFunc.LoadScene("Stage2-1Scene");
            }
            if (GameManager.instance.stageOneNum == 7)
            {
                GFunc.LoadScene("Stage2-2Scene");
            }
        }
    }

    public void OnClickStageSurvival()
    {
        GFunc.LoadScene("StageSurvive");
    }

    public void OnClickOptionButton()
    {
        //옵션창 켜는 코드
    }

    public void OnClickHelpButton()
    {
        //도움말 켜는 코드
    }

    public void OnClickQuitButton()
    {
        //물어보거나 바로 게임 종료하는 코드
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

    public void OnClickMainMenuButton()
    {
        if(GameManager.instance.stageOneNum >= 5)
        {
            GameManager.instance.isStageOneEnd = true;
        }
        GameManager.instance.stageOneNum += 1;
        GFunc.LoadScene("TitleSceneLJY");
    }
}
