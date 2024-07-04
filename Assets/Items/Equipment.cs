using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New equipment", menuName = "Data/Item Data/EquipmentData")]

public class Equipment : Item
{
    [Header("Bonus Stats")]

    [SerializeField] int atk;
    public int Atk { get { return atk; } }


    [SerializeField] int def;
    public int Def { get { return def; } }


    [SerializeField] int magic;
    public int Magic { get { return magic; } }

    [SerializeField] int speed;
    public int Speed { get { return speed; } }

    [SerializeField] int critR;
    public int CritR { get { return critR; } }   

    [Header("Tags")]
    [SerializeField] equipmentSlot EquipmentSlot;
    public equipmentSlot p_equipmentSlot { get { return EquipmentSlot; } }

    [SerializeField] weaponType WeaponType;
    public weaponType p_weaponType { get {return WeaponType;} }


    [SerializeField] armorType ArmorType;
    public armorType p_armorType { get { return ArmorType; } }
    
    public enum equipmentSlot
    {
        Weapon,
        Armor
    }

    public enum weaponType
    {
        none,
        Sword,
        Axe,
        Tome        
    }

    public enum armorType
    {
        none,
        HeavyArmor,
        MediumArmor,
        Robe
    }
}
