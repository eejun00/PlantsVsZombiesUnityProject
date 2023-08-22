using DG.Tweening;
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
    protected float beforeSpeed;
    protected float beforeAniSpeed;
    private float attackAfter = default; //좀비가 공격하고난 후 흐른 시간
    protected Plant plant; // 좀비가 마주친 식물을 받아올 변수
    protected Animator animator;

    public bool isMad = false;
    public bool isFreeze = false;       //빙결에 걸렸는지 확인하기
    public bool isMeetMower = false;
    private bool isSlowed = false;          // 슬로우에 걸렸는지 확인하기
    private Color slowColor = new Color(0.474f, 0.651f, 1f); // 79A6FF, 변경할 색상
    private Color currentColor;


    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;   //현재 체력을 최대 체력과 같게 설정
    }

    private void Start()
    {   
        animator = GetComponent<Animator>();    
        attackAfter = 0f;
    }

    private void Update()
    {
        if (isMeetPlant == true && transform.CompareTag("Zombie"))
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
        else if(transform.CompareTag("Zombie"))
        {
            // 좀비를 왼쪽으로 이동시킴
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            // 좀비가 왼쪽으로 벗어났을 때 제거
            if (transform.position.x < -10.3f)
            {
                Destroy(gameObject);
            }
        }
        else if(transform.CompareTag("MadZombie"))
        {           
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            // 좀비가 왼쪽으로 벗어났을 때 제거
            if (transform.position.x > 7.5f)
            {
                Die();
            }
        }

        if(isMeetMower)
        {
            DownScaleZombie();
            if(transform.localScale.y <= 0f)
            {
                isDie = true;
            }
        }   // if: 예초기와 만났을 경우 

        if(isSlowed == true)
        {
            TakeSlow();
        }

        if (isDie == true)
        {
            Die();
        }
    }

    public void TakeMad()
    {
        if (transform.CompareTag("MadZombie")) { return; }
        isMad = true;
        transform.tag = "MadZombie";
        
        transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        ChangeColorsRecursively(transform, new Color(1f, 0.65f, 1f));
    }

    // 예초기에 닿았을 경우 실행하기 위한 함수
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
        
        if(isSlowed == true)
        {
            currentColor = slowColor;
        }
        else
        {
            currentColor = new Color(1f, 1f, 1f, 1f);
        }

        // 피격 이펙트를 보여주기 위한 색상 변경
        ChangeColorsRecursively(transform, new Color(currentColor.r, currentColor.g, currentColor.b, 0.5f));
        ChangeColorsDoColor(transform);


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

    // 좀비가 슬로우에 걸렸을 경우 실행하는 함수
    public void TakeSlow()
    {
        if (isSlowed == true)
        {
            return;
        }   // if: 이미 슬로우에 걸렸을 경우 리턴
        else
        {
            isSlowed = true;
            ChangeColorsRecursively(transform,slowColor);
            animator.speed = 0.6f;
            moveSpeed = moveSpeed * 0.6f;
            attackSpeed = attackSpeed * 1.2f;
        }
    }

    public void TakeFreeze()
    {
        if(isFreeze == true) { return; }
        isFreeze = true;
        beforeSpeed = moveSpeed;
        beforeAniSpeed = animator.speed;
        moveSpeed = 0f;
        animator.speed = 0f;

        StartCoroutine(Freezing());
    }

    private IEnumerator Freezing()
    {
        yield return new WaitForSeconds(3.0f);
        moveSpeed = beforeSpeed;
        animator.speed = beforeAniSpeed;
        isFreeze = false;
        Debug.Log("프리징 실행됨");
        yield break;
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
                animator.SetTrigger("Attack");
                isMeetPlant = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Plant"))
        {
            animator.SetTrigger("End");
            isMeetPlant = false;
            plant = null;
        }
    }

    // 받아온 컬러로 모든 자식오브젝트의 컬러를 변경한다.
    private void ChangeColorsRecursively(Transform parent,Color color_)
    {
        foreach (Transform child in parent)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color_;
            }

            // 자식 오브젝트에 대해 재귀적으로 실행
            ChangeColorsRecursively(child, color_);
        }
    }

    // 피격 시 알파값 변경을 위해 사용하는 자식오브젝트들 컬러 변경 함수
    private void ChangeColorsDoColor(Transform parent)
    {
        foreach (Transform child in parent)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.DOColor(new Color(currentColor.r, currentColor.g, currentColor.b, 1f), 0.3f);
            }

            // 자식 오브젝트에 대해 재귀적으로 실행
            ChangeColorsDoColor(child);
        }
    }



}
