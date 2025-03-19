using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocker : MonoBehaviour
{
    public CustomMoveTrigger lockerZoomInTrigger;
    public CameraPosition originalPosition;
    public CameraDirection originalDirection;
    int incorrectKeycodeEntered = 0;

    [Space]
    public GameObject unlockedInteraction;

    public void OnCorrectCodeEntered()
    {
        // Go back to the main view
        PlayerMovement.instance.Travel(originalPosition, originalDirection);
        lockerZoomInTrigger.gameObject.SetActive(false); // We can never go back to locker zoomed in view
        TelemetryLogger.Log(this, "Locker puzzle solved. Amount of incorrect attempts: "+incorrectKeycodeEntered);
        unlockedInteraction.SetActive(true);

    }

    public void OnWrongCodeEntered()
    {
        incorrectKeycodeEntered += 1;
        TelemetryLogger.Log(this, "Incorrect locker keypad code entered. Amount of incorrect attempts: "+incorrectKeycodeEntered);
        
    }
}
