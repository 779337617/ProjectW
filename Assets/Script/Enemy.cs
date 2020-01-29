using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected Timer attackCooldownTimer; //攻击间隔计时器

    protected Vector2 playerCharacterPos;
    protected float playerDistance;
    public GameObject[] attacksPrefabs;

    public float alarmRadius;  //预警半径
    public float attackCooldown; //攻击间隔
    private int type;
    [SerializeField]
    private int strengthValue;

    private float randomSpeed;

    /// <summary>
    /// 怪物强度系数，用于关卡生成计算
    /// </summary>
    public int StrengthValue { get => strengthValue; set => strengthValue = value; }
    /// <summary>
    /// 怪物类型1-普通怪物,2-群居怪物,3-精英怪物
    /// </summary>
    [ShowInInspector, Wrap(0,4), PropertyTooltip("怪物类型1-普通怪物,2-群居怪物,3-精英怪物")]
    public int Type { get => type; set => type = value; }

    private Vector2 moveDirection;

    public float attackDetectionRadius;//攻击检测半径
    // Start is called before the first frame update
    protected override void Start()
    {
        playerCharacterPos = Player.MyInstance.transform.position;
        playerDistance = (playerCharacterPos - new Vector2(transform.position.x,transform.position.y)).magnitude;
        attackCooldownTimer = gameObject.AddComponent<Timer>();
        attackCooldownTimer.Duration = attackCooldown;
        attackCooldownTimer.Run();
        randomMoveCooldownTimer.Duration = 1f;
        randomMoveCooldownTimer.Run();
        randomMoveTimer.Duration = 1f;
        randomMoveTimer.Run();

        canMove = true;
        isRandomMove = true;
        animator.SetBool("Move", true);
        randomIdleCoolDownTimer.Duration = Random.Range(2, 5);
        randomIdleCoolDownTimer.Run();
     

        isRandomIdle = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        playerCharacterPos = Player.MyInstance.transform.position;
        playerDistance = (playerCharacterPos - new Vector2(transform.position.x, transform.position.y)).magnitude;
       // Debug.Log(playerDistance+","+attackDetectionRadius);
        stateInfo = this.animator.GetCurrentAnimatorStateInfo(0);

            
            //if (!stateInfo.IsName("BaseAttack"))
            if(stateInfo.IsName("Run"))
            {
                Turn();
            }
            if (canMove && !isRandomIdle)
            {
                RandomMoveTime();
                if (isRandomMove)
            {
                RandomMove();
            }
            else
            {
                agent.maxSpeed = moveSpeed;
                MoveToPlayer();
            }
                
            }
            else
            {
                agent.maxSpeed = 0;
            }

            if (randomIdleTimer.Finished)
            {
                EndIdle();
                
            }

            RandomIdle();


    }


    public override void TakeDamage(int damage)
    {
        //health reduce 
        currentHp -= damage;
        if (currentHp <= 0)
        {
            isAlive = false;
            Debug.Log(isAlive);
            //
        }
        else
        {
            DoStiffness();//造成硬直

        }
    }

    /// <summary>
    /// 朝着玩家角色移动
    /// </summary>
    protected void MoveToPlayer()
    {
        animator.SetBool("Move", true);
        agent.SetDestination(playerCharacterPos);
        Turn();

    }


    protected virtual void BaseAttack(float t)
    {
        //Debug.Log("attack");
        animator.SetBool("Attack",true);
        
    }



    /// <summary>
    /// 让怪物AI避免完全一致导致的重叠,判断RandomIdle
    /// </summary>
    public void RandomIdle()
    {
        if (randomIdleCoolDownTimer.Finished)
        {
            randomIdleCoolDownTimer.Duration = Random.Range(3, 6f); //随机下次判断间歇时间
            randomIdleCoolDownTimer.Run();
            if(Random.Range(0,1f) < 0.5f)
            {
                EnterIdle();
            }
        }

        
       
    }

   

    /// <summary>
    /// 选择攻击方向
    /// </summary>
    /// <returns></returns>
    public GameObject ChooseHitBox()
    {
        GameObject g = attackPositions[0];
        float attackAngle = GetAngleBetweenVectors(new Vector2(0, 1), Player.MyInstance.transform.position);
        if (337.5 >= attackAngle || attackAngle < 22.5)
        {
            g = attackPositions[0];
        }
        if (292.5 <= attackAngle && attackAngle < 337.5)
        {
            g = attackPositions[1];
        }
        if (247.5 <= attackAngle && attackAngle < 292.5)
        {
            g = attackPositions[2];
        }
        if (202.5 <= attackAngle && attackAngle < 247.5)
        {
            g = attackPositions[3];
        }
        if (157.5 <= attackAngle && attackAngle < 202.5)
        {
            g = attackPositions[4];
        }
        if (112.5 <= attackAngle && attackAngle < 157.5)
        {
            g = attackPositions[5];
        }
        if (67.5 <= attackAngle && attackAngle < 112.5)
        {
            g = attackPositions[6];
        }
        if (22.5 <= attackAngle && attackAngle < 67.5)
        {
            g = attackPositions[7];
        }
        return g;
    }


    /// <summary>
    /// 进入死亡阶段
    /// </summary>
    public void DeathComing()
    {
        animator.SetBool("Death", true);
        //UndoStiffness();
        if (stateInfo.IsName("Death") && stateInfo.normalizedTime >= 1.0f)
        {
            Destroy(gameObject);
        }

    }
    
    /// <summary>
    /// 预警半径检测
    /// 玩家处在预警半径中，敌人不会进入Idle状态
    /// </summary>
    public void AlarmRadiusDetection()
    {
        if(playerDistance < alarmRadius)
        {
            Debug.Log("进入预警半径");
            EndIdle();
            Turn();
        }

    }


    /// <summary>
    /// 控制动画转向
    /// </summary>
    protected void Turn()
    {
        animator.SetFloat("DirectionX", playerCharacterPos.x - transform.position.x);
        animator.SetFloat("DirectionY", playerCharacterPos.y - transform.position.y);
    }
    /// <summary>
    /// 向指定方向转向
    /// </summary>
    /// <param name="v"></param>
    protected void Turn(Vector3 v)
    {
        animator.SetFloat("DirectionX", v.x);
        animator.SetFloat("DirectionY", v.y);
    }
    /// <summary>
    /// 随机方向移动
    /// </summary>
    protected void RandomMove()
    {       
        
        gameObject.transform.position = gameObject.transform.position + randomDir * Time.deltaTime * randomSpeed;
        Turn();
        Debug.Log("正在随机移动");


            

       
    }
    
    /// <summary>
    /// 随机移动时间判定
    /// </summary>
    protected void RandomMoveTime()
    {
        if(randomMoveTimer.Finished)
        {
            isRandomMove = false;
            if(randomMoveCooldownTimer.Finished)
            {
                randomMoveCooldownTimer.Duration = Random.Range(3, 6f);
                randomMoveCooldownTimer.Run();
                if(Random.Range(0,1f) <0.5f)
                {
                    StopMoving();
                    randomDir.x = Random.Range(-1f, 1f);
                    randomDir.y = Random.Range(-1f, 1f);
                    randomSpeed = Random.Range(0.1f, moveSpeed);
                    isRandomMove = true;
                    randomMoveTimer.Duration = Random.Range(0.1f, 1f);
                    randomMoveTimer.Run();
                }


            }
        }

    }
}
