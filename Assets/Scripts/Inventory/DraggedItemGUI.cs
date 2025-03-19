using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using UnityEngine;
using UnityEngine.InputSystem;

public class DraggedItemGUI : MonoBehaviour
{
    public UnityEngine.UI.Image draggedItemImage;

    private void Start()
    {
        draggedItemImage.enabled = false;
    }

    private void LateUpdate()
    {
        if (draggedItemImage.enabled)
            draggedItemImage.rectTransform.position = Mouse.current.position.value;// + dragOffset;
    }

    public void Enable(Sprite sprite)
    {
        //TelemetryLogger.Log(this, "Picked up inside inventory"); // This logs everytime you pick up an item in the inventory

        draggedItemImage.sprite = sprite;
        draggedItemImage.enabled = true;
    }

    public void Disable()
    {
        //TelemetryLogger.Log(this, "Put down inside inventory"); // This logs everytime you click off an item in the inventory

        draggedItemImage.enabled = false;
    }
}
