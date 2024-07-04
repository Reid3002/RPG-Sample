using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    public string GetName();
    public string GetMessage();
    public int GetSPCost();
    public string GetTargetType();
    public int GetMaxTargetsForSkill();
    public int GetAnimation();
    public void UseSkill(Battler user, Battler[] targets);
}
