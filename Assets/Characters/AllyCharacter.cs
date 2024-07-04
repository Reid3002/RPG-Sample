using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCharacter : Character
{
    private int level = 1;
    public int Level => level;

    [SerializeField]private float[] lvlUpXpRequirement;

    private float currentXP;
    public float CurrentXP => currentXP;

    public Equipment weapon;

    public Equipment armor;

    public string activePotion;

    private PartyManager partyManager;
    protected PlayerInventory playerInventory;

    private void Start()
    {
        partyManager = PartyManager.instance;
        playerInventory = partyManager.GetCharacter(0).playerInventory;       

    }   
   

    public void Equip(Equipment item)
    {
        if (partyManager == null)
        {
            partyManager = PartyManager.instance;
        }
        if (playerInventory == null)
        {
            playerInventory = partyManager.GetCharacter(0).playerInventory;
        }
        if (((int)item.p_equipmentSlot) == 0)
        {
            if (weapon == null)
            {
                weapon = item;
                Atk += item.Atk;
                Def += item.Def;
                Magic += item.Magic;
                Speed += item.Speed;
                CritR += item.CritR;               
                playerInventory.Discard(weapon);

            }
            else
            {
                Unequip(0);
                weapon = item;
                Atk += item.Atk;
                Def += item.Def;
                Magic += item.Magic;
                Speed += item.Speed;
                CritR += item.CritR;
                playerInventory.Discard(weapon);
            }

        }
        else if (((int)item.p_equipmentSlot) == 1)
        {
            if (armor == null)
            {
                armor = item;
                Atk += item.Atk;
                Def += item.Def;
                Magic += item.Magic;
                Speed += item.Speed;
                CritR += item.CritR;
                playerInventory.Discard(armor);
            }
            else
            {
                Unequip(1);
                armor = item;
                Atk += item.Atk;
                Def += item.Def;
                Magic += item.Magic;
                Speed += item.Speed;
                CritR += item.CritR;
                playerInventory.Discard(armor);
            }


        }

    }

    public void Unequip(int slot)
    {
        if (slot == 0 && weapon != null)
        {
            Atk -= weapon.Atk;
            Def -= weapon.Def;
            Magic += weapon.Magic;
            playerInventory.AddItem(weapon);
            weapon = null;
        }
        else if (slot == 1 && armor != null)
        {
            Atk -= weapon.Atk;
            Def -= weapon.Def;
            Magic += weapon.Magic;
            playerInventory.AddItem(armor);
            armor = null;
        }
    }

    public float GetXpToNextLvl()
    {
        return lvlUpXpRequirement[level-1];
    }

    public void GainXP(float xp)
    {
        if (level < lvlUpXpRequirement.Length)
        {
            currentXP += xp;
            //XpGainedEvent();
            LvlUp();
        }
    }

    private void LvlUp()
    {
        if (currentXP > lvlUpXpRequirement[level])
        {
            currentXP -= lvlUpXpRequirement[level];
            level++;
            MaxHP = RPGUtilities.CalculateStatByLevel(MaxHP, Level, characterClass.maxHpModifier);
            MaxSP = RPGUtilities.CalculateStatByLevel(MaxSP, Level, characterClass.maxSpModifier);
            Atk = RPGUtilities.CalculateStatByLevel(Atk, Level, characterClass.atkModifier);
            Def = RPGUtilities.CalculateStatByLevel(Def, Level, characterClass.defModifier);
            Magic = RPGUtilities.CalculateStatByLevel(Magic, Level, characterClass.magicModifier);
            Speed = RPGUtilities.CalculateStatByLevel(Speed, Level, characterClass.speedModifier);
            CritR = RPGUtilities.CalculateStatByLevel(CritR, Level, characterClass.critRModifier);
        }
        print("You leveled up to lvl " + (level-1).ToString());
    }
}
