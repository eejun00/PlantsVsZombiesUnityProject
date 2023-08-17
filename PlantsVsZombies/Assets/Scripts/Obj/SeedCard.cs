using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeedCard : MonoBehaviour
{
    public GameObject seedPrefab;   // ī�忡 �� �Ĺ�������
    private Image seedImage;        // ī�� �ȿ� �� �Ĺ� �̹���    
    private TMP_Text seedCost;      // �Ĺ��� �ڽ�Ʈ �ؽ�Ʈ
    private int cost;               // �Ĺ��� �ڽ�Ʈ

    private GameObject seedCoolTimeObj; // ī���� ��Ÿ�� ������Ʈ 
    private Image seedCoolTimeImg;      // ī���� ��Ÿ�� ������Ʈ �̹��� ������Ʈ
    private float coolTime;             // �Ĺ� �������� ��Ÿ�� ����
    private float currentTime;          // ��Ÿ�� ������ ���� ����ð� ����
    private bool isSeedCoolTime;        // ���� ��Ÿ�� ������ Ȯ���ϱ� ���� ����

    private GameObject towerSpawner;    // �Ĺ� ������ ���� ������ ������Ʈ
    private TowerSpawner towerSpawnerScript;    // ������ ������Ʈ�� ��ũ��Ʈ



    private void Awake()
    {
        // ������ �ʿ��� ������Ʈ�� ������Ʈ ��������
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
            // ī�忡 ��� �������� ������Ʈ�� ������ �� ����
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
        }   // if: ���������� �������̶��
        else
        {

        }   // else if : �Ĺ� ����ȭ���̶��
    }
}
