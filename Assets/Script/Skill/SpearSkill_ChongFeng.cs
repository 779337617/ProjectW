using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 冲锋技能
/// </summary>
public class SpearSkill_ChongFeng : BaseSkill
{
    public float chargeDuration;
    private Timer chargeTimer;
    public float rushDistance;
    public bool isDetectHit = false;
    public int PhaseNumber;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
   
    }

    protected override void Awake()
    {
        base.Awake();
        chargeTimer = gameObject.AddComponent<Timer>();
       // Debug.Log("??");
        chargeTimer.Duration = chargeDuration;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (chargeTimer.Finished)
        {
            if(PhaseNumber!=2)
            Phase_2();
        }
        if(isDetectHit)
        {
            HitCheck();
        }
    }

    public override void Release()
    {
        base.Release();
        Phase_1();
    }

    public void Phase_1()
    {
        PhaseNumber = 1;
        chargeTimer.Run();
    }

    public void Phase_2()
    {
        PhaseNumber = 2;
        isDetectHit = true;
        animator.SetTrigger("Phase_2");
        Player.MyInstance.StartLineDrive(rushDistance, 0.2f);
        Invoke("TryDestory",0.2f);
    }
}
