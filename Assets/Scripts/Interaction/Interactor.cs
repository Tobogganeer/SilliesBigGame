using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Texture2D defaultCursor, interactCursor;

    public Vector2 defaultCursorHotspot = new Vector2(0f, 0f);
    // Interact cursor might be centered on the middle of the sprite
    public Vector2 interactCursorHotspot = new Vector2(16f, 16f);

    Camera cam;
    IInteractable current;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        CastRay();
        Texture2D cursor = current == null ? defaultCursor : interactCursor;
        Vector2 hotspot = current == null ? Vector2.zero : new Vector2(16f, 16f);
        Cursor.SetCursor(cursor, hotspot, CursorMode.Auto);
    }

    void CastRay()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // Check for object
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Check if it's interactable
            if (hit.collider.TryGetComponent(out current))
            {
                // Check if we are tryna interact
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    // Do it
                    current.OnClicked();
                }
            }
            else current = null;
        }
        else current = null;
    }
}
