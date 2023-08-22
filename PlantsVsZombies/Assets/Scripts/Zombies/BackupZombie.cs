using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackupZombie : Zombies
{
    private bool isStandFlag = false; // 한번만 실행되게 하기
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(animator.GetBool("Standing") && !isStandFlag)
        {
            isStandFlag = true;
            beforeSpeed = moveSpeed;
            moveSpeed = 0f;
        }
        else if(!animator.GetBool("Standing") && isStandFlag)
        {
            isStandFlag = false;
            moveSpeed = beforeSpeed;
        }
    }

    public void PlayUpSunGlass()
    {
        beforeSpeed = moveSpeed;
        moveSpeed = 0f;
        animator.SetTrigger("UpSunGlass");
    }

    public void PlayEndSunGlass()
    {
        moveSpeed = beforeSpeed;
        animator.SetTrigger("EndSunGlass");
    }
}
