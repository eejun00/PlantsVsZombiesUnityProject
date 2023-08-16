using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketZombie : Zombies
{
    public GameObject fullBucket;
    public GameObject halfBucket;
    public GameObject quaterBucket;

    //Update is called once per frame
    void FixedUpdate()
    {

        if (currentHP <= 42)
        {
            fullBucket.SetActive(false);
            halfBucket.SetActive(true);
        }

        if (currentHP <= 32)
        {
            halfBucket.SetActive(false);
            quaterBucket.SetActive(true);
        }

        if (currentHP <= 22)
        {
            quaterBucket.SetActive(false);
        }
    }

}
