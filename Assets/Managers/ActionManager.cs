using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private static List<IAction> actions = new List<IAction>();
    private static bool start;
    [SerializeField] StartTurnButton button;
    public event Action OnTurnEnd;


    //Singleton******************************************************
    private static ActionManager Instance;
    public static ActionManager instance { get { return Instance; } }
    //*************************************************************
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        button.OnTurnStart += StartTurn;

    }
   
    void Update()
    {
        if (start)
        {
            if(actions.Count == 0)
            {                
                OnTurnEnd();
                print("No more actions left in queue, the turn ends.");
                start = false;
                return;
            }
            else if (actions[0] == null)
            {                
                OnTurnEnd();
                print("the current action was NULL, the Update method was stoped.");
                start = false;
                return;
            }

            if (actions[0].HasActionEnded() == true)
            {
                actions.Remove(actions[0]);
            }
            else if (actions[0].HasActionStarted() == false)
            {
                if(actions[0].GetBattler() != null)
                {
                    actions[0].StartAction();
                }
                else { actions.Remove(actions[0]); }
            }            
            
        }
        
    }

    public void AddAction(IAction action)
    {
        actions.Add(action);

        if (actions.Count > 1)
        {
            actions.Sort((a, b) => a.GetSpeed().CompareTo(b.GetSpeed()));
        }        
    }

    private void StartTurn()
    {
        print("The turn has started");
        start = true;
    }
}
