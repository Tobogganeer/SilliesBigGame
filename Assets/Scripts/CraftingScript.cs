using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingScript : MonoBehaviour
{

    public GameObject Craftingslot1;
    public GameObject Craftingslot2;

    public GameObject ResultSlot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Craftingslot1.GetComponent<ItemSlot>().itemDataKey != string.Empty && Craftingslot1.GetComponent<ItemSlot>().itemDataKey != string.Empty)
        {

        }
    }
}
