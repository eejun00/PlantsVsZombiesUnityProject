using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �̱��� ���

    public int stageOneNum = 1;
    public bool isStageOneEnd = false;
    public bool isGameover = false;   // ���� ���� Ȯ�� ����
    public bool stagePlaying = false; // ���������� �����ϰ� �ִ����� ���� ����
    public bool isStageClear = false; // ���������� Ŭ�����ߴ��� Ȯ�����ִ� ����
    public bool isWave = false;       // ���̺갡 ���ۉ���� Ȯ���ϴ� ����
    public bool isSelectSeed = false; // �Ĺ� ����ȭ������ Ȯ���ϴ� ����
    public int zombieDeathCount = default;

    public int cost = default; // �޺� �ڽ�Ʈ ����
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
            GFunc.LogWarning("���� �� �� �̻��� ���� �Ŵ����� �����մϴ�!");
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
        if (stageOneNum > 0 && costText != null) { AddCost(100); } //�ʱ� �ڽ�Ʈ 100����        

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
        //����ī�޶� �����̱�
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
            // ������ ���ϴ� ������Ʈ�� ã�ƿͼ� ó���ϴ� �ڵ带 �ۼ�
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
            // ������ ���ϴ� ������Ʈ�� ã�ƿͼ� ó���ϴ� �ڵ带 �ۼ�
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
                // �Ĺ� ����ȭ���� �����ϴ� ������������ ������Ʈ�� ������
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
