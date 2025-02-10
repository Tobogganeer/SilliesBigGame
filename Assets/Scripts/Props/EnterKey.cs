using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using UnityEngine;

public class EnterKey : MonoBehaviour, IInteractable
{
    public Keypad kp;

    public void OnClicked()
    {
        kp.EnterPressed();
    }
}
