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
    public SpriteRenderer itemImageSlot;

    public ItemData ItemData;

    public GameObject itemPanel;

    void Start()
    {


        if (itemDataKey != null)
        {
            Search(itemDataKey);
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

        print(ItemData.itemData[itemKey]["itemImage"]);

        itemImageSlot.sprite = Resources.Load<Sprite>(ItemData.itemData[itemKey]["itemImage"]);
    }
}
