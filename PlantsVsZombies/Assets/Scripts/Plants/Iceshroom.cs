using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Iceshroom : Plant
{
    private bool isPlanted = false;
    public GameObject effectObj;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        isPlanted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
        {
            if (transform.parent.CompareTag("Tile") && isPlanted == false)
            {
                isPlanted = true;
                BoomIceshroom();               
            }
        }
        if (transform.localScale.x >= 2.1f)
        {
            effectObj.SetActive(true);
            effectObj.GetComponent<Image>().DOColor(Color.clear, 0.5f);
            FreezeZombie();
        }
    }

    private void BoomIceshroom()
    {
        transform.DOScale(2.1f, 1f);
        Destroy(gameObject, 1.51f);
    }

    private void FreezeZombie()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");

        // 찾은 좀비 오브젝트들의 변수 변경
        foreach (GameObject zombie in zombies)
        {
            Zombies zombieScript = zombie.GetComponent<Zombies>();
            if (zombieScript != null && zombieScript.isFreeze == false)
            {
                // 좀비 오브젝트의 스크립트에서 변수 변경
                zombieScript.TakeSlow();
                zombieScript.TakeFreeze();
            }
        }
    }
}
