using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumable
{
    void Consume(GameObject target);
    void RemoveEffect(Character target);
    void SpecialEffect(Character target);
    bool isEffectActive(Character character);
    float effectDuration(float seconds);
}
