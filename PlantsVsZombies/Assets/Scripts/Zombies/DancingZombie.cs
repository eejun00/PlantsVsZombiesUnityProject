using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingZombie : Zombies
{
    public GameObject spotlight;
    public GameObject backupZombiePrefab;
    private Transform spawnPoint;
    private float distance;
    private bool firstDance = false;
    private int danceCount = 3;
    Vector3[] backupZombiePositions = new Vector3[4];

    private GameObject[] backupZombies = new GameObject[4];
    //private bool ElementNull;

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
        if(spotlight != null)
        {
            spotlight.transform.position = transform.position;
        }

    }

    private void OnDestroy()
    {
        if(spotlight != null)
        { 
        Destroy(spotlight);
        }
    }

    public void SpawnBackupZombie()
    {
        spotlight = Instantiate(spotlight,transform.position,Quaternion.identity);
        backupZombiePositions[0] = new Vector3(transform.position.x - 2.5f, transform.position.y, transform.position.z);
        backupZombiePositions[1] = new Vector3(transform.position.x + 2.5f, transform.position.y, transform.position.z);
        backupZombiePositions[2] = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        backupZombiePositions[3] = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
        for (int i = 0; i < 4; i++)
        {
            backupZombies[i] = Instantiate(backupZombiePrefab, backupZombiePositions[i], Quaternion.identity);
            GameManager.instance.zombieDeathCount += 1;
            backupZombies[i].transform.localScale =
                new Vector3(backupZombies[i].transform.localScale.x, 0f, backupZombies[i].transform.localScale.z);
            backupZombies[i].transform.DOScaleY(1.05f, 0.5f);
        }

        if(transform.position.y < -4f)
        {
            backupZombies[3].GetComponent<Zombies>().Die();
        }
        else if(transform.position.y > 1.8)
        {
            backupZombies[2].GetComponent<Zombies>().Die();
        }

    }

    public void EndSpawnBackupZombie()
    {
        for(int i = 0; i < 4; i++)
        {
            if (backupZombies[i] != null)
            {
                backupZombies[i].GetComponent<Animator>().SetBool("Standing", false);
            }
        }
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

        //ElementNull = false;
        //for (int i = 0; i < backupZombies.Length; i++)
        //{
        //    if (backupZombies[i] == null)
        //    {
        //        ElementNull = true;
        //        break; 
        //    }
        //}

        if (danceCount < 3 /*|| ElementNull == false*/)
        {
            animator.SetTrigger("UpSunGlass");
            //danceCount += 1;
        }
        else /*if(ElementNull)*/
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
