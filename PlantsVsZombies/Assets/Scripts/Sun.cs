using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sun : MonoBehaviour,IPointerClickHandler
{
    public int sunCost = 50;
    private float sunSpeed = 5f;
    private Transform sunDestination;
    private GameObject rootObjDest;

    private void Awake()
    {
        rootObjDest = GFunc.GetRootObject("UiCanvas");
        sunDestination = rootObjDest.FindChildObject("SunIcon").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("클릭했다.");
        GameManager.instance.AddCost(sunCost);
        transform.Translate(sunDestination.position * Time.deltaTime * sunSpeed);        
    }
}
