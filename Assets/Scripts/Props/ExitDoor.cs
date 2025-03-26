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


    void IInteractable.OnClicked()
    {
        if (!buttonPressed)
        {
            dialog.text = "Won't open unless I press the button...";
            dialog.OnClicked();
        }
        else
        {
            StartMiniGame();
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

    public void StartMiniGame()
    {
        miniGameStarted = true;
        crowbar.SetActive(true);
    }
}
