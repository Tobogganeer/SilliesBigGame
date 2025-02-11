using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocker : MonoBehaviour
{
    public CustomMoveTrigger lockerZoomInTrigger;
    public CameraPosition originalPosition;
    public CameraDirection originalDirection;

    [Space]
    public GameObject unlockedInteraction;

    public void OnCorrectCodeEntered()
    {
        // Go back to the main view
        PlayerMovement.instance.Travel(originalPosition, originalDirection);
        lockerZoomInTrigger.gameObject.SetActive(false); // We can never go back to locker zoomed in view
        unlockedInteraction.SetActive(true);

    }
}
