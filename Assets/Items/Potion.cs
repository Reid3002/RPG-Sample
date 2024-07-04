using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    [SerializeField] private int amount;
    [SerializeField] private stat statToIncrease;
   private enum stat
    {
        MaxHp,
        HP,
        MaxSP,
        SP,
        Atk,
        Def,
        Magic,
        Speed,
        CritR
    }
    
    public void Use()
    {
       CommandQueue.Instance.Enqueue(new StatIncreaseCommand());
    }
}
