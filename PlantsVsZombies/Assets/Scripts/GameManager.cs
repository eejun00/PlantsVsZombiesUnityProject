using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �̱��� ���

    public bool isGameover = false;   // ���� ���� Ȯ�� ����
    public bool stagePlaying = false; // ���������� �����ϰ� �ִ����� ���� ����
    public GameObject gameoverUi;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
