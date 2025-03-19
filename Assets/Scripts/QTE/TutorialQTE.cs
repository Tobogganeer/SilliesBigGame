using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialQTE : MonoBehaviour
{
    public RectTransform syringeObj;
    public RectTransform target;

    [Space]
    public GameObject cg;

    [Space]
    public float movementSize = 150f;
    public float movementSpeed = 5f;

    float sin;
    float timer;

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

        // Check if we clicked target
        bool hoveringOverTarget = RectTransformUtility.RectangleContainsScreenPoint(target, syringeObj.position);
        if (Mouse.current.leftButton.wasPressedThisFrame && hoveringOverTarget)
        {
            PopUp.Show("Start cutscene now... (END OF DEMO)", 3f);
            TelemetryLogger.Log(this, "Level 1 Completed - QTE hit", "Time: " + LevelTimer.levelTimer);
            cg.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        //PopUp.Show(RectTransformUtility.RectangleContainsScreenPoint(target, syringeObj.position).ToString());
        timer += Time.deltaTime * movementSpeed;
        sin = Mathf.Sin(timer);

        Vector2 offset = new Vector2(sin * movementSize, 0);

        if (!PauseMenu.IsPaused)
            syringeObj.position = Mouse.current.position.value + offset;
    }
}
