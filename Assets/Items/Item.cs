using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField] string p_name;
    public string name { get { return p_name; } }

    [SerializeField] Sprite p_sprite;
    public Sprite sprite { get { return p_sprite; } }


    [SerializeField] string p_id;
    public string id { get { return p_id; } }

    [SerializeField] itemType ItemType;
    public itemType type { get { return ItemType; } }

    [SerializeField] public enum itemType
    {
        Consumable,
        Equipment,
        KeyItem
    }

    [SerializeField] string description;
    public string Description { get { return description; } }
        
}
