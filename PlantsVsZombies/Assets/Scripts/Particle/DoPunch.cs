using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoPunch : MonoBehaviour
{
    public int vibrato = 10;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOPunchScale(new Vector3(0.1f,0.1f,0.1f), 500f, vibrato);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
