using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInteractable : MonoBehaviour, IInteractable
{
    public string text;
    public float time = 3f;
    public bool oneTimeOnly = false;

    bool activated = false;

    public void OnClicked()
    {
        if (oneTimeOnly && activated)
            return;

        activated = true;
        PopUp.Show(text, time);
    }
}
