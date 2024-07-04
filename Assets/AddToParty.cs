using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToParty : MonoBehaviour, IInteractable
{
    [SerializeField] AllyCharacter rewardCharacter;    

    public void Interacted(GameObject interactor)
    {
        print("interacted");
        if (rewardCharacter != null && interactor.CompareTag("Player"))
        {
            PartyManager.instance.AddCharacter(rewardCharacter);
            print("Character awarded");
            Destroy(this.gameObject);   
        }
    }

}
