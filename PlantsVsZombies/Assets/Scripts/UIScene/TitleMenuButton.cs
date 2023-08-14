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
        //�ɼ�â �Ѵ� �ڵ�
    }

    public void OnClickHelpButton()
    {
        //���� �Ѵ� �ڵ�
    }

    public void OnClickQuitButton()
    {
        //����ų� �ٷ� ���� �����ϴ� �ڵ�
    }

    public void OnClickMainMenuButton()
    {
        GFunc.LoadScene("TitleSceneLJY");
    }

}
