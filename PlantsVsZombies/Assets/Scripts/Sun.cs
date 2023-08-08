using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sun : MonoBehaviour,IPointerClickHandler
{
    public int sunCost = 50;
    private float sunSpeed = 10f;
    private Transform sunDestination;
    private GameObject rootObjDest;
    private bool isClick = default;
    private float distance = default;
    private float downDistanceY = default;

    private void Awake()
    {
        rootObjDest = GFunc.GetRootObject("UiCanvas");
        sunDestination = rootObjDest.FindChildObject("SunIcon").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        isClick = false;
        downDistanceY = Random.Range(-2.5f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isClick)
        {
            distance = Vector2.Distance(transform.position, sunDestination.position);
            Vector2 moveDirection = (sunDestination.position - transform.position).normalized;
            transform.Translate(moveDirection * Time.deltaTime * sunSpeed);
            if(distance < 1f)
            {
                GameManager.instance.AddCost(sunCost);
                Destroy(gameObject);
            }
        }
        else
        {
            if (gameObject.transform.position.y > downDistanceY)
            {
                transform.Translate(Vector2.down * Time.deltaTime * 3f);
            }        
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("클릭했다");
        isClick = true;         
    }
}
