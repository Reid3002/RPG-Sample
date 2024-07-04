using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new item", menuName = "Data/Enemy Party")]
public class EnemyParty : ScriptableObject
{
    [SerializeField] EnemyCharacter[] Enemies;
    public EnemyCharacter[] enemies => Enemies;
}
