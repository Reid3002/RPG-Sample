using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction 
{
    public int GetSpeed();

    public Battler GetBattler();
    public bool HasActionEnded();
    public void StartAction();
    public bool HasActionStarted();
    public void Execute();

}
