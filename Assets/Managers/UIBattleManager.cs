using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBattleManager : MonoBehaviour, IOptions
{
    [SerializeField] GameObject allySelector;
    [SerializeField] GameObject enemySelector;
    [SerializeField] private GameObject[] characterInfo;
    [SerializeField] private List<GameObject> activeCharacterInfo = new List<GameObject>();
    [SerializeField] private GameObject[] enemyInfo;
    [SerializeField] private StartTurnButton startButton;
    [SerializeField] private Text actionMessage;
    [SerializeField] private ActionManager actionManager;
    private bool selectingSkillTargets = false;
    public BattleInfo currentInfoPanel;

    //Singleton******************************************************
    private static UIBattleManager Instance;
    public static UIBattleManager instance { get { return Instance; } }
    //**************************************************************


    //Temporal info
    SkillData skillData;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        List<BattleInfo> startButtonInfo = new List<BattleInfo>();
        foreach(var item in characterInfo)
        {
            if (item.activeSelf == true)
            {
                item.GetComponent<BattleInfo>().OnSkillSelected += ReadSelectedSkill;
                activeCharacterInfo.Add(item);
                startButtonInfo.Add(item.GetComponent<BattleInfo>());
            }            
        }
        foreach (var item in enemyInfo)
        {
            if (item.activeSelf == true)
            {                
                startButtonInfo.Add(item.GetComponent<BattleInfo>());
            }
        }

        startButton.ClearInfoPanels();
        startButton.SetInfoPanels(startButtonInfo.ToArray());
        startButton.OnTurnStart += HideDropdowns;
        actionManager.OnTurnEnd += ShowDropdowns;
    }

    private void Update()
    {
        if (selectingSkillTargets )
        {
            if (skillData.selectedTargets.Count == skillData.maxTargets)
            {
                skillData.activeSelector.gameObject.SetActive(false);
                currentInfoPanel.SetSelectedAction(new SkillAction(skillData.skillToExecute, skillData.selectedTargets, skillData.caster));
                ShowDropdowns();
                //print("Show dropdowns");
                selectingSkillTargets = false;
                currentInfoPanel = null;
            }
        }        
    }

    public void ReadSelectedSkill(ISkill skill, Battler user, BattleInfo infoPanel)
    {
        currentInfoPanel = infoPanel;
        skillData = new SkillData(skill, user);
        HideDropdowns();
        //print("dropdowns hidden");
        print(skill.GetName());

        skillData.maxTargets = skill.GetMaxTargetsForSkill();

        print(skill.GetTargetType());

        switch (skill.GetTargetType())
        {            
            case "ally":                
                foreach (var item in activeCharacterInfo)
                {
                        skillData.targetList.Add(item);
                }
                allySelector.GetComponent<Selector>().SetOptionsList(skillData.targetList);
                skillData.activeSelector = allySelector.GetComponent<Selector>();
                allySelector.SetActive(true);
                //print("show ally selector");
                break;

            case "enemy":
                
                foreach (var item in enemyInfo)
                {
                    if (item.activeSelf)
                    {
                        skillData.targetList.Add(item);
                    }
                }

                enemySelector.GetComponent<Selector>().SetOptionsList(skillData.targetList);
                skillData.activeSelector = enemySelector.GetComponent<Selector>();
                enemySelector.SetActive(true);
                break;

            case "self":
                
                break;
        }

        if (skillData.targetList.Count < skillData.maxTargets)
        {
            skillData.maxTargets = skillData.targetList.Count;
        }

        selectingSkillTargets = true;
                
    }

    public struct SkillData
    {
        public SkillData(ISkill skill, Battler battler)
        {
            skillToExecute = skill;
            caster = battler;
            targetList = new List<GameObject>();         
            selectedTargets = new List<Battler>();
            activeSelector = null;
            maxTargets = 1;
        }
        public readonly ISkill skillToExecute;
        public readonly Battler caster;
        public List<GameObject> targetList;
        public Selector activeSelector;
        public List<Battler> selectedTargets;
        public int maxTargets;
    }
    
    private void HideDropdowns()
    {
        foreach (var item in activeCharacterInfo)
        {
            item.GetComponent<BattleInfo>().HideDropDown();
        }
    }

    private void ShowDropdowns()
    {
        foreach (var item in activeCharacterInfo)
        {
            item.GetComponent<BattleInfo>().ShowDropDown();
        }
    }

    public void OptionSelected(int option)
    {
        if (skillData.selectedTargets.Count != skillData.maxTargets)
        {
            skillData.selectedTargets.Add(skillData.targetList[skillData.activeSelector.GetResult()].GetComponent<BattleInfo>().GetBattler());
            skillData.activeSelector.RemoveOption(skillData.activeSelector.GetResult());
            skillData.activeSelector.ResetIndex();
        }


    }

    public void ShowMessage(string message)
    {
        actionMessage.text = message;
    }
}
