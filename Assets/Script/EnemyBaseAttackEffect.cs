using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseAttackEffect : MonoBehaviour
{
    private int damage;
    private Timer destoryTimer;
    private Animator animator;
    private Collider2D[] hitObjects;
    private List<Collider2D> hittedObjects;
    //public float rotateAngle;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Animator>() == null)
        {
            destoryTimer = gameObject.AddComponent<Timer>();
            destoryTimer.Duration = 0.1f;

        }
        else
        {
            animator = GetComponent<Animator>();
        }
        hitObjects = new Collider2D[50];
        destoryTimer.Run();
        // transform.RotateAround(Player.MyInstance.transform.position, Vector3.forward, rotateAngle);
        damage = GetComponentInParent<Enemy>().baseATK;

      // StartCoroutine( AttackActive());

    }

    // Update is called once per frame
    void Update()
    {
        if(animator != null)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                HitCheck();
                Destroy(gameObject);
            }
        }
      else if (destoryTimer.Finished)
        {
            HitCheck();
            Destroy(gameObject);
        }
    }

    void HitCheck()
    {
        ContactFilter2D cf = new ContactFilter2D();
        cf.useTriggers = true;
        Physics2D.OverlapCollider(this.GetComponent<Collider2D>(), cf, hitObjects);
        if (hitObjects.Length > 0)
        {
            foreach (Collider2D hit in hitObjects)
            {
                if (hit != null)
                {
                    if (hit.tag == "Player")
                    {
                        hit.GetComponent<Player>().TakeDamage(damage , transform);
                        if(Player.MyInstance.hitable)
                        KnockBack(hit);
                    }
                }

            }

        }
    }

    protected void KnockBack(Collider2D c)
    {
        if (c.tag == "Player")
        {
            c.GetComponent<Player>().KnockBack(transform.position - Player.MyInstance.transform.position, 0.2f);
        }

    }
    private IEnumerator AttackActive()
    {
        yield return new WaitForSeconds(0.3f);
        HitCheck();

    }
}
