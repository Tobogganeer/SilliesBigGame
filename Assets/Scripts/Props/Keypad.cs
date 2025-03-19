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

    int incorrectKeycodeEntered = 0;

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
            Sound.KeypadGood.PlayDirect();
            TelemetryLogger.Log(this, "Locker puzzle solved. Amount of incorrect attempts: " + incorrectKeycodeEntered);
            onCorrectPasscodeEntered?.Invoke();
        }
        else
        {
            Sound.KeypadBad.PlayDirect();
            incorrectKeycodeEntered += 1;
            TelemetryLogger.Log(this, "Incorrect locker keypad code entered. Amount of incorrect attempts: " + incorrectKeycodeEntered);
            onWrongPasscodeEntered?.Invoke();
        }
        current.Clear();
        text.text = string.Join(' ', current);
    }
}
