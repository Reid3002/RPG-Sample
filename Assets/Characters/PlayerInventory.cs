using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PlayerInventory: MonoBehaviour
{
    [SerializeField] private List<Item> inventory;
    [SerializeField]private int maxSize = 10;
    public int MaxSize { get { return maxSize; } }

    //public PlayerInventory(int size)
    //{
    //    inventory = new List<Item>();
    //    maxSize = size;
    //}

    public List<Item> GetInventory()
    {
        return inventory;
    }

    public void AddItem(Item item)
    {
        inventory.Add(item);        
    }

    public void Discard(Item item)
    {
        inventory.Remove(item);
    }
}
