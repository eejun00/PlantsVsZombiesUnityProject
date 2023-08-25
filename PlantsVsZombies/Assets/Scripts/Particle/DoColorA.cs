using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoColorA : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().DOFade(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
