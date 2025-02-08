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

    public SerializableDictionary<CameraPosition.MoveDirection, Button> buttons;
    List<CustomMoveTrigger> triggers = new List<CustomMoveTrigger>();

    public static void SetUI(CameraPosition.CameraRotation rotation)
    {
        instance.DisableAll();

        if (rotation == null)
            return;

        foreach (CameraPosition.Transition trans in rotation.transitions)
        {
            if (instance.buttons.TryGetValue(trans.directionToClick, out Button button))
            {
                button.gameObject.SetActive(true);
                button.onClick.AddListener(() => PlayerMovement.instance.Travel(trans));
            }
            else if (trans.directionToClick == CameraPosition.MoveDirection.Custom && trans.moveTrigger != null)
            {
                // Enable this custom trigger
                instance.triggers.Add(trans.moveTrigger);
                trans.moveTrigger.SetColliderEnabled(true);
                trans.moveTrigger.OnClicked += () => PlayerMovement.instance.Travel(trans);
            }
        }
    }

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
