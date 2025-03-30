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
    public Image itemImageSlot;

    public GameObject itemPanel;

    TextMeshProUGUI itemNameText;
    TextMeshProUGUI itemDescriptionText;

    public bool Empty => string.IsNullOrEmpty(itemDataKey);

    void Start()
    {
        itemImageSlot = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();

        UpdateGraphics();

        itemNameText = itemPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        itemDescriptionText = itemPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemNameText.text = itemDataKey.ToString();
        itemDescriptionText.text = itemDescription.ToString();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemNameText.text = string.Empty;
        itemDescriptionText.text = string.Empty;
    }


    public void UpdateGraphics()
    {
        if (string.IsNullOrEmpty(itemDataKey))
        {
            // Make item sprite clear
            itemImageSlot.color = new Vector4(255, 255, 255, 0);
            return;
        }

        itemDescription = ItemData.GetData(itemDataKey)["itemDescription"];

        // Make non-transparent
        itemImageSlot.color = Color.white;

        itemImageSlot.sprite = ItemSprites.Get(itemDataKey);

    }

    public virtual void Clear()
    {
        itemImageSlot.sprite = null;
        itemImageSlot.color = new Vector4(255, 255, 255, 0);
        itemDataKey = string.Empty;
        itemDescription = string.Empty;
    }

    
}
