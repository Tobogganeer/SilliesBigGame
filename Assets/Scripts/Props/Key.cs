using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public Keypad kp;
    public int value;

    public void OnClicked()
    {
        kp.KeyPressed(value);
        Sound.KeypadPress.PlayDirect();
    }
}
