//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BasicHpPotion : MonoBehaviour, IConsumable
//{
//    public void Consume(GameObject target)
//    {
//        target.TryGetComponent<Character>(out Character character);
//        {            
//            SpecialEffect(character);           

//            Destroy(this);

//        }

//    }

//    public void RemoveEffect(Character target) { }

//    public void SpecialEffect(Character target)
//    {
//        target.HP += 10;
//    }
//    public string GetId() { return "001"/*id*/; }
//    public bool isEffectActive(Character target)
//    {
//        if (target.activePotion == GetId())
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }
//    public float effectDuration(float seconds)
//    {
//        return seconds;
//    }
//}
