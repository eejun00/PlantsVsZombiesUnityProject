using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 사용

    
    public bool isGameover = false;   // 게임 오버 확인 변수
    public bool stagePlaying = false; // 스테이지를 진행하고 있는지에 대한 변수

    private int cost = default; // 햇빛 코스트 변수
    public GameObject gameoverUi;
    public TMP_Text costText;

    private void Awake()
    {
        if (instance.IsValid() == false)
        {
            instance = this;
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
