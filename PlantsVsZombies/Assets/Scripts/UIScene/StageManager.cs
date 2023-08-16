using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int stagenum = default;
    private void Start()
    {
        if (GameManager.instance.isStageOneEnd == false)
        {
            GameManager.instance.stageOneNum = stagenum;
        }
    }
}
