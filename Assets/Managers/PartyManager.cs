using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    // Singleton***********************************

    private static PartyManager Instance;

    public static PartyManager instance { get { return Instance; } }




    //*************************************


    [SerializeField] List<AllyCharacter> Party= new List<AllyCharacter>();
    [SerializeField] private List<AllyCharacter> ActiveParty = new List<AllyCharacter>();
    private Dictionary<int, string> Ids = new Dictionary<int, string>();
    private int index = 0;
    private int activeIndex = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public bool AddCharacter(AllyCharacter character)
    {
        if (Party.Count < 4)
        {
            Party.Add(character);
            //character.id = index+1;            
            //Ids.Add(character.id, character.name);
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public void RemoveCharacter(AllyCharacter character)
    {
        if (character != Party[0])
        {
            Party.Remove(character);
            //Ids.Remove(character.id);
        }
        
    }

    public List<AllyCharacter> CheckParty()
    {
        return Party;
    }

    public AllyCharacter GetCharacter(int index)
    {
        return Party[index];
    }

    //public KeyValuePair<int,string> SearchCharacter(int id)
    //{
    //    return Ids.;
    //}

    public void DistributeXP(int xp)
    {
        float individualXP = xp / Party.Count;
        foreach(AllyCharacter character in Party)
        {
            character.GainXP(individualXP);
        }
    }
}
