using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeedCard : MonoBehaviour
{
    public GameObject seedPrefab;   // 카드에 들어갈 식물프리팹
    private Image seedImage;        // 카드 안에 들어갈 식물 이미지    
    private TMP_Text seedCost;      // 식물의 코스트 텍스트
    private int cost;               // 식물의 코스트

    private GameObject seedCoolTimeObj; // 카드의 쿨타임 오브젝트 
    private Image seedCoolTimeImg;      // 카드의 쿨타임 오브젝트 이미지 컴포넌트
    private float coolTime;             // 식물 프리팹의 쿨타임 정보
    private float currentTime;          // 쿨타임 관리를 위한 현재시간 변수
    private bool isSeedCoolTime;        // 현재 쿨타임 중인지 확인하기 위한 변수

    private GameObject towerSpawner;    // 식물 스폰을 위한 스포너 오브젝트
    private TowerSpawner towerSpawnerScript;    // 스포너 오브젝트의 스크립트



    private void Awake()
    {
        // 구현에 필요한 컴포넌트와 오브젝트 가져오기
        towerSpawner = GFunc.GetRootObject("TowerSpawner");
        towerSpawnerScript = towerSpawner.GetComponent<TowerSpawner>();
        seedCost = gameObject.FindChildComponent<TMP_Text>("SeedCost");
        seedImage = gameObject.FindChildComponent<Image>("SeedImg");
        seedCoolTimeObj = gameObject.FindChildObject("SeedCoolTime");
        seedCoolTimeImg = seedCoolTimeObj.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(seedPrefab != null)
        {
            // 카드에 담긴 프리팹의 컴포넌트를 가져온 후 저장
            Plant plantscript = seedPrefab.GetComponent<Plant>();
            seedImage.sprite = seedPrefab.GetComponent<SpriteRenderer>().sprite;
            cost = plantscript.cost;
            coolTime = plantscript.coolTime;
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
        if(isSeedCoolTime)
        {
            seedCoolTimeObj.SetActive(true);
            if(currentTime < coolTime)
            {
                currentTime += Time.deltaTime;
                float fillAmount = Mathf.Lerp(1, 0, currentTime / coolTime);
                seedCoolTimeImg.fillAmount = fillAmount;
            }
            else
            {
                isSeedCoolTime = false;
                currentTime = 0.0f;
                seedCoolTimeImg.fillAmount = 1f;
                seedCoolTimeObj.SetActive(false);
            }
        }
    }

    public void OnClickButton()
    {
        
        if (!isSeedCoolTime && GameManager.instance.cost > cost && GameManager.instance.stagePlaying == true)
        {           
            towerSpawnerScript.ReadyToSpawnTower(seedPrefab);
            isSeedCoolTime = true;
        }   // if: 스테이지를 진행중이라면
        else
        {

        }   // else if : 식물 선택화면이라면
    }
}
