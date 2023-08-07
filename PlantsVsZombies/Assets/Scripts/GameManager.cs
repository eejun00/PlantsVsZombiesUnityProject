using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �̱��� ���

    
    public bool isGameover = false;   // ���� ���� Ȯ�� ����
    public bool stagePlaying = false; // ���������� �����ϰ� �ִ����� ���� ����

    private int cost = default; // �޺� �ڽ�Ʈ ����
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
