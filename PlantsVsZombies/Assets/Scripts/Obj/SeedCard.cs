using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeedCard : MonoBehaviour
{
    public GameObject seedPrefab;
    private TMP_Text seedCost;
    private Image seedImage;
    private GameObject towerSpawner;
    private TowerSpawner towerSpawnerScript;

    private void Awake()
    {
        towerSpawner = GFunc.GetRootObject("TowerSpawner");
        towerSpawnerScript = towerSpawner.GetComponent<TowerSpawner>();
        seedCost = gameObject.FindChildComponent<TMP_Text>("SeedCost");
        seedImage = gameObject.FindChildComponent<Image>("SeedImg");
    }

    // Start is called before the first frame update
    void Start()
    {
        if(seedPrefab != null)
        {
            Plant plantscript = seedPrefab.GetComponent<Plant>();
            seedImage.sprite = seedPrefab.GetComponent<SpriteRenderer>().sprite;
            int cost = plantscript.cost;
            seedCost.text = string.Format("{0}", cost);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickButton()
    {
        towerSpawnerScript.ReadyToSpawnTower(seedPrefab);
        if (GameManager.instance.stagePlaying == false)
        {

        }   // if: 스테이지를 진행중이라면
        else
        {

        }   // else: 스테이지를 진행중이 아니라면(식물 선택화면이라면)
    }
}
