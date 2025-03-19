using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using UnityEngine;
using UnityEngine.InputSystem;

public class BadTimeButton : MonoBehaviour
{
    public UnityEngine.InputSystem.Key badTimeKey = UnityEngine.InputSystem.Key.Enter;

    private void Update()
    {
        if (Keyboard.current[badTimeKey].wasPressedThisFrame)
        {
            PopUp.Show("Bad time!", 1f);
            Sound.KeypadBad.Override().SetVolume(0.5f).Play();
            Telemetry.BadTimeButtonPressed();
        }
    }
}
