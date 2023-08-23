using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamoverMenu : MonoBehaviour
{
    private bool isOnPanel = false;
    public GameObject menuPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isOnPanel && Input.anyKeyDown)
        {
            isOnPanel = true;
            menuPanel.SetActive(true);
        }
    }
}
