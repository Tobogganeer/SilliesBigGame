using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public CursorTypes cursors;

    Camera cam;
    IInteractable current;
    ICustomCursor currentCursor;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        CastRay();
        // Set the current cursor type if we are hovering over something
        if (currentCursor == null)
            cursors.SetCursorType(CursorType.Default);
        else
            cursors.SetCursorType(currentCursor.GetCursorType());
    }

    void CastRay()
    {
        GameObject hitObject = null;

        // Check if we are hovering over UI
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            // Returns null
            //hitObject = EventSystem.current.currentSelectedGameObject;
            if (UIInteractable.CurrentlyHovered != null)
                hitObject = UIInteractable.CurrentlyHovered.gameObject;
        }
        else
        {
            // Otherwise cast a ray into the scene
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
                hitObject = hit.collider.gameObject;
        }

        // Check for object
        if (hitObject != null)
        {
            IInteractable old = current;

            // Check if it's interactable
            if (hitObject.TryGetComponent(out current))
            {
                // Hovered over a new object
                if (old != current)
                {
                    old?.OnCursorExit();
                    current.OnCursorEnter();
                }

                // Check if we are tryna interact
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    // Do it
                    current.OnClicked();
                }

                current.OnCursorStay();
            }
            else NoInteractableFound();

            // Check if has a custom cursor
            if (!hitObject.TryGetComponent(out currentCursor))
                current = null;
        }
        else
        {
            // Hit nothing
            NoInteractableFound();
            currentCursor = null;
        }
    }

    void NoInteractableFound()
    {
        current?.OnCursorExit();
        current = null;
    }
}
