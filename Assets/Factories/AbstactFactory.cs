using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstactFactory<T>
{    
    public abstract T Create(int id);
}
