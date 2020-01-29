using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttackEffect : MonoBehaviour
{
    private Timer destoryTimer;
    private Animator animator;
    private Collider2D[] hitObjects;
    private List<Collider2D> hittedObjects;
    public float rotateAngle;
    public float knockBackFactor;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        destoryTimer = gameObject.AddComponent<Timer>();
        hitObjects = new Collider2D[50];

        destoryTimer.Duration = 0.5f;
        destoryTimer.Run();
        transform.RotateAround(Player.MyInstance.transform.position, Vector3.forward, rotateAngle);
        HitCheck();

    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime> 1.0f )
        {
            Destroy(gameObject);
        }
    }

    void HitCheck()
    {
        ContactFilter2D c = new ContactFilter2D();
        c.useTriggers = true;
        Physics2D.OverlapCollider(this.GetComponent<PolygonCollider2D>(), c, hitObjects);
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
                        KnockBack(hit);
                       // Debug.Log(hit.name + "受到了攻击");
                    }
                }

            }

        }
    }

    protected void KnockBack(Collider2D c)
    {
        if (c.tag == "Enemy")
        {
            c.GetComponent<Enemy>().KnockBack(Player.MyInstance.transform.position - c.transform.position, knockBackFactor);
        }

    }
}
