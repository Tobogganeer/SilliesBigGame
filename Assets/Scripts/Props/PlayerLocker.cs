using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocker : MonoBehaviour
{
    public CustomMoveTrigger lockerZoomInTrigger;

    [Space]
    public GameObject unlockedInteraction;

    public void OnCorrectCodeEntered()
    {
        // Go back to the main view
        PlayerMovement.instance.Travel(PlayerMovement.instance.PreviousPosRot.position, PlayerMovement.instance.PreviousPosRot.rotation);
        lockerZoomInTrigger.gameObject.SetActive(false); // We can never go back to locker zoomed in view
        unlockedInteraction.SetActive(true);

    }
}
