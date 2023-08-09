using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �̱��� ���

    
    public bool isGameover = false;   // ���� ���� Ȯ�� ����
    public bool stagePlaying = false; // ���������� �����ϰ� �ִ����� ���� ����
    public bool isStageClear = false; // ���������� Ŭ�����ߴ��� Ȯ�����ִ� ����
    public int zombieDeathCount = default;

    public int cost = default; // �޺� �ڽ�Ʈ ����
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
            GFunc.LogWarning("���� �� �� �̻��� ���� �Ŵ����� �����մϴ�!");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cost = 50; //�ʱ� �ڽ�Ʈ 50����
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
