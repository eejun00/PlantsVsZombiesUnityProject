using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuButton : MonoBehaviour
{
    public void OnClickStage1Button()
    {
        GFunc.LoadScene("Stage1Scene");
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
