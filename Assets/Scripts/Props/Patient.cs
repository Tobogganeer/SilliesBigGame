using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour, IInteractable, ICustomCursor
{
    public void OnClicked()
    {
        if (Inventory.HasItem("Syringe"))
        {
            PopUp.Show("End of milestone gameplay");
        }
    }

    public CursorType GetCursorType()
    {
        return Inventory.HasItem("Syringe") ? CursorType.InteractHand : CursorType.Default;
    }
}
