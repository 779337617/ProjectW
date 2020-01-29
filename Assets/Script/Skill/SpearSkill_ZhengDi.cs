using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSkill_ZhengDi : BaseSkill
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        castingTimer = gameObject.AddComponent<Timer>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        castingTimer.Duration = castingDuration;
        castingTimer.Run();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(castingTimer.Finished)
        {
            TryDestory();
        }
    }

    public override void Release()
    {
        base.Release();
        HitCheck();
    }

    protected override void KnockBack(Collider2D c)
    {
        if (c.tag == "Enemy")
        {
            c.GetComponent<Enemy>().KnockBack(Player.MyInstance.transform.position - c.transform.position, knockbackFactor);
        }
    }
}
