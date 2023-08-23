using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 사용

    public int stageOneNum = 1;
    public bool isStageOneEnd = false;
    public bool isGameover = false;   // 게임 오버 확인 변수
    public bool stagePlaying = false; // 스테이지를 진행하고 있는지에 대한 변수
    public bool isStageClear = false; // 스테이지를 클리어했는지 확인해주는 변수
    public bool isWave = false;       // 웨이브가 시작됬는지 확인하는 변수
    public bool isSelectSeed = false; // 식물 선택화면인지 확인하는 변수
    public int zombieDeathCount = default;

    public int cost = default; // 햇빛 코스트 변수
    public GameObject uiCanvas;
    public GameObject gameoverUi;
    public TMP_Text costText;
    public GameObject gameClearUi;
    public Image clearImg;
    public float fadeDuration = 2f;


    private Color originalColor = default;
    private Color transparentColor;

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
        uiCanvas = GFunc.GetRootObject("UiCanvas");
        gameoverUi = uiCanvas.FindChildObject("GameOverUi");
        costText = uiCanvas.FindChildComponent<TMP_Text>("CostText");
        gameClearUi = uiCanvas.FindChildObject("GameClearUi");
        clearImg = uiCanvas.FindChildComponent<Image>("EffectImg");
        if (stageOneNum > 0 && costText != null) { AddCost(100); } //초기 코스트 100설정        

        if (stageOneNum >= 7)
        {
            isSelectSeed = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameClearUi != null)
        {
            if (isStageClear && isGameover == false && zombieDeathCount <= 0)
            {
                OnClearUi();
                isStageClear = false;
                stagePlaying = false;
            }
        }
    }

    public void PassStage()
    {
        stageOneNum += 1;
        cost = 50;
    }

    public void OnClearUi()
    {
        gameClearUi.SetActive(true);
        clearImg.DOColor(Color.clear, 2.0f);

    }

    public void LetsRock()
    {
        uiCanvas.FindChildObject("SeedChooser").SetActive(false);
        //메인카메라 움직이기
        Camera.main.transform.DOMoveX(-1.5f, 1.5f);
        GFunc.GetRootObject("AfterStartObj").SetActive(true);
        uiCanvas.FindChildObject("AfterStartUi").SetActive(true);
        isSelectSeed = false;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mode == LoadSceneMode.Single)
        {
            FindAndProcessObjectsInScene(scene);
        }
    }

    private void FindAndProcessObjectsInScene(Scene scene)
    {

        if (scene.name == "TitleSceneLJY")
        {
            uiCanvas = GFunc.GetRootObject("UiCanvas");
            Button stageOneBtn = uiCanvas.FindChildComponent<Button>("Stage1Button");
            TMP_Text stageOnetext = uiCanvas.FindChildComponent<TMP_Text>("Stage1");

            if (isStageOneEnd || stageOneNum > 5)
            {
                isStageOneEnd = true;
                stageOneBtn.enabled = false;
                Image btnImg = stageOneBtn.GetComponent<Image>();
                btnImg.DOColor(new Color(0.4f, 0.4f, 0.4f), 2f);
                stageOnetext.text = string.Format("5");
            }
            else
            {
                stageOnetext.text = string.Format("{0}", stageOneNum);
            }
        }
        else if (scene.name == "EndingScene")
        {

        }
        else if (scene.name == "StageSurvive")
        {
            // 씬에서 원하는 오브젝트를 찾아와서 처리하는 코드를 작성
            uiCanvas = GFunc.GetRootObject("UiCanvas");
            gameoverUi = uiCanvas.FindChildObject("GameOverUi");
            costText = uiCanvas.FindChildComponent<TMP_Text>("CostText");
            gameClearUi = uiCanvas.FindChildObject("GameClearUi");
            clearImg = uiCanvas.FindChildComponent<Image>("EffectImg");
            cost = 0;
            AddCost(100);
            if (originalColor == default)
            {
                originalColor = clearImg.color;
                Debug.Log(clearImg.color);
            }
            else
            {
                clearImg.color = originalColor;
            }
            transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
            isSelectSeed = true;
        }
        else
        {
            // 씬에서 원하는 오브젝트를 찾아와서 처리하는 코드를 작성
            uiCanvas = GFunc.GetRootObject("UiCanvas");
            gameoverUi = uiCanvas.FindChildObject("GameOverUi");
            costText = uiCanvas.FindChildComponent<TMP_Text>("CostText");
            gameClearUi = uiCanvas.FindChildObject("GameClearUi");
            clearImg = uiCanvas.FindChildComponent<Image>("EffectImg");
            cost = 0;
            AddCost(100);
            if (originalColor == default)
            {
                originalColor = clearImg.color;
                Debug.Log(clearImg.color);
            }
            else
            {
                clearImg.color = originalColor;
            }
            transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

            if (stageOneNum >= 7)
            {
                // 식물 선택화면이 존재하는 스테이지부터 오브젝트들 꺼놓기
                //GFunc.GetRootObject("AfeterStartObj").SetActive(false);
                //uiCanvas.FindChildObject("AfterStartUi").SetActive(false);
                isSelectSeed = true;
            }
        }
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
