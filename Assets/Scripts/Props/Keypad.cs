using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Tobo.Audio;
using UnityEngine.Events;

public class Keypad : MonoBehaviour
{
    public TMP_Text text;

    public List<int> correctOrder = new List<int>();

    public UnityEvent onCorrectPasscodeEntered;
    public UnityEvent onWrongPasscodeEntered;

    List<int> current = new List<int>();

    public List<int> Current => current;

    public void KeyPressed(int value)
    {
        if (current.Count == correctOrder.Count)
            EnterPressed();
        // Add value after so player can see full input before "submitting"
        else
            current.Add(value);

        // Display current numbers
        text.text = string.Join(' ', current);
    }

    public void EnterPressed()
    {
        if (current.SequenceEqual(correctOrder))
        {
            Sound.KeypadGood.Play2D();
            onCorrectPasscodeEntered?.Invoke();
        }
        else
        {
            Sound.KeypadBad.Play2D();
            onWrongPasscodeEntered?.Invoke();
        }
        current.Clear();
        text.text = string.Join(' ', current);
    }
}
