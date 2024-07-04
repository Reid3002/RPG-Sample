using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "new item", menuName = "Data/ClassData")]
public class ClassTemplate : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] int Id;
    public int id => Id;
    [SerializeField] Sprite ClassIcon;
    public Sprite classIcon { get { return ClassIcon; } }


    [Header("Stats increase by level")]
    [SerializeField] private float MaxHPModifier;
    public float maxHpModifier { get { return MaxHPModifier; } }

    [SerializeField] private float MaxSPModifier;
    public float maxSpModifier { get { return MaxSPModifier; } }

    [SerializeField] float AtkModifier;
    public float atkModifier => AtkModifier;

    [SerializeField] private float DefModifier;
    public float defModifier => DefModifier;

    [SerializeField] private float MagicModifier;
    public float magicModifier => MagicModifier;

    [SerializeField] private float SpeedModifier;
    public float speedModifier => SpeedModifier;

    [SerializeField] private float CritRModifier;
    public float critRModifier => CritRModifier;


    [Header("Equipment")]
    [SerializeField] usableWeapon Weapon;
    public usableWeapon weapon { get { return Weapon; }}

    [SerializeField] usableArmor Armor;
    public usableArmor armor { get { return Armor; }}
    public enum usableWeapon
    {
        Sword,
        Axe,
        Staff,
        Tome,
        Bow,
    }

    public enum usableArmor
    {
        LightArmor,
        MediumArmor,
        HeavyArmor,
        Robe,
    }    

    [Header("Skills available to this class")]
    [SerializeField] Skill[] SkillList;
    public Skill[] skills { get { return SkillList; } }

    [Header("Battler Prefab")]
    [SerializeField] GameObject Battler;
    public GameObject battler => Battler;

    public ISkill GetSkill(int skill)
    {
        return skills[skill] as ISkill;
    }
}
