using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ExitDoor : MonoBehaviour, IInteractable
{
    public bool jammed = true;
    bool buttonPressed = false;
    bool miniGameStarted = false;
    public CustomMoveTrigger lockerZoomInTrigger;
    public CameraPosition originalPosition;
    public CameraDirection originalDirection;

    public TextInteractable dialog;
    public GameObject self;
    public GameObject miniGame;

    public BoxCollider moveTriggerBoxCollider;

    public Material door;
    public Material doorCrowbar;
    

    void IInteractable.OnClicked()
    {
        if (!jammed)
        {
            // roll credits
            SceneManager.LoadScene("EndScreen");
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
                self.GetComponent<MeshRenderer>().material = doorCrowbar;
                miniGame.SetActive(true);
            }
        }
    }

    public void Update()
    {
        if (moveTriggerBoxCollider.enabled)
        {
            self.GetComponent<MeshRenderer>().material = door;
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
        self.GetComponent<MeshRenderer>().material = door;
        miniGame.SetActive(false);
    }
}
