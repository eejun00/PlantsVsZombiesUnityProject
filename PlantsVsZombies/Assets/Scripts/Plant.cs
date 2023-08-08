using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int cost = 50;

    private void Awake()
    {

    }

    void Update()
    {



    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
