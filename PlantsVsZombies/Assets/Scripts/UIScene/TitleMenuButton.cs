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
