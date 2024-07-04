using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private protected string Name;
    public string name => Name;
    [SerializeField] private protected Sprite Portrait;
    public Sprite portrait => Portrait;

    [SerializeField] private protected int MaxHP;
    public int maxHp { get { return MaxHP; }}
    [SerializeField] private protected int MaxSP;
    public int maxSp { get { return MaxSP; }}
    public int currentSP;
    public int currentHP;
    [SerializeField] private protected int Atk;
    public int atk => Atk;
    [SerializeField] private protected int Def;
    public int def => Def;
    [SerializeField] private protected int Magic;
    public int magic => Magic;
    [SerializeField] private protected int Speed;
    public int speed => Speed;
    [SerializeField] private protected int CritR;
    public int critR => CritR;
    

    [SerializeField] protected ClassTemplate CharacterClass;
    public ClassTemplate characterClass { get { return CharacterClass; } }

    
}
