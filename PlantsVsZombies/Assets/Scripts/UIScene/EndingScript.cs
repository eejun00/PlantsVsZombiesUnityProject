using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().DOFade(1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
