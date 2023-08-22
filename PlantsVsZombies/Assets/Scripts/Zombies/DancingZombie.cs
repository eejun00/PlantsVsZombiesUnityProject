using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingZombie : Zombies
{
    private Transform spawnPoint;
    private float distance;
    private bool firstDance = false;
    private int danceCount = 3;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        animator = GetComponent<Animator>();
        spawnPoint = transform;        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, spawnPoint.position);
        if(distance >= 3.0f)
        {
            spawnPoint = transform;
            
        }
    }

    public void SpawnBackupZombie()
    {
        Debug.Log("½ÇÇàµÊ");
    }

    public void EndSpawnBackupZombie()
    {
        animator.SetTrigger("EndSpawnMotion");
        moveSpeed = beforeSpeed;
    }

    public void PlayUpSunGlass()
    {
        if(firstDance == false)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            firstDance = true;
        }
        beforeSpeed = moveSpeed;
        moveSpeed = 0f;
        if (danceCount < 3)
        {
            animator.SetTrigger("UpSunGlass");
            danceCount += 1;
        }
        else
        {
            animator.SetTrigger("SpawnMotion");
            danceCount = 0;
        }

    }

    public void PlayEndSunGlass()
    {
        moveSpeed = beforeSpeed;
        animator.SetTrigger("EndSunGlass");
    }
}
