using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Tobo.Audio;

public class Keypad : MonoBehaviour
{
    public TMP_Text text;

    public List<int> correctOrder = new List<int>();

    List<int> current = new List<int>();

    public void KeyPressed(int value)
    {
        if (current.Count == correctOrder.Count)
        {
            if (current.SequenceEqual(correctOrder))
            {
                Sound.KeypadGood.Play2D();
            }
            else
                Sound.KeypadBad.Play2D();
            current.Clear();
        }
        // Add value after so player can see full input before "submitting"
        else
            current.Add(value);



        // Display current numbers
        text.text = string.Join(' ', current);
    }
}
