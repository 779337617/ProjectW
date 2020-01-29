using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSkill_LuanCi : BaseSkill
{
    public float damageCheckCoolDown;
    protected bool damageCheckTrigger;
    protected Timer damageCheckTimer;

    protected override void Awake()
    {
        base.Awake();
        castingTimer = gameObject.AddComponent<Timer>();
        damageCheckTimer = gameObject.AddComponent<Timer>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        castingTimer.Duration = castingDuration;
        castingTimer.Run();
        damageCheckTimer.Duration = damageCheckCoolDown;
        damageCheckTrigger = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(castingTimer.Finished)
        {
            TryDestory();
        }
        if(damageCheckTrigger)
        {
            hittedObjects = new List<Collider2D>();
            damageCheckTimer.Run();
            HitCheck();
            damageCheckTrigger = false;
        }
        if (damageCheckTimer.Finished)
        {
            damageCheckTrigger = true;
        }
    }
}
