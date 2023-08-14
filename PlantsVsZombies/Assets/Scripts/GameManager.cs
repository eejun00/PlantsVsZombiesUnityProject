using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 사용

    public int stageOneNum = 1;
    public bool isStageOneEnd = false;
    public bool isGameover = false;   // 게임 오버 확인 변수
    public bool stagePlaying = false; // 스테이지를 진행하고 있는지에 대한 변수
    public bool isStageClear = false; // 스테이지를 클리어했는지 확인해주는 변수
    public bool isWave = false;       // 웨이브가 시작됬는지 확인하는 변수
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
        AddCost(100); //초기 코스트 100설정        
    }

    // Update is called once per frame
    void Update()
    {
        if(isStageClear && isGameover == false && zombieDeathCount <= 0)
        {
            OnClearUi();
            isStageClear = false;
            stagePlaying = false;
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
        StartCoroutine(FadeOutUI());
    }

    private IEnumerator FadeOutUI()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            clearImg.color = Color.Lerp(originalColor, transparentColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        clearImg.color = transparentColor; // 완전한 투명 상태로 설정
        clearImg.gameObject.SetActive(false);

        yield return null;
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
            stageOnetext.text = string.Format("{0}", stageOneNum);
            if (isStageOneEnd)
            {
                stageOneBtn.enabled = false;
            }
        }
        else if (scene.name == "EndingScene")
        {

        }
        else
        {
            // 씬에서 원하는 오브젝트를 찾아와서 처리하는 코드를 작성
            uiCanvas = GFunc.GetRootObject("UiCanvas");
            gameoverUi = uiCanvas.FindChildObject("GameOverUi");
            costText = uiCanvas.FindChildComponent<TMP_Text>("CostText");
            gameClearUi = uiCanvas.FindChildObject("GameClearUi");
            clearImg = uiCanvas.FindChildComponent<Image>("EffectImg");
            if(originalColor == default)
            {
                originalColor = clearImg.color;
                Debug.Log(clearImg.color);
            }
            else
            {
                clearImg.color = originalColor;
            }
            transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
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
