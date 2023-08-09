using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallnut : Plant
{
    private GameObject fullHpImg;
    private GameObject halfHpImg;

    // Start is called before the first frame update
    void Start()
    {
        fullHpImg = gameObject.FindChildObject("FullHpWallnut");
        halfHpImg = gameObject.FindChildObject("HalfWallnut");
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHP < 50f)
        {
            fullHpImg.SetActive(false);
            if(currentHP < 25f)
            {
                halfHpImg.SetActive(false);
            }
        }

    }
}
