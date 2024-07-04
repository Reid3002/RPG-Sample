using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleInfo : MonoBehaviour
{
    private int ID;
    public int id => ID;
    private Battler battler;
    [SerializeField] Text characterName;
    [SerializeField] Slider hpBar;
    [SerializeField] Slider spBar;
    [SerializeField] Image characterPortrait;
    [SerializeField] Image classIcon;
    [SerializeField] Text className;
    [SerializeField] GameObject actionSelector;

    [SerializeField] TextMeshPro testtext;

    private IAction SelectedAction; //it's used by the UIManager
    private bool initialized = false; //it's handled by the BattleManager 
    public event Action<ISkill, Battler, BattleInfo> OnSkillSelected;
    [SerializeField] bool isEnemy;
    private ActionManager actionManager;
    public event Action OnBattlerAssigned;


    private void Awake()
    {
        
    }
    private void Start()
    {
        actionManager = ActionManager.instance;
        
        actionManager.OnTurnEnd += ClearSelectedAction;
        actionManager.OnTurnEnd += ResetDropDown;
    }
    private void Update()
    {
             
    }
    public void UpdateHealthBar(int value)
    {
        hpBar.value = value;        
    }

    public void UpdateSPBar(int value)
    {
        spBar.value = value;
    }

    public void Initialize(Character character, Battler thisBattler, int Id)
    {
        ID = Id;
        battler = thisBattler;
        characterName.text = character.name;
        hpBar.maxValue = character.maxHp;
        spBar.maxValue = character.maxSp;
        hpBar.value = character.currentHP;
        spBar.value = character.currentSP;
        characterPortrait.sprite = character.portrait;
        classIcon.sprite = character.characterClass.classIcon;
        className.text = character.characterClass.name;
        initialized = true;

        battler.OnHealthChanged += UpdateHealthBar;
        battler.OnSPChanged += UpdateSPBar;

        if (isEnemy == false)
        {
            actionSelector.GetComponent<Dropdown>().options.Clear();
            actionSelector.GetComponent<Dropdown>().options.Add(item: new Dropdown.OptionData(text:"", image: null));

            if (character.characterClass.skills != null && character.characterClass.skills[0] != null)
            {
                for (int i = 0; i< character.characterClass.skills.Length; i++)
                {
                    ISkill skill = character.characterClass.GetSkill(i);

                    actionSelector.GetComponent<Dropdown>().options.Add(item: new Dropdown.OptionData(text: skill.GetName() + " " + "SP: " + skill.GetSPCost().ToString(), image: null));
                }
            }           

        }        
    }
    
    public Dropdown GetDropdown()
    {
        return actionSelector.GetComponent<Dropdown>();
    }

    public void SetSelectedSkill()
    {
        int skill = actionSelector.GetComponent<Dropdown>().value -1;

        if (skill >= 0 && battler.character.characterClass.GetSkill(skill).GetSPCost() <= spBar.value)
        {
            OnSkillSelected(battler.character.characterClass.GetSkill(skill), battler, this);
        }
        else if (battler.character.characterClass.GetSkill(skill).GetSPCost() > spBar.value)
        {
            actionSelector.GetComponent<Dropdown>().value = 0;
        }
        
        //print(actionSelector.GetComponent<Dropdown>().value + " " + battler.character.characterClass.GetSkill(skill).GetName());
    }

    public void SetSelectedAction(IAction action)
    {
        SelectedAction = action;
    }

    public IAction GetSelectedAction()
    {
        return SelectedAction;
    }

    private void ClearSelectedAction()
    {
        SelectedAction = null;
    }

    public void HideDropDown()
    {
        actionSelector.SetActive(false);
    }

    public void ShowDropDown()
    {
        actionSelector.SetActive(true);
    }

    private void ResetDropDown()
    {
        try { actionSelector.GetComponent<Dropdown>().value = 0; }
        catch { }
    }

    public Battler GetBattler()
    {
        return battler;
    }
}
