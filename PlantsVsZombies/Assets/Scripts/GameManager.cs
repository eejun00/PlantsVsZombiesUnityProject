using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �̱��� ���

    public int stageOneNum = 1;
    public bool isStageOneEnd = false;
    public bool isGameover = false;   // ���� ���� Ȯ�� ����
    public bool stagePlaying = false; // ���������� �����ϰ� �ִ����� ���� ����
    public bool isStageClear = false; // ���������� Ŭ�����ߴ��� Ȯ�����ִ� ����
    public int zombieDeathCount = default;

    public int cost = default; // �޺� �ڽ�Ʈ ����
    public GameObject uiCanvas;
    public GameObject gameoverUi;
    public TMP_Text costText;
    public GameObject gameClearUi;
    public Image clearImg;
    public float fadeDuration = 2f;

    
    private Color originalColor;
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
        cost = 50; //�ʱ� �ڽ�Ʈ 50����
        originalColor = clearImg.color;
        transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isStageClear && isGameover == false && zombieDeathCount <= 0)
        {
            OnClearUi();
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
        isStageClear = false;
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

        clearImg.color = transparentColor; // ������ ���� ���·� ����
        clearImg.gameObject.SetActive(false);
        clearImg.color = originalColor;

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
        // ������ ���ϴ� ������Ʈ�� ã�ƿͼ� ó���ϴ� �ڵ带 �ۼ�
        uiCanvas = GFunc.GetRootObject("UiCanvas");
        gameoverUi = uiCanvas.FindChildObject("GameOverUi");
        costText = uiCanvas.FindChildComponent<TMP_Text>("CostText");
        gameClearUi = uiCanvas.FindChildObject("GameClearUi");
        clearImg = uiCanvas.FindChildComponent<Image>("EffectImg");

        if(scene.name == "TitleSceneLJY")
        {
            Button stageOneBtn = uiCanvas.FindChildComponent<Button>("Stage1Button");
            TMP_Text stageOnetext = uiCanvas.FindChildComponent<TMP_Text>("Stage1");
            stageOnetext.text = string.Format("{0}", stageOneNum);
            if(isStageOneEnd)
            {
                stageOneBtn.enabled = false;
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
