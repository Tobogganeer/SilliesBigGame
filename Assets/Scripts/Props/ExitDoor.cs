using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ExitDoor : MonoBehaviour, IInteractable
{
    bool minigameComplete = false;
    bool unlockButtonPressed = false;
    bool miniGameStarted = false;

    public GameObject self;
    public GameObject miniGame;

    [Space]
    public CameraPosition pos;
    public CameraRotation rot;

    [Space]
    public GameObject normalDoor;
    public GameObject crowbarDoor;
    

    void IInteractable.OnClicked()
    {
        if (minigameComplete)
        {
            // roll credits
            SceneManager.LoadScene("EndScreen");
            return;
        }

        if (!unlockButtonPressed)
        {
            PopUp.Show("It won't budge!", 1.5f);
            Sound.ElectricBuzz.PlayAtPosition(transform.position);
        }
        else
        {
            if (!miniGameStarted)
            {
                // Show crowbar sprite
                ShowDoor(crowbar: true);
                // Start minigam
                miniGameStarted = true;
                miniGame.SetActive(true);
            }
        }
    }

    public void Update()
    {
        PosRot player = PlayerMovement.instance.CurrentPosRot;
        // Check if player has moved or looked away
        if (player.position != pos.position || player.rotation != Quaternion.LookRotation(rot.facing.GetOffset()))
        {
            ShowDoor(crowbar: false);
            miniGame.SetActive(false);
            miniGameStarted = false;
        }
    }


    public void UnlockButtonPressed()
    {
        unlockButtonPressed = true;
        /*
        if (!unlockButtonPressed)
        {
            PopUp.Show("Shit... the door is jammed.");
            unlockButtonPressed = true;
        }
        */
    }

    public void FinishMiniGame()
    {
        minigameComplete = true;
        ShowDoor(crowbar: false);
        miniGame.SetActive(false);
    }

    void ShowDoor(bool crowbar)
    {
        crowbarDoor.SetActive(crowbar);
        normalDoor.SetActive(!crowbar);
    }
}
