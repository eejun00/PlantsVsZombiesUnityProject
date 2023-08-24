using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveCamera());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator MoveCamera()
    {
        while (true)
        {
            transform.DOMoveX(-0.5f, 3f);
            yield return new WaitForSeconds(3.0f);
            transform.DOMoveX(-2.5f, 3f);
            yield return new WaitForSeconds(3.0f);
        }
    }
}
