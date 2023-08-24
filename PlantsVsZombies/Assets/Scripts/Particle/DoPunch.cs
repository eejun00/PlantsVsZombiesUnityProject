using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoPunch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOPunchScale(new Vector3(0.1f,0.1f,0.1f), 500f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
