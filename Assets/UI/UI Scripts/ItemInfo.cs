using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour, IOptions
{
    [SerializeField] Text itemName;
    [SerializeField] Text itemDescription;
    [SerializeField] Text itemStats;
    [SerializeField] Text itemInteraction;
    [SerializeField] Image itemSprite;
    [SerializeField] GameObject characterSelector;
    [SerializeField] PlayerInventory inventory;
    public Selector selector;
    [HideInInspector]public Item item;
    private int itemIndex;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        selector.canMove = true;
        itemName.text = item.name;
        itemDescription.text = item.Description;
        itemSprite.sprite = item.sprite;

        if (item.type.ToString() == "Equipment")
        {
            itemInteraction.text = "Equip";
            itemStats.text = BuildStatsText(item);
        }
        else if (item.type.ToString() == "Consumable")
        {
            itemInteraction.text = "Use";
        }
    }

    private void OnDisable()
    {
        selector.ResetIndex();
        selector.canMove = false;
    }

    private string BuildStatsText(Item selectedItem)
    {
        Equipment equipment = selectedItem as Equipment;
        string atk = "Atk: "+equipment.Atk.ToString();
        string def = "Def: "+equipment.Def.ToString();
        string magic = "Magic: "+equipment.Magic.ToString();
        string speed = "Speed: "+equipment.Speed.ToString();
        string critR = "Crit Rate: " + equipment.CritR.ToString() + "%";

        return atk + "\n" + def + "\n" + magic + "\n" + speed + "\n" + critR;
    }

    public void OptionSelected(int option)
    {
        switch (option)
        {
            case 0:
                characterSelector.SetActive(true);
                characterSelector.GetComponent<InvTargetSelect>().item = item;
                this.gameObject.SetActive(false);
                break;
            case 1:
                inventory.Discard(item);
                this.gameObject.SetActive(false);

                break;
            case 2:
                this.gameObject.SetActive(false);                
                break;
        }
    }
}
