using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  技能模版类
///  保存技能对象中不变的属性（基本属性）
///  在游戏中不会发生太大的变化
/// </summary>
public class SkillModel
{   //技能id
    private int skillID;
    //技能名称
    private string skillName;
    // 图标地址，用地址保存，当要使用时进行加载
    private string iconPath;

    // 技能描述
    private string description;
    // 技能类型
    private string skillType;

    // 对技能的目标,附加的状态,造成的伤害等进行描述,由子类进行重写
    private string targetDescription;

    // 消耗mp
    //private int mp;


    // 技能影响范围，是一个以r为半径的圆
    private float skillInfluenceRadius;

    // 技能CD时间
    private float cooldown;
    // 施法持续时间,为0时表示该技能不会持续释放
    private float spellDuration;

    // 施法时，自身产生的特效
    private GameObject selfEffect;
    // 施法时，目标产生的特效
    private GameObject targetEffect;
    // 额外属性(子类技能/操作中额外需要的属性,如闪电链技能需要链接次数等等)
    private Dictionary<string, object> extraAttributes = new Dictionary<string, object>();


    public SkillModel() { }

    //// 实现对extraAttributes的快速赋值
    //public SkillModel(params Tuple<string, object>[] tuples)
    //{
    //    foreach (var o in tuples)
    //    {
    //        extraAttributes[o.First] = o.Second;
    //    }
    //}

    public string SkillName
    {
        get
        {
            return skillName;
        }

        set
        {
            skillName = value;
        }
    }

    public string IconPath
    {
        get
        {
            return iconPath;
        }

        set
        {
            iconPath = value;
        }
    }





    public string SkillType
    {
        get
        {
            return skillType;
        }

        set
        {
            skillType = value;
        }
    }

    public string TargetDescription
    {
        get
        {
            return targetDescription;
        }

        set
        {
            targetDescription = value;
        }
    }



    public float SkillInfluenceRadius
    {
        get
        {
            return skillInfluenceRadius;
        }

        set
        {
            skillInfluenceRadius = value;
        }
    }

    public float Cooldown
    {
        get
        {
            return cooldown;
        }

        set
        {
            cooldown = value;
        }
    }

    public float SpellDuration
    {
        get
        {
            return spellDuration;
        }

        set
        {
            spellDuration = value;
        }
    }


    public GameObject SelfEffect
    {
        get
        {
            return selfEffect;
        }

        set
        {
            selfEffect = value;
        }
    }

    public GameObject TargetEffect
    {
        get
        {
            return targetEffect;
        }

        set
        {
            targetEffect = value;
        }
    }

    public Dictionary<string, object> ExtraAttributes
    {
        get
        {
            return extraAttributes;
        }

        set
        {
            extraAttributes = value;
        }
    }

    public int SkillID
    {
        get
        {
            return skillID;
        }

        set
        {
            skillID = value;
        }
    }

}
