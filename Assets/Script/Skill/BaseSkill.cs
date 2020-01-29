using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BaseSkill : MonoBehaviour
{
    public int skillId;
    public SkillType skillType;
    private Timer destoryTimer;
    protected Animator animator;
    protected Collider2D[] hitObjects;
    protected List<Collider2D> hittedObjects;
    protected Timer castingTimer;

    public AnimationClip[] dirPrefabs;


    public int damage;
    public string description;
    [Tooltip("击退距离")]
    public float knockbackFactor;
    [Tooltip("眩晕目标的时间长度")]
    public float stunDuration;
    public float cooldown;
    [Tooltip("持续施法技能，持续施法时间")]
    public float castingDuration;

    // Start is called before the first frame update
   protected virtual void Awake()
    {
        AfterCreate();
        animator = GetComponent<Animator>();
        // destoryTimer = gameObject.AddComponent<Timer>();
        // destoryTimer.Duration = 0.5f;
        hitObjects = new Collider2D[50];
        hittedObjects = new List<Collider2D>();
        // destoryTimer.Run();
    }
    protected virtual void Start()
    {
      
    }

    // Update is called once per frame
    protected virtual void Update()
    {
       
    }
    /// <summary>
    /// 命中检测，碰撞体内是否有敌人
    /// 
    /// </summary>
    protected void HitCheck()
    {
        Collider2D[] p = new Collider2D[500];
        Physics2D.OverlapCollider(this.GetComponent<Collider2D>(), new ContactFilter2D(), p);
        if(p.Length>0)
        {

            foreach (Collider2D op in p)
        {
                if(op != null)
                {
                   // op.gameObject.SetActive(false);

                    if (op.tag == "Enemy")
                    {

                        if (!hittedObjects.Contains(op))
                        {
                            hittedObjects.Add(op);
                            Damage(op);
                            KnockBack(op);
                        }
                    }
                }
            }          
        }
    }

    protected void Damage()
    {
        if (hitObjects.Length > 0)
        {
            foreach (Collider2D hit in hitObjects)
            {
                if (hit != null)
                {
                    //Debug.Log(hit.name);
                    if (hit.tag == "Enemy")
                    {
                        hit.GetComponent<Enemy>().TakeDamage(10);
                        // Debug.Log(hit.name + "受到了攻击");
                    }
                }
            }
        }
    }
    protected void Damage(Collider2D c)
    {
            if (c.tag == "Enemy")
            {
                c.GetComponent<Enemy>().TakeDamage(damage);
            }
    }
    /// <summary>
    /// 击退被击中的目标
    /// </summary>
    protected void KnockBack()
    {
        if (hitObjects.Length > 0)
        {
            foreach (Collider2D hit in hitObjects)
            {
                if (hit != null)
                {
                    //Debug.Log(hit.name);
                    if (hit.tag == "Enemy")
                    {
                        hit.GetComponent<Enemy>().KnockBack(transform.position - hit.transform.position, knockbackFactor);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 击退被击中的目标
    /// </summary>
    protected virtual void KnockBack(Collider2D c)
    {
        if (c.tag == "Enemy")
        {
            c.GetComponent<Enemy>().KnockBack(transform.position - c.transform.position, knockbackFactor);
        }
         
    }

    /// <summary>
    /// 眩晕敌人
    /// </summary>
    protected void Stun()
    {
        if (hitObjects.Length > 0)
        {
            foreach (Collider2D hit in hitObjects)
            {
                if (hit != null)
                {
                    //Debug.Log(hit.name);
                    if (hit.tag == "Enemy")
                    {
                        hit.GetComponent<Enemy>().Stun(stunDuration);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 释放技能
    /// </summary>
    public virtual void Release()
    {

    }

    protected virtual void AfterCreate()
    {
        Player.MyInstance.AbleToSetMousePos(false);
       // Debug.Log("鼠标位置不再改变了");
    }
    
    protected virtual void BeforeDestory()
    {
        Player.MyInstance.AbleToSetMousePos(true);

    }
    
    protected void TryDestory()
    {
        BeforeDestory();
        Destroy(gameObject);
    }
}
