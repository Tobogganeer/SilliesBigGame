using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobo.Util;
using UnityEngine.UI;

public class MovementUI : MonoBehaviour
{
    public static MovementUI instance;
    private void Awake()
    {
        instance = this;
    }

    public SerializableDictionary<MoveDirection, Button> buttons;
    List<CustomMoveTrigger> triggers = new List<CustomMoveTrigger>();

    public static void SetUI(CameraRotation rotation)
    {
        instance.DisableAll();

        if (rotation == null)
            return;

        foreach (CameraTransition trans in rotation.transitions)
        {
            if (instance.buttons.TryGetValue(trans.directionToClick, out Button button))
            {
                button.gameObject.SetActive(true);
                button.onClick.AddListener(() => PlayerMovement.instance.Travel(trans));
            }
            else if (trans.directionToClick == MoveDirection.Custom && trans.moveTrigger != null)
            {
                // Enable this custom trigger
                instance.triggers.Add(trans.moveTrigger);
                trans.moveTrigger.SetColliderEnabled(true);
                trans.moveTrigger.OnClicked += () => PlayerMovement.instance.Travel(trans);
            }
        }
    }

    public static void SetUI(PosRot currentPosRot)
    {
        if (!CameraPosition.TryGetRotation(currentPosRot, out CameraRotation rotation))
        {
            // TODO: Go back to known safe position?
            //Debug.LogError("Failed to get transitions from " + currentPosRot + "! (we need a fallback for this)");
            // EDIT: Hiding spots have to transitions/rotations - just silently accept the error (it will disable all UI)
            return;
        }

        SetUI(rotation);
    }

    public static void ClearUI() => SetUI(null);

    public void DisableAll()
    {
        foreach (Button button in buttons.dictionary.Values)
        {
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
        }

        foreach (CustomMoveTrigger trigger in triggers)
        {
            trigger.SetColliderEnabled(false);
            trigger.RemoveAllListeners();
        }

        triggers.Clear();
    }
}
