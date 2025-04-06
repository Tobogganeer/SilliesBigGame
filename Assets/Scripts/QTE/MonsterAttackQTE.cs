using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterAttackQTE : MonoBehaviour
{
    public RectTransform crowbar;
    public RectTransform target;

    [Space]
    public GameObject self;
    public RectTransform image;
    public GameObject monster;

    [Space]
    public float movementSize = 150f;
    public float movementSpeed = 5f;

    float sin;
    float timer;

    private void OnEnable()
    {
        //PopUp.Show("Tutorial QTE: Insert the syringe into the patient's arm", 5f);
        crowbar.gameObject.SetActive(true);
        target.position = new Vector2(Random.Range(Screen.width / 2 + 50, Screen.width / 2 + 300), Random.Range(Screen.height / 2 + 50, Screen.height / 2 + 200));
        //target.position = new Vector2(Random.Range(50, 300), Random.Range(150, 350));
    }

    private void OnDisable()
    {
        crowbar.gameObject.SetActive(false);
        Cursor.visible = true;
    }

    private void Update()
    {
        // Make the cursor invisible if we aren't paused
        Cursor.visible = PauseMenu.IsPaused;

        // Check if we clicked target
        bool hoveringOverTarget = RectTransformUtility.RectangleContainsScreenPoint(target, crowbar.position);
        if (Mouse.current.leftButton.wasPressedThisFrame && hoveringOverTarget)
        {
            PopUp.Show("Monster Whacked", 3f);
            monster.GetComponent<Blackboard>().GetVariable("whacked").value = true;
            self.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        // crowbar movement
        timer += Time.deltaTime * movementSpeed;
        sin = Mathf.Sin(timer);

        Vector2 offset = new Vector2(sin * movementSize, 0);
        Vector2 shake = new Vector2(Random.Range(1, 5), Random.Range(1, 10));

        if (!PauseMenu.IsPaused)
            crowbar.position = Mouse.current.position.value + offset + shake;


        // monster image shake
        image.position = new Vector2(Screen.width/2 + Random.Range(1, 10), Screen.height/2 + Random.Range(1, 10));
    }
}
