using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void BaseAttack(float t)
    {
        canMove = false;
        animator.SetBool("Move", false);
        animator.SetBool("Attack", true);
        // EnterIdle();
        actionCastTri = true;
        StartCoroutine(TriggerAttack(t));


    }

    /// <summary>
    /// 触发攻击(生成攻击碰撞检测盒子等）
    /// </summary>
    protected IEnumerator TriggerAttack(float t)
    {
        yield return new WaitForSeconds(t);
        Debug.Log(actionCastTri);
        if(actionCastTri)
        {
            GameObject p = ChooseHitBox();
            GameObject a = Instantiate(attacksPrefabs[0], p.transform.position, transform.rotation, transform);
            a.SetActive(true);
        }
      
    }

    /// <summary>
    /// 基础攻击范围检测
    /// </summary>
    /// <param name="t"></param>
    public void AttackDetection(float t)
    {
        if (playerDistance < attackDetectionRadius)
        {
            StopMoving();
            // StartCoroutine(AttackDelay(0.2f));
            if (attackCooldownTimer.Finished)
            {
                BaseAttack(t);
                attackCooldownTimer.Run();
            }
            else
            {
                animator.SetBool("Attack", false);
            }
        }
        else
        {
            canMove = true;
        }
    }
    /// <summary>
    /// 指定距离范围检测模版
    /// </summary>
    /// <param name="t"></param>
    public void AttackDetection(float t, float distance)
    {
        if (playerDistance < distance)
        {
            StopMoving();
            // StartCoroutine(AttackDelay(0.2f));
            if (attackCooldownTimer.Finished)
            {
                //BaseAttack();
                attackCooldownTimer.Run();
            }
            else
            {
                animator.SetBool("Attack", false);
            }
        }
        else
        {
            canMove = true;
        }
    }
}
