using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCClose : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Destroy(this.gameObject);
        }
    }
}
