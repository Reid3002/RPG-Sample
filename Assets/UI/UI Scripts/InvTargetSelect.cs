using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvTargetSelect : MonoBehaviour, IOptions
{
    [SerializeField] GameObject[] charactersInfo = new GameObject[5];
    [SerializeField] Selector selector;
    private PartyManager partyManager;
    [HideInInspector] public Item item;

    private void Awake()
    {
        partyManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PartyManager>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < charactersInfo.Length; i++)
        {
            charactersInfo[i].SetActive(false);
        }

        List<AllyCharacter> characters = partyManager.CheckParty();
        int b = 0;
        foreach (AllyCharacter character in characters)
        {
            charactersInfo[b].gameObject.GetComponent<CharacterInfo>().GetCharacterInfo(b);
            charactersInfo[b].gameObject.SetActive(true);

            b++;
        }

        selector.canMove = true;
    }

    private void OnDisable()
    {
        selector.canMove = false;
    }


    public void OptionSelected(int option)
    {
        if (((int)item.type) == 0)
        {
            
        }
        else if (((int)item.type) == 1)
        {
            Equipment selectedEquipment = item as Equipment;

            if (selectedEquipment.p_equipmentSlot.ToString()== "Weapon")
            {
                if (partyManager.CheckParty()[option].characterClass.weapon.ToString() == selectedEquipment.p_weaponType.ToString())
                {
                    partyManager.CheckParty()[option].Equip(selectedEquipment);
                    gameObject.SetActive(false);
                }
            }
            else if (selectedEquipment.p_equipmentSlot.ToString() == "Armor")
            {
                if (partyManager.CheckParty()[option].characterClass.armor.ToString() == selectedEquipment.p_armorType.ToString())
                {
                    partyManager.CheckParty()[option].Equip(selectedEquipment);
                    gameObject.SetActive(false);
                }
            }
                     

        }
    }
}
