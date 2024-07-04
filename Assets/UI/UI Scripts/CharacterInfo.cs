using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField] Sprite errorImage;
    [SerializeField] Sprite emptySlot;
    private Image portrait;
    [SerializeField] Text characterName;
    [SerializeField] Image classImage;
    [SerializeField] Text className;
    [SerializeField] Image weaponSlot;
    [SerializeField] Text weaponName;
    [SerializeField] Image armorSlot;
    [SerializeField] Text armorName;

    private int Character;
    public int character { get { return Character; } }
    private PartyManager partyManager;

    private void Awake()
    {
        
    }

    public void GetCharacterInfo(int characterIndex)
    {
        if (partyManager == null)
        {
            partyManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PartyManager>();
        }
        
        if (portrait == null)
        {
            portrait = this.gameObject.GetComponent<Image>();
        }
       

        Debug.Log(characterIndex);
        AllyCharacter thisCharacter = partyManager.GetCharacter(characterIndex);
        Character = characterIndex;
        Debug.Log("name: "+thisCharacter.name);
        if (thisCharacter != null)
        {
         //Validations to assign variables

            //PORTRAIT
            if (thisCharacter.portrait!=null) 
            {
                portrait.sprite = thisCharacter.portrait;
            } 
            else {portrait.sprite = errorImage;}

            //CHARACTER NAME
            if (thisCharacter.name != null) characterName.text = thisCharacter.name; else characterName.text = "text not found";

            classImage.sprite = thisCharacter.characterClass.classIcon;

            //CLASS
            className.text = thisCharacter.characterClass.name;

            //WEAPON SPRITE
            if (thisCharacter.weapon != null && thisCharacter.weapon.sprite != null){weaponSlot.sprite = thisCharacter.weapon.sprite;}
            else if (thisCharacter.weapon == null){weaponSlot.sprite = emptySlot;}
            else if(thisCharacter.weapon.sprite) { weaponSlot.sprite = errorImage; }

            //WEAPON NAME
            if (thisCharacter.weapon != null && thisCharacter.weapon.name != null) { weaponName.text = thisCharacter.weapon.name; }
            else if (thisCharacter.weapon == null) { weaponName.text = ""; }
            else if (thisCharacter.weapon.name == null) {weaponName.text = "tect not found";}

            //ARMOR SPRITE
            if (thisCharacter.armor != null && thisCharacter.armor.sprite != null){ armorSlot.sprite = thisCharacter.armor.sprite;}
            else if (thisCharacter.armor == null){ armorSlot.sprite = emptySlot;}
            else if (thisCharacter.armor.sprite == null){ armorSlot.sprite = errorImage;}

            //ARMOR NAME
            if (thisCharacter.armor != null && thisCharacter.armor.name != null){armorName.text = thisCharacter.armor.name;}
            else if (thisCharacter.armor == null){armorName.text = "";}
            else if (thisCharacter.armor.name == null) {weaponName.text = "text not found";}
        }        
    }
}
