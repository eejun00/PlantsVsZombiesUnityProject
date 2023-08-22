using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombies : MonoBehaviour
{
    [SerializeField]
    private float maxHP;                     // �ִ� ü��
    public float currentHP;                 // ���� ü��
    private bool isDie = false;              // ���� ��� �����̸� isDie�� true�� ����   
    public float zombieAD = 1f;             // ������ ���� ������ ��
    private bool isMeetPlant = false;
    public float moveSpeed = 2f; // ���� �̵� �ӵ�
    public float attackSpeed = 1f; // ���� ���� �ӵ�
    protected float beforeSpeed;
    protected float beforeAniSpeed;
    private float attackAfter = default; //���� �����ϰ� �� �帥 �ð�
    protected Plant plant; // ���� ����ģ �Ĺ��� �޾ƿ� ����
    protected Animator animator;

    public bool isMad = false;
    public bool isFreeze = false;       //���ῡ �ɷȴ��� Ȯ���ϱ�
    public bool isMeetMower = false;
    private bool isSlowed = false;          // ���ο쿡 �ɷȴ��� Ȯ���ϱ�
    private Color slowColor = new Color(0.474f, 0.651f, 1f); // 79A6FF, ������ ����
    private Color currentColor;


    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;   //���� ü���� �ִ� ü�°� ���� ����
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
                    plant.TakeDamage(zombieAD); // ������ ������
                    attackAfter = 0f;
                }
            }
        }
        else if(transform.CompareTag("Zombie"))
        {
            // ���� �������� �̵���Ŵ
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            // ���� �������� ����� �� ����
            if (transform.position.x < -10.3f)
            {
                Destroy(gameObject);
            }
        }
        else if(transform.CompareTag("MadZombie"))
        {           
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            // ���� �������� ����� �� ����
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
        }   // if: ���ʱ�� ������ ��� 

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

    // ���ʱ⿡ ����� ��� �����ϱ� ���� �Լ�
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
            newScale.y = Mathf.Max(newScale.y, 0f); // �������� ������ ���� �ʵ��� ����
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
        // Tip. ���� ü���� damage ��ŭ �����ؼ� ���� ��Ȳ�� �� ���� Ÿ���� ������ ���ÿ� ������
        // enemy.OnDie() �Լ��� ���� �� ����� �� �ִ�.

        // ���� ���� ���°� ��� �����̸� �Ʒ� �ڵ带 �������� �ʴ´�.
        if (isDie == true) return;
        
        if(isSlowed == true)
        {
            currentColor = slowColor;
        }
        else
        {
            currentColor = new Color(1f, 1f, 1f, 1f);
        }

        // �ǰ� ����Ʈ�� �����ֱ� ���� ���� ����
        ChangeColorsRecursively(transform, new Color(currentColor.r, currentColor.g, currentColor.b, 0.5f));
        ChangeColorsDoColor(transform);


        // ���� ü���� damage ��ŭ ���� 
        currentHP -= damage;

        // ü���� 0���� = �� ĳ���� ���
        if (currentHP <= 0)
        {
            isDie = true;
            // �� ĳ���� ���
            //enemy.OnDie(EnemyDestroyType.Kill);
        }
    }

    // ���� ���ο쿡 �ɷ��� ��� �����ϴ� �Լ�
    public void TakeSlow()
    {
        if (isSlowed == true)
        {
            return;
        }   // if: �̹� ���ο쿡 �ɷ��� ��� ����
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
        Debug.Log("����¡ �����");
        yield break;
    }

    public void Die()
    {
        // ���� ����� ���� ó��, ���� ��� ��� �ִϸ��̼� ��� ���� ������ �� ����
        Destroy(gameObject); // �ϴ� ������Ʈ �ı��� ó��
        GameManager.instance.zombieDeathCount -= 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Plant"))
        {
            plant = other.GetComponent<Plant>(); // �浹�� ������Ʈ�� Plant ������Ʈ ��������
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

    // �޾ƿ� �÷��� ��� �ڽĿ�����Ʈ�� �÷��� �����Ѵ�.
    private void ChangeColorsRecursively(Transform parent,Color color_)
    {
        foreach (Transform child in parent)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color_;
            }

            // �ڽ� ������Ʈ�� ���� ��������� ����
            ChangeColorsRecursively(child, color_);
        }
    }

    // �ǰ� �� ���İ� ������ ���� ����ϴ� �ڽĿ�����Ʈ�� �÷� ���� �Լ�
    private void ChangeColorsDoColor(Transform parent)
    {
        foreach (Transform child in parent)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.DOColor(new Color(currentColor.r, currentColor.g, currentColor.b, 1f), 0.3f);
            }

            // �ڽ� ������Ʈ�� ���� ��������� ����
            ChangeColorsDoColor(child);
        }
    }



}
