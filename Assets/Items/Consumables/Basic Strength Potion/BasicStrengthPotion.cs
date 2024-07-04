//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BasicStrengthPotion : MonoBehaviour, IConsumable
//{
//    private float potionDuration;
//    private void Update()
//    {
//        if (potionDuration > 0)
//        {
//            potionDuration -= Time.deltaTime;
//        }
//        if (potionDuration <= 0)
//        {
//            //RemoveEffect(this.gameObject.GetComponent<Character>());            
//        }
//    }

//    public void Consume(GameObject target)
//    {
//        target.TryGetComponent<Character>(out Character character);
//        {
//            //character.gameObject.AddComponent<BasicStrengthPotion>();
//            SpecialEffect(character);

//            character.activePotion = GetId();

//            potionDuration = effectDuration(60);

//        }

//    }

//    public void RemoveEffect(Character target)
//    {
//        target.TryGetComponent<Character>(out Character character);
//        {
//            character.Atk -= 5;
//            Destroy(this);
//        }
//    }

//    public void SpecialEffect(Character target)
//    {
//        target.Atk += 5;
//    }
//    public string GetId() { return "002"/*id*/; }
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
