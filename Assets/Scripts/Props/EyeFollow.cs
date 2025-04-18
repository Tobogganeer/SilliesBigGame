using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EyeFollow : MonoBehaviour
{
    public float multiplier = 0.01f;
    public float maxAmount = 0.01f;

    Vector3 startingLocalPosition;
    Transform parent;
    Camera cam;

    private void Start()
    {
        startingLocalPosition = transform.localPosition;
        cam = Camera.main;
        parent = transform.parent;
    }

    Vector3 GetStartingPosition() => parent.position + startingLocalPosition;

    private void Update()
    {
        Vector3 cursorScreenPos = Mouse.current.position.value;
        // Set z so the world position is accurate
        cursorScreenPos.z = Vector3.Distance(GetStartingPosition(), cam.transform.position);

        // Get vector from current position to cursor's position in world
        Vector3 cursorPos = cam.ScreenToWorldPoint(cursorScreenPos);
        Vector3 offset = cursorPos - GetStartingPosition();

        // Divide the offset (eyes move less towards cursor) and clamp (max travel)
        offset = Vector3.ClampMagnitude(offset * multiplier, maxAmount);

        // Apply offset
        transform.position = GetStartingPosition() + offset;
    }
}