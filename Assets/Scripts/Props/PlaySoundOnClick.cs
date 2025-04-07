using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobo.Audio;

public class PlaySoundOnClick : MonoBehaviour, IInteractable
{
    public Sound sound;

    public void OnClicked()
    {
        sound.MaybeNull().PlayAtPosition(transform.position);
    }
}
