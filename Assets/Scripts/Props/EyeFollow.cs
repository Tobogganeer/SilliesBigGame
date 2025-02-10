using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class EyeFollow : MonoBehaviour
{
    public float amount = 0.00001f;
    public Vector3 mask = new Vector3(1, 1, 0);
    public float delay = 0.2f;

    Vector3 startingPosition;
    public Vector3 position;

    private IEnumerator Start()
    {
        startingPosition = transform.localPosition;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            position = Mouse.current.position.ReadValue() * amount;

            transform.localPosition = startingPosition + Vector3.Scale(position, mask);
            yield return wait;
        }
    }
}
