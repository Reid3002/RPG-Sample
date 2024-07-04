using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item", menuName = "Data/SkillData/DamageSkill")]
public class DamageSkill : Skill, ISkill
{
    [SerializeField] string Name;

    [SerializeField] string Description;
    public string description => Description;

    private string ActionMessage;
    public string actionMessage => ActionMessage;

    [SerializeField] int damage;
    [SerializeField] int SPcost;
    [SerializeField] int SPgain;

    [SerializeField] targetType TargetType;
    public enum targetType
    {
        ally,
        enemy,
        self
    }

    [SerializeField] private int MaxTargetsForSkill = 1;

    [SerializeField] skillAnimation animation;
    public enum skillAnimation
    {
        attack,
        skill1,
        skill2,
        skill3
    }

    public string GetTargetType()
    {
        return TargetType.ToString();
    }

    public int GetMaxTargetsForSkill()
    {
        return MaxTargetsForSkill;
    }

    public int GetAnimation()
    {
        return ((int)animation);
    }

    public void UseSkill(Battler user, Battler[] targets)
    {
        foreach (var target in targets)
        {
            user.DealDamage(target, (damage + user.atk));
        }
        user.ReduceSP(SPcost);
        user.IncreaseSP(SPgain);

        if(targets.Length > 1)
        {
            ActionMessage = user.character.name + " dealt " + (damage + user.atk).ToString() + " Damage to several enemies";
        }
        else
        {
            ActionMessage = user.character.name + " dealt " + (damage + user.atk).ToString() + " Damage to " + targets[0].character.name;
        }
        
    }

    public string GetName()
    {
        return Name;
    }

    public int GetSPCost()
    {
        return SPcost;
    }

    public string GetMessage()
    {
        return actionMessage;
    }
}
