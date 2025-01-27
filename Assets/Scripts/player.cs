using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{

    public GameObject Camera;
    public GameObject InventoryPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void InventoryToggle()
    {
        InventoryPanel.SetActive(!InventoryPanel.activeInHierarchy);
    }
}
