using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombies : MonoBehaviour
{
    [SerializeField]
    private float maxHP;                     // 최대 체력
    public float currentHP;                 // 현재 체력
    private bool isDie = false;              // 적이 사망 상태이면 isDie를 true로 설정   
    public float zombieAD = 1f;             // 좀비의 공격 데미지 양
    private bool isMeetPlant = false;
    public float moveSpeed = 2f; // 좀비 이동 속도
    public float attackSpeed = 1f; // 좀비 공격 속도
    private float attackAfter = default; //좀비가 공격하고난 후 흐른 시간
    private Plant plant; // 좀비가 마주친 식물을 받아올 변수

    public bool isMeetMower = false;
    private bool isSlowed = false;          // 슬로우에 걸렸는지 확인하기
    private Color newColor = new Color(0.474f, 0.651f, 1f); // 79A6FF, 변경할 색상

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;   //현재 체력을 최대 체력과 같게 설정
    }

    private void Start()
    {        
        attackAfter = 0f;
    }

    private void Update()
    {
        if (isMeetPlant == true)
        {
            if (plant != null)
            {
                attackAfter += Time.deltaTime;
                if (attackAfter > attackSpeed)
                {
                    plant.TakeDamage(zombieAD); // 데미지 입히기
                    attackAfter = 0f;
                }
            }
        }
        else
        {
            // 좀비를 왼쪽으로 이동시킴
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            // 좀비가 왼쪽으로 벗어났을 때 제거
            if (transform.position.x < -10.3f)
            {
                Destroy(gameObject);
            }
        }

        if(isMeetMower)
        {
            DownScaleZombie();
            if(transform.localScale.y <= 0f)
            {
                isDie = true;
            }
        }

        if (isDie == true)
        {
            Die();
        }
    }

    private void DownScaleZombie()
    {
        StartCoroutine(ScaleDownAndDestroy());
    }

    IEnumerator ScaleDownAndDestroy()
    {
        while (transform.localScale.y > 0)
        {
            Vector3 newScale = transform.localScale;
            newScale.y -= 0.05f * Time.deltaTime;
            newScale.y = Mathf.Max(newScale.y, 0f); // 스케일이 음수가 되지 않도록 보정
            transform.localScale = newScale;

            yield return null;
        }

        if(transform.localScale.y <= 0f)
        {
            yield break;
        }
    }

    public void TakeDamage(float damage)
    {
        // Tip. 적의 체력이 damage 만큼 감소해서 죽을 상황일 때 여러 타워의 공격을 동시에 받으면
        // enemy.OnDie() 함수가 여러 번 실행될 수 있다.

        // 현재 적의 상태가 사망 상태이면 아래 코드를 실행하지 않는다.
        if (isDie == true) return;

        // 현재 체력을 damage 만큼 감소 
        currentHP -= damage;

        // 체력이 0이하 = 적 캐릭터 사망
        if (currentHP <= 0)
        {
            isDie = true;
            // 적 캐릭터 사망
            //enemy.OnDie(EnemyDestroyType.Kill);
        }
    }

    public void TakeSlow()
    {
        if (isSlowed == true)
        {
            return;
        }
        else
        {
            isSlowed = true;
            ChangeColorsRecursively(transform);
            moveSpeed = moveSpeed * 0.6f;
            attackSpeed = attackSpeed * 1.2f;
        }
    }
    public void Die()
    {
        // 좀비가 사망할 때의 처리, 예를 들어 사망 애니메이션 재생 등을 수행할 수 있음
        Destroy(gameObject); // 일단 오브젝트 파괴로 처리
        GameManager.instance.zombieDeathCount -= 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Plant"))
        {
            plant = other.GetComponent<Plant>(); // 충돌한 오브젝트의 Plant 컴포넌트 가져오기
            if (plant != null)
            {
                isMeetPlant = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Plant"))
        {
            isMeetPlant = false;
            plant = null;
        }
    }


    private void ChangeColorsRecursively(Transform parent)
    {
        foreach (Transform child in parent)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = newColor;
            }

            // 자식 오브젝트에 대해 재귀적으로 실행
            ChangeColorsRecursively(child);
        }
    }

}
