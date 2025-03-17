using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialQTE : MonoBehaviour
{
    public RectTransform syringeObj;

    private void OnEnable()
    {
        PopUp.Show("Tutorial QTE: Insert the syringe into the patient's arm", 5f);
        syringeObj.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        syringeObj.gameObject.SetActive(false);
        Cursor.visible = true;
    }

    private void Update()
    {
        // Make the cursor invisible if we aren't paused
        Cursor.visible = PauseMenu.IsPaused;
    }

    private void LateUpdate()
    {
        if (!PauseMenu.IsPaused)
            syringeObj.position = Mouse.current.position.value;
    }
}
