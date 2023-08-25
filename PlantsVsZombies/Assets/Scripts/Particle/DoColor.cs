using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoColor : MonoBehaviour
{
    public Image background;
    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        StartCoroutine(DoColorA());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator DoColorA()
    {
        for(int i = 0; i < 500;  i++)
        {
            background.DOColor(new Color(background.color.r, background.color.g, background.color.b, 0f), 3f);
            yield return new WaitForSeconds(3f);
            background.DOColor(new Color(background.color.r, background.color.g, background.color.b, 1f), 3f);
            yield return new WaitForSeconds(3f);
        }
    }
}
