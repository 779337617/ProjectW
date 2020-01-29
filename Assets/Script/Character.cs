using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHp;
    public int currentHp;
    //private int max_mp;
    //private int current_mp;
    public int baseATK;// 攻击力
    //public int baseDEF;//防御力
    public int qi; //内力值
    public int qiEffect; //内力值转化护体真气效率 
    private int stableDEF; //护体真气
    public int skillStars; //技能水平参考值
    public GameObject[] attackPositions;


    public float moveSpeed;

    public bool isAlive; //存活标记
    protected bool isDizzy = false;  //眩晕标记
    public bool hitable;  //无敌标记
    protected Animator animator; //
    protected AnimatorOverrideController animatorOverrideController;
    protected AnimatorStateInfo stateInfo;
    protected Rigidbody2D rb2d;

    protected Vector3 randomDir; //随机移动方向
    protected Timer stiffnessTime;   //僵直计时器
    protected Timer dizzyTime; //眩晕计时器
    protected Timer inhitableTimer;   //僵直计时器
    protected Timer randomIdleTimer; //间歇时长计时器
    protected Timer randomIdleCoolDownTimer; //间歇计时器
    protected Timer randomMoveTimer; //随机移动时长计时器
    protected Timer randomMoveCooldownTimer; //随机移动间隔计时器

    protected bool isRandomIdle;//间歇标记
    protected bool isRandomMove;//随机移动标记

    public bool canMove;  //是否可移动标记

    protected bool isStiffness = false;//硬直标记
    public float stiffnessDuration; //硬直时间、
    public float inhitableDuration; //无敌持续时间
    protected bool actionCastTri; //动作是否被打断的标记

    private PolyNavAgent _agent;
    protected PolyNavAgent agent
    {
        get { return _agent != null ? _agent : _agent = GetComponent<PolyNavAgent>(); }
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
       
    }

    protected virtual void Awake()
    {
        isAlive = true;
        hitable = true;

        animator = gameObject.GetComponent<Animator>();
        stiffnessTime = gameObject.AddComponent<Timer>();
        stiffnessTime.Duration = stiffnessDuration;
        dizzyTime = gameObject.AddComponent<Timer>();
        inhitableTimer = gameObject.AddComponent<Timer>();
        randomIdleTimer = gameObject.AddComponent<Timer>();
        randomIdleCoolDownTimer = gameObject.AddComponent<Timer>();
        randomMoveTimer = gameObject.AddComponent<Timer>();
        randomMoveCooldownTimer = gameObject.AddComponent<Timer>(); 

        inhitableTimer.Duration = inhitableDuration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isAlive)
        {
            if (stiffnessTime.Finished)
            {
                UndoStiffness();
            }
            if (inhitableTimer.Finished)
            {
                UnInhitable();
            }
        }
    }

    protected virtual void FixedUpdate()
    {

    }


    public virtual void TakeDamage(int damage)
    {
        if(hitable)
        {
            //health reduce 
            currentHp -= damage;
            if (currentHp <= 0)
            {
                isAlive = false;
                //m_animator.SetBool("death", false);    
            }
            else
            {
                DoStiffness();//造成硬直
                Inhitable();
            }
          
        }
    }
    public virtual void TakeDamage(int damage,Transform t)
    {
        if (hitable)
        {
            //health reduce 
            currentHp -= damage;
            if (currentHp <= 0)
            {
                isAlive = false;
                //m_animator.SetBool("death", false);    
            }
            //DoStiffness();//造成硬直
            KnockBack(t.position - transform.position, 0.1f);
            Inhitable();
        }
    }

    /// <summary>
    /// 造成硬直
    /// </summary>
    public void DoStiffness()
    {
        actionCastTri = false;
        animator.SetBool("Hitted", true);
        stiffnessTime.Run();
        animator.speed = 0;
        canMove = false;
        isStiffness = true;
    }

    public void Inhitable()
    {
        hitable = false;
        inhitableTimer.Run();
    }
    public void UnInhitable()
    {
        hitable = true;
    }

    /// <summary>
    /// 解除硬直
    /// </summary>
    public void UndoStiffness()
    {
        //stiffnessTime.Run();
        animator.speed = 1;
        canMove = true;
        isStiffness = false;
        animator.SetBool("Hitted", false);
    }
    public virtual void Stun(float stunDuration)
    {
        isDizzy = true;
        animator.SetBool("dizzy", true);
        rb2d.velocity = Vector2.zero;
        dizzyTime.Duration = stunDuration;
        dizzyTime.Run();
    }

    public virtual void Sober()
    {
        isDizzy = false;
        animator.SetBool("dizzy", false);
       
    }
    /// <summary>
    /// 角色受击击退
    /// </summary>
    /// <param name="backDir">击退方向</param>
    /// <param name="backFactor">击退距离</param>
    public void KnockBack(Vector2 backDir, float backFactor = 0.3f)
    {
        if (hitable)
        {
            backDir = backDir.normalized * backFactor;
            transform.position = new Vector2(transform.position.x - backDir.x, transform.position.y - backDir.y);
        }
    }
    public float GetAngleBetweenVectors(Vector2 vector1, Vector2 vector2)
    {
        Vector2 difference = vector2 - vector1;
        float rawTargetAngle = Vector2.Angle(Vector2.up, vector2);
        float angle = rawTargetAngle;

        difference *= -1;


        if (difference.x < 0 && difference.y >= 0)
        {
            angle = 360 - rawTargetAngle;
        }
        else if (difference.x < 0 && difference.y <= 0)
        {
            angle = 360 - rawTargetAngle;
        }

        //Debug.Log("Angle: " + angle + " ---- Difference: " + difference + " ---- Raw: " + rawTargetAngle);
        return angle;
    }

    /// <summary>
    /// 开始移动
    /// </summary>
    protected void StartMoving()
    {
        canMove = true;
        animator.SetBool("Move", true);
    }
    /// <summary>
    /// 停止移动
    /// </summary>
    protected void StopMoving()
    {
        agent.Stop();
        canMove = false;
        animator.SetBool("Move", false);

    }

    /// <summary>
    /// 进入间歇
    /// </summary>
    public void EnterIdle()
    {
        StopMoving();
        randomIdleTimer.Duration = Random.Range(0.5f, 2f); //随机此次间歇时长
        randomIdleTimer.Run();
        isRandomIdle = true;
        Debug.Log("开始Idle");

    }
    /// <summary>
    /// 停止间歇
    /// </summary>
    protected void EndIdle()
    {
        isRandomIdle = false;
        StartMoving();
    }
 
}
