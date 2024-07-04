using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlerFactory : AbstactFactory<GameObject>
{    
    private Dictionary<int, GameObject> BattlerDictionary;

    public BattlerFactory (BattlerFactoryData data)
    {
        List<ClassTemplate> orderedData = new List<ClassTemplate>();
        orderedData = data.classData;
        BattlerDictionary = new Dictionary<int, GameObject>();
        foreach (var item in orderedData)
        {
            BattlerDictionary.Add(item.id, item.battler);
        }
    }

    public override GameObject Create(int id)
    {
       return GameObject.Instantiate(BattlerDictionary[id], Vector3.zero, Quaternion.identity);
    }
}
