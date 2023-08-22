using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypnoEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOShakeScale(0.7f);
        Destroy(gameObject,0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
