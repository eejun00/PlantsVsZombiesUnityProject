using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int stagenum = default;
    public bool isSuvival = false;
    private void Start()
    {
        if (!isSuvival)
        {
            GameManager.instance.stageOneNum = stagenum;

        }
    }
}
