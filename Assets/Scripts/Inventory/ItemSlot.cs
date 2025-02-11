using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int inventorySlot = 0;
    public string itemDataKey;
    public string itemDescription;
    public string itemCombination;
    public Image itemImageSlot;

    public ItemData ItemData;

    public GameObject itemPanel;

    void Start()
    {
        itemImageSlot = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();

        if (itemDataKey != string.Empty)
        {
            Search(itemDataKey);
        }
        else
        {
            itemImageSlot.color = new Vector4(255, 255, 255, 0);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = itemDataKey.ToString();

        itemPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = itemDescription.ToString();
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        itemPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = string.Empty;

        itemPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = string.Empty;
    }


    public void Search(string itemKey)
    {
        itemDescription = ItemData.itemData[itemKey]["itemDescription"];
        //itemDescription = ItemData.items[itemKey].description;

        if (itemImageSlot == null)
            itemImageSlot = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();

        itemImageSlot.color = Color.white;

        itemCombination = ItemData.itemData[itemKey]["combination"];

        itemImageSlot.sprite = Resources.Load<Sprite>(ItemData.itemData[itemKey]["itemImage"]);

    }

    public virtual void Clear()
    {
        if (itemImageSlot == null)
            itemImageSlot = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();

        itemImageSlot.sprite = null;
        itemCombination = string.Empty;
        itemImageSlot.color = new Vector4(255, 255, 255, 0);
        itemDataKey = string.Empty;
        itemDescription = string.Empty;
    }

    
}
