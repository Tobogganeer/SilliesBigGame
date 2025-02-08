using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobo.Util;

public class MovementUI : MonoBehaviour
{
    public static MovementUI instance;
    private void Awake()
    {
        instance = this;
    }

    public SerializableDictionary<CameraPosition.MoveDirection, GameObject> buttons;

    public static void SetUI(CameraPosition.CameraRotation rotation)
    {
        instance.SetAllActive(false);
        foreach (CameraPosition.Transition trans in rotation.transitions)
        {
            if (instance.buttons.TryGetValue(trans.directionToClick, out GameObject button))
                button.SetActive(true);
        }
    }

    public void SetAllActive(bool enabled)
    {
        foreach (GameObject button in buttons.dictionary.Values)
        {
            button.SetActive(enabled);
        }
    }
}
