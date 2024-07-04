using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battler : MonoBehaviour
{
    private int id;
    private byte alignment;
    //[SerializeField] bool AiControlled = false;
    [SerializeField] int XPRewardedOnKill;
    public int xpRewardedOnKill => XPRewardedOnKill;
    private bool IsPlayer;
    public bool isPlayer => IsPlayer;
    private Animator animator;
    private Character assignedCharacter;
    public Character character { get { return assignedCharacter; } }
    private string name;
    private bool isAlive;
    public bool characterAssigned = false;
    //public StatusEffect[] effects;


    //Stats---------------------------------------------
    private int maxHP;
    private int currentHP;
    private int maxSP;
    private int currentSP;
    private int Atk;
    public int atk => Atk;
    private int Def;
    public int def => Def;
    private int Magic;
    public int magic => Magic;
    private int Speed;
    public int speed => Speed;
    private int CritR;
    //------------------------------------------------------

    //Events
    public event Action<int> OnHealthChanged;
    public event Action<int> OnSPChanged;
    public event Action OnHit;
    public event Action OnAnimationEnd;
    public event Action<int, int> OnDeath;

    //------------------------------------------------------


    private void Awake()
    {
        //Character is assigned by the battle manager
        animator = gameObject.GetComponent<Animator>();
        
    }

    private void Start()
    {        
        if (currentHP <= 0)
        {
            isAlive = false;
        }       
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Initialize(Character character, int ID, byte side)
    {
        id = ID;
        alignment = side;
        assignedCharacter = character;
        name = character.name;
        maxHP = character.maxHp;
        currentHP = character.currentHP;
        maxSP = character.maxSp;
        currentSP = character.currentSP;
        Atk = character.atk;
        Def = character.def;
        Magic = character.magic;
        Speed = character.speed;
        CritR = character.critR;

        characterAssigned = true;

        if(character is PlayerCharacter)
        {
            IsPlayer = true;
        }

        print("character assigned");
        
    }

    public void IncreaseHealth(int value)
    {
        if ((currentHP += value) < maxHP)
        {
            currentHP += value;
        }
        else { currentHP = maxHP;}
        OnHealthChanged(currentHP);
    }

    public void TakeDamage(int amount, int multiplier = 1)
    {
        currentHP -= amount * multiplier;
        if(currentHP <= 0)
        {
            isAlive = false;
            OnDeath(id, alignment);
        }
        OnHealthChanged(currentHP);
    }
    
    public void ReduceSP(int amount)
    {
        currentSP -= amount;
        OnSPChanged(currentSP);
    }

    public void IncreaseSP(int amount)
    {
        currentSP += amount;
        OnSPChanged(currentSP);
    }   

    public void DealDamage(Battler target, int dmg)
    {
        int dodge = UnityEngine.Random.Range(1, 100);
        if (dodge < target.Speed / 2)
        {
            target.Dodge();
        }
        else
        {
            int crit = UnityEngine.Random.Range(1, 100);
            if (crit <= CritR)
            {
                target.TakeDamage(dmg, 2);                
            }
            else
            {
                target.TakeDamage(dmg, 1);                
            }
        }        
    }

    public void FireProjectile()
    {

    }

    public void Dodge()
    {
        GetComponent<Animator>().SetTrigger("dodge");        
    }

    public void PlayAnimation(int index)
    {
        switch (index)
        {
            case 0:
                try { animator.SetTrigger("attack"); }
                catch { animator.SetTrigger("skill1"); }
                //print("animation: attack");
                break;
            case 1:
                try { animator.SetTrigger("skill1"); }
                catch { animator.SetTrigger("skill1"); }
                //print("animation: skill1");
                break;
            case 2:
                try { animator.SetTrigger("skill2"); }
                catch { animator.SetTrigger("skill1"); }
                //print("animation: skill2");
                break;

            case 3:
                try { animator.SetTrigger("skill3"); }
                catch { animator.SetTrigger("skill1"); }
                //print("animation: skill3");
                break;
            case 4:
                try { animator.SetTrigger("buff"); }
                catch { animator.SetTrigger("skill1"); }
                //print("animation: buff");
                break;
        }        
    }

    public void OnHitEvent()
    {
        OnHit();
    }

    public void OnAnimationEndEvent()
    {
        OnAnimationEnd();
    }  

    public void ForceStatusUpdate()
    {
        OnHealthChanged(currentHP);
        OnSPChanged(currentSP);
    }

    public void ResetLocalPosition()
    {
        this.gameObject.transform.localPosition = new Vector3(0, 0, 0);
    }

}
