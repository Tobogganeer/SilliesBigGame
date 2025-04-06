using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChemCabinetKeypad : MonoBehaviour
{
    public Keypad keypad;
    public List<int> passwordWeTellThemToEnter;

    bool hasInputGivenPassword;

    int incorrectKeycodeEntered = 0;

    private void Start()
    {
        keypad.onWrongPasscodeEntered.AddListener(WrongPass);
    }

    void WrongPass()
    {
        if (hasInputGivenPassword)
            return;

        // They input the old password
        if (passwordWeTellThemToEnter.SequenceEqual(keypad.Current))
        {
            hasInputGivenPassword = true;
            PopUp.Show("Management must've done that monthly password change again and didn't tell me...", 5f);
            keypad.onWrongPasscodeEntered.RemoveListener(WrongPass);
        }
        else
        {
            // Re-tell them if they are silly and forgot
            PopUp.Show("It was " + string.Join("", passwordWeTellThemToEnter));
        }
    }

    public void WongPassAgain()
    {
        PopUp.Show("Management must've done that monthly password change again and didn't tell me...", 5f);
        TelemetryLogger.Log(this, "Incorrect locker keypad code entered. Amount of incorrect attempts: " + incorrectKeycodeEntered);
    }

}
