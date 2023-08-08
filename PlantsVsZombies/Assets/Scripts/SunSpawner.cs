using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSpawner : MonoBehaviour
{
    public GameObject sun;
    private float spawnTime;
    private float respawnTime = 5f;
    private float randomX = default;
    private float posY = default;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 5f;
        posY = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;
        if(spawnTime > respawnTime) 
        {
            spawnTime = 0f;
            randomX = Random.Range(-6.5f, 4f);
            Vector3 spawnPos = new Vector3(randomX, posY, transform.position.z);
            GameObject sunClone = Instantiate(sun, spawnPos, transform.rotation);
        }
    }
}
