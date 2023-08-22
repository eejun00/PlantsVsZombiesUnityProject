using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hypnoshroom : Plant
{
    public GameObject hypnoEffect;
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

        if (transform.parent != null)
        {
            if (transform.parent.CompareTag("Tile") && isPlanted == false)
            {
                isPlanted = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isPlanted && collision.CompareTag("Zombie"))
        {
            collision.GetComponent<Zombies>().TakeMad();
            Instantiate(hypnoEffect, collision.transform.position + Vector3.up, Quaternion.identity);
            Die();
        }
    }
}
