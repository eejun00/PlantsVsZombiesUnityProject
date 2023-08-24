using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int stagenum = default;

    private void Start()
    {
        if(stagenum == 0)
        {
            /* Pass*/
        }
        else
        {
            GameManager.instance.stageOneNum = stagenum;
        }
        GameManager.instance.isGameover = false;
        GameManager.instance.stagePlaying = false;
        GameManager.instance.isWave = false;
    }
}
