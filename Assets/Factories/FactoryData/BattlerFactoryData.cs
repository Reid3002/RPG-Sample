using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item", menuName = "Data/BattlerFactoryData")]
public class BattlerFactoryData : ScriptableObject
{
    [SerializeField] List<ClassTemplate> ClassData;
    public List<ClassTemplate> classData { get { return ClassData; } }
}
