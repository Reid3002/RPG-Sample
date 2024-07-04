using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] List<Item> rewardItems;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interacted(GameObject interactor)
    {
        if (rewardItems != null && interactor.CompareTag("Player"))
        {
            foreach (var item in rewardItems)
            {
                interactor.GetComponent<PlayerInventory>().AddItem(item);
                //interactor.GetComponent<PlayerCharacter>().inventory.AddItem(item);
                rewardItems.Remove(item);
                animator.SetBool("open", true);
            }
        }
    }    
}
