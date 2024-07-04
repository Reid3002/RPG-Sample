using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTurnButton : MonoBehaviour
{
    private BattleInfo[] battleInfo;
    public event Action OnTurnStart;

    public void SetInfoPanels(BattleInfo[] info)
    {        
        battleInfo = info;
    }

    public void ClearInfoPanels()
    {
        battleInfo = null;
    }

    public void StartTurn()
    {
        foreach (BattleInfo info in battleInfo)
        {
            if (info.GetSelectedAction() != null)
            {
                ActionManager.instance.AddAction(info.GetSelectedAction());
            }            
        }
        OnTurnStart();
        //print("Turn Started");
    }
}
