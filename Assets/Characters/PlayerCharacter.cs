using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : AllyCharacter
{
    public PlayerInventory inventory => playerInventory;
    void Start()
    {
        GameManager.instance.GetComponent<PartyManager>().AddCharacter(this);
        playerInventory = GetComponent<PlayerInventory>();
        //inventory = new PlayerInventory(20);
    }
}
