using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusScreen : MonoBehaviour, IOptions
{
    [SerializeField] GameObject[] charactersInfo = new GameObject[4];
    //[SerializeField] Selector selector;
    private PartyManager partyManager;
    //[HideInInspector] public Item item;   

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
        //{
        //    this.gameObject.SetActive(false);
        //}
    }

    private void OnEnable()
    {
        if (partyManager == null)
        {
            partyManager = PartyManager.instance;
        }

        for (int i = 0; i < charactersInfo.Length; i++)
        {
            charactersInfo[i].SetActive(false);
        }

        List<AllyCharacter> characters = partyManager.CheckParty();
        int b = 0;
        foreach (AllyCharacter character in characters)
        {
            charactersInfo[b].gameObject.GetComponent<CharacterStatus>().GetCharacterInfo(b);
            charactersInfo[b].gameObject.SetActive(true);

            b++;
        }

        //selector.canMove = true;
    }

    private void OnDisable()
    {
        //selector.canMove = false;
    }


    public void OptionSelected(int option)
    {
       
    }
}
