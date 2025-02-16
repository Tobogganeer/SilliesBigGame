using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour, IInteractable, ICustomCursor
{
    public GameObject CG;

    public void OnClicked()
    {
        if (Inventory.HasItem("Syringe")) CG.SetActive(enabled);
    }

    public CursorType GetCursorType()
    {
        return Inventory.HasItem("Syringe") ? CursorType.InteractHand : CursorType.Default;
    }
}
