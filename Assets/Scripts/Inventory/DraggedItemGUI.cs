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
        draggedItemImage.sprite = sprite;
        draggedItemImage.enabled = true;
    }

    public void Disable()
    {
        draggedItemImage.enabled = false;
    }
}
