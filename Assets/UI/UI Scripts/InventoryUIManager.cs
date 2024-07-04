using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] Sprite emptySlot;
    [SerializeField] Sprite errorImage;
    [SerializeField] GameObject itemSelector;
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] GameObject characterPanel;
    [SerializeField] GameObject itemInfoPanel;

    [SerializeField] GameObject[] slots;
    [SerializeField] GameObject[] characterInfo = new GameObject[5];

    private int itemIndex = 0;
    public int GetItemIndex() { return itemIndex; }

    private bool itemSelectorMovement = true;

    // Start is called before the first frame update
    void Start()
    {       
       characterPanel.SetActive(false);
       for(int i = 0; i < characterInfo.Length;i++)
        {
            if (i > 0)
            {
                characterInfo[i].gameObject.transform.position = new Vector3(characterInfo[i].gameObject.transform.position.x + 150, characterInfo[i].gameObject.transform.position.y, characterInfo[i].gameObject.transform.position.z);
            }            
        }
        //playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().inventory;
        //slots = new GameObject[playerInventory.MaxSize];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInventorySlots();
        ItemSelectorMovement(itemSelectorMovement);        

        if (Input.GetKeyDown(KeyCode.Return) && characterPanel.activeSelf == false && slots[itemIndex] != null)
        {
            SelectItem();            
        }

        if (itemInfoPanel.activeSelf || characterPanel.activeSelf)
        {
            itemSelectorMovement = false;
        }
        else
        {
            itemSelectorMovement = true;
        }

        if (characterPanel.activeSelf==true && Input.GetKey(KeyCode.Escape))
        {            
            characterPanel.SetActive(false);
            itemSelectorMovement = true;
        }


    }

    public void UpdateInventorySlots()
    {
        if (playerInventory != null)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < playerInventory.GetInventory().Count && playerInventory.GetInventory()[i] != null)
                {
                    slots[i].gameObject.GetComponent<UnityEngine.UI.Image>().sprite = playerInventory.GetInventory()[i].sprite;
                }
                else
                {
                    slots[i].gameObject.GetComponent<UnityEngine.UI.Image>().sprite = emptySlot;
                }
            }
        }
        else { Debug.LogError("InventoryUIMnager: Inventory not found"); }
    }

    private void ItemSelectorMovement(bool active)
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (itemIndex > 0)
                {
                    itemIndex--;
                }
                else
                {
                    itemIndex = slots.Length - 1;
                }

            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (itemIndex < slots.Length - 1)
                {
                    itemIndex++;
                }
                else
                {
                    itemIndex = 0;
                }

            }

            itemSelector.transform.position = slots[itemIndex].transform.position;
        }        
    }

    private void SelectItem()
    {
        Item selectedItem = null;
        if (playerInventory.GetInventory()[itemIndex] != null)
        {            
            itemSelectorMovement = false;
            selectedItem = playerInventory.GetInventory()[itemIndex];

            itemInfoPanel.GetComponent<ItemInfo>().item = selectedItem;
            itemInfoPanel.SetActive(true);          

        }       
    }    
}
