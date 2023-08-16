using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conZombie : Zombies
{
    public GameObject fullcon;
    public GameObject halfcon;
    public GameObject quatercon;

    //Update is called once per frame
    void FixedUpdate()
    {

        if (currentHP <= 38)
        {
            fullcon.SetActive(false);
            halfcon.SetActive(true);
        }

        if (currentHP <= 30)
        {
            halfcon.SetActive(false);
            quatercon.SetActive(true);
        }

        if (currentHP <= 22)
        {
            quatercon.SetActive(false);
        }
    }

}
