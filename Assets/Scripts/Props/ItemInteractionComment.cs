using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractionComment : MonoBehaviour, IInteractable
{
    public string whenPlayerIsHolding;
    public string say;
    public float forSeconds = 3f;

    public void OnClicked()
    {
        if (Inventory.CurrentlyHeldItem == whenPlayerIsHolding)
            PopUp.Show(say, forSeconds);
    }
}
