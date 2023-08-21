using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceshroom : Plant
{
    private bool isPlanted = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        isPlanted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.CompareTag("Tile") && isPlanted == false)
        {
            isPlanted = true;
            BoomIceshroom();
        }
    }

    private void BoomIceshroom()
    {

    }
}
