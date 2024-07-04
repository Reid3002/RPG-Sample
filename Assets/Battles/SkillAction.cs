using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : IAction
{
    private readonly Battler actor;
    private readonly int speed;
    private readonly ISkill skillToExecute;
    private readonly List<Battler> skillTargets;
    private bool actionStarted = false;
    private bool actionFinished = false;


    public SkillAction(ISkill skill, List<Battler> targets, Battler caster)
    {
        actor = caster;
        speed = caster.speed;
        skillToExecute = skill;
        skillTargets = targets;
    }

    public bool HasActionEnded()
    {
        return actionFinished;
    }

    public void StartAction()
    {
        for(int i = 0; i< skillTargets.Count; i++)
        {
            if (skillTargets[i] == null)
            {
                skillTargets.RemoveAt(i);
            }
        }

        if (skillTargets.Count > 0)
        {
            actor.OnHit += Execute;
            actor.OnAnimationEnd += EndAction;
            actor.PlayAnimation(skillToExecute.GetAnimation());

            actionStarted = true;
        }
        else
        {
            actionFinished = true;
        }

    }

    public bool HasActionStarted()
    {
        return actionStarted;
    }
    public void Execute()
    {
        skillToExecute.UseSkill(actor, skillTargets.ToArray());
        UIBattleManager.instance.ShowMessage(skillToExecute.GetMessage());
    }

    private void EndAction()
    {
        actor.OnHit -= Execute;
        actor.OnAnimationEnd -= EndAction;
        actor.ResetLocalPosition();
        actor.ResetLocalPosition();
        actionFinished = true;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public Battler GetBattler()
    {
        return actor;
    }
}
