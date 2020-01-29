using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_1 : MeleeEnemy
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

        if (isAlive)
        {
            if (!isStiffness)
            {
                AttackDetection(0.5f);
                AlarmRadiusDetection();
            }
        }
        else
        {
            canMove = false;
            hitable = false;
            DeathComing();
        }
    }


    

    public override void TakeDamage(int damage)
    {
        if(hitable)
        {
            base.TakeDamage(damage);
            //DoStiffness();
            Debug.Log("我受到了攻击");
        }
    }

   


}
