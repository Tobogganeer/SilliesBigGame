using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInteractable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //public CursorType cursorType = CursorType.InteractHand;

    public static UIInteractable CurrentlyHovered;

    public void OnPointerEnter(PointerEventData eventData)
    {
        CurrentlyHovered = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Only set to null if we are still hovered
        if (CurrentlyHovered == this)
            CurrentlyHovered = null;
    }

    public void OnClicked()
    {
        // Does nothing
    }

    /*
    CursorType IInteractable.GetCursorType()
    {
        return cursorType;
    }
    */
}
