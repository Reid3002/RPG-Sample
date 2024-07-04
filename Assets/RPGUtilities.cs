using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RPGUtilities
{
    
    public static int CalculateStatByLevel(int baseStat, int currentLvl, float modifier)
    {
       int result = Mathf.RoundToInt(baseStat* modifier);
        if(currentLvl > 0)
        {
            currentLvl--;
            CalculateStatByLevel(result, currentLvl, modifier);
            
        }
        else
        {
            return result;
        }

        return default;

    }    

}
