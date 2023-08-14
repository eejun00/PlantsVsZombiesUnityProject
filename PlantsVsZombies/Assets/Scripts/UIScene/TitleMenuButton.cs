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
    }

    public void OnClickStage2Button()
    {
        GFunc.LoadScene("Stage2Scene");
    }

    public void OnClickStage3Button()
    {
        GFunc.LoadScene("Stage3Scene");
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
    }

    public void OnClickMainMenuButton()
    {
        GFunc.LoadScene("TitleSceneLJY");
    }

}
