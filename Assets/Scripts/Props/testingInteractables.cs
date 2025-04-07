using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pHStrips : MonoBehaviour
{

    public GameObject newObj;
    // Update is called once per frame
    void Update()
    {
        if (Inventory.HasItem("Solution 6"))
        {
            gameObject.GetComponent<EnableObjectOnClick>().obj = newObj;
        }
    }
}
