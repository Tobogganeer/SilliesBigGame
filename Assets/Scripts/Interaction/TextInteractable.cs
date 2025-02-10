using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInteractable : MonoBehaviour, IInteractable
{
    public string text;
    public float time = 3f;

    public void OnClicked()
    {
        PopUp.Show(text, time);
    }
}
