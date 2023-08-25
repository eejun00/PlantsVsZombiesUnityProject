using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    private float startTime = default;
    private float readyTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime <= readyTime)
        {
            startTime += Time.deltaTime;
        }
        else
        {
            if (Input.anyKey)
            {
                GFunc.LoadScene("TitleSceneLJY");
            }
        }      
    }
}
