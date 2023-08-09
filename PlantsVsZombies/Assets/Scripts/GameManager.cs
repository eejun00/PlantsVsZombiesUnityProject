using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 사용

    
    public bool isGameover = false;   // 게임 오버 확인 변수
    public bool stagePlaying = false; // 스테이지를 진행하고 있는지에 대한 변수
    public bool isStageClear = false; // 스테이지를 클리어했는지 확인해주는 변수
    public int zombieDeathCount = default;

    public int cost = default; // 햇빛 코스트 변수
    public GameObject gameoverUi;
    public TMP_Text costText;
    public GameObject gameClearUi;

    private void Awake()
    {
        if (instance.IsValid() == false)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            GFunc.LogWarning("씬에 두 개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cost = 50; //초기 코스트 50설정
    }

    // Update is called once per frame
    void Update()
    {
        if(isStageClear && isGameover == false && zombieDeathCount <= 0)
        {
            OnClearUi();
        }
    }

    public void OnClearUi()
    {
        gameClearUi.SetActive(true);
    }

    public void AddCost(int cost_)
    {
        cost += cost_;
        costText.text = string.Format("{0}", cost);
    }

    public void EndGame()
    {
        isGameover = true;
        gameoverUi.SetActive(true);
    }
}
