using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitDoor : MonoBehaviour, IInteractable
{
    public bool jammed = true;
    bool buttonPressed = false;
    bool miniGameStarted = false;
    public CustomMoveTrigger lockerZoomInTrigger;
    public CameraPosition originalPosition;
    public CameraDirection originalDirection;

    public TextInteractable dialog;
    public GameObject crowbar;
    public GameObject miniGame;

    public BoxCollider moveTriggerBoxCollider;
    

    void IInteractable.OnClicked()
    {
        if (!jammed)
        {
            // roll credits
            return;
        }
        if (!buttonPressed)
        {
            dialog.text = "Won't open unless I press the button...";
            dialog.OnClicked();
        }
        else
        {
            if (!miniGameStarted)
            {
                miniGameStarted = true;
                crowbar.SetActive(true);
                miniGame.SetActive(true);
            }
        }
    }

    public void Update()
    {
        if (moveTriggerBoxCollider.enabled)
        {
            crowbar.SetActive(false);
            miniGame.SetActive(false);
        }
    }


    public void buttonPress()
    {
        Debug.Log("pressed");
        if (!buttonPressed)
        {
            dialog.text = "Shit... the door is jammed.";
            dialog.OnClicked();
            buttonPressed = true;
        }
        
    }

    public void FinishMiniGame()
    {
        jammed = false;
        crowbar.SetActive(false);
        miniGame.SetActive(false);
    }
}
