using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TutorialQTE : MonoBehaviour
{
    public RectTransform syringeObj;
    public RectTransform target;

    [Space]
    public GameObject cg;

    [Space]
    public UnityEvent onLevel2Unlocked;
    public Cutscene cutscene;
    public CameraPosition posAfterCutscene;
    public CameraDirection dirAfterCutscene;

    [Space]
    public float movementSize = 150f;
    public float movementSpeed = 5f;

    float sin;
    float timer;

    private void OnEnable()
    {
        //PopUp.Show("Tutorial QTE: Insert the syringe into the patient's arm", 5f);
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
            //PopUp.Show("Start cutscene now... (END OF DEMO)", 3f);
            cg.SetActive(false);
            onLevel2Unlocked?.Invoke();
            cutscene.Play(CutsceneFinished);
            Interactor.Enabled = false; // Disable interaction
            Inventory.ConsumeItem("Charged Flashlight");
            Inventory.ConsumeItem("Syringe");
        }
    }

    void CutsceneFinished()
    {
        Interactor.Enabled = true;
        // Move to level 3
        PlayerMovement.instance.Travel(posAfterCutscene, dirAfterCutscene, null, 0, 0, true);
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
