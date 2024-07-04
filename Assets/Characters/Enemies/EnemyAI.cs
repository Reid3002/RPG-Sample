using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Battler battler;
    public Character character;
    public BattleInfo infoPanel;
    private List<Battler> allies;
    private List<Battler> enemies;
    [SerializeField] ActionManager actionManager;
    private bool initialized = false;   

    public void StartAI(List<Battler> Allies, List<Battler> Enemies)
    {
        if (infoPanel.gameObject.activeSelf)
        {
            actionManager.OnTurnEnd += ChooseSkill;
        }
        allies = Allies;
        enemies = Enemies;
        battler.OnDeath += RemoveFromEvents;
        ChooseSkill();
    }

    public void SuscribeToTurnEnd()
    {
        if (infoPanel.gameObject.activeSelf)
        {
            actionManager.OnTurnEnd += ChooseSkill;
            actionManager.OnTurnEnd += UpdateTargets;
        }
    }

    private void ChooseSkill()
    {
        int skill = UnityEngine.Random.Range(0, character.characterClass.skills.Length);
        List<Battler> target = new List<Battler>();
        switch (character.characterClass.GetSkill(skill).GetTargetType().ToString())
        {
            case "ally":
                target.Add(allies[UnityEngine.Random.Range(0,allies.Count)]);
                infoPanel.SetSelectedAction(new SkillAction(character.characterClass.GetSkill(skill), target, battler));
                break;

            case "enemy":
                target.Add(enemies[UnityEngine.Random.Range(0, enemies.Count /*- 1*/)]);
                infoPanel.SetSelectedAction(new SkillAction(character.characterClass.GetSkill(skill), target, battler));
                break;

            case "self":
                target.Add(battler);
                infoPanel.SetSelectedAction(new SkillAction(character.characterClass.GetSkill(skill), target, battler));
                break;
        }        
    }

    private void UpdateTargets()
    {
        int i = 0;
        foreach (Battler battler in allies)
        {
            if (battler == null)
            {
                allies.Remove(allies[i]);
            }
            i++;
        }

        int b = 0;
        foreach (Battler battler in enemies)
        {
            if (battler == null)
            {
                enemies.Remove(enemies[b]);
            }
            b++;
        }
    }

    private void RemoveFromEvents(int a, int b)
    {
        actionManager.OnTurnEnd -= ChooseSkill;
        actionManager.OnTurnEnd -= UpdateTargets;
    }
}
