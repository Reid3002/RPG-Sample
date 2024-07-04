using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item", menuName = "Data/SkillData/HealSkill")]
public class HealSkill : Skill, ISkill
{
    [SerializeField] string Name;

    [SerializeField] string Description;
    public string description => Description;

    private string actionMessage = "";

    [SerializeField] int HealAmount;
    public int healAmount => HealAmount;

    [Tooltip("healAmount + RoundToInt(magic * MagicModifier")]
    [SerializeField] float MagicModifier;

    [SerializeField] int SPcost;

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
    public void UseSkill(Battler user, Battler[] targets)
    {       
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].IncreaseHealth(healAmount+ Mathf.RoundToInt(user.magic*MagicModifier));
        }
        actionMessage = user.character.name + " healed " + (healAmount + Mathf.RoundToInt(user.magic * MagicModifier).ToString() + " to selected allies");
        user.ReduceSP(SPcost);
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
