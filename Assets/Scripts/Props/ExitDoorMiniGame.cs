using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorMiniGame : MonoBehaviour
{
    public GameObject pivot;
    public GameObject ExitDoor;

    public float rotation = 0.25f;

    public float progress = 0.0f;
    public float progressRate = -5f;

    public float timer = 0.0f;
    public float timerLimit = 5.0f;

    // Update is called once per frame
    void Update()
    {
        if (progress >= 99.5)
        {
            ExitDoor.GetComponent<ExitDoor>().FinishMiniGame();
        }

        if (progressRate < 0 && timer < timerLimit)
        {
            timer += 1 * Time.deltaTime;
        }
        else 
        {
            progress = Mathf.Clamp(progress + progressRate * Time.deltaTime, 0.0f, 100.0f);
        }

        //Debug.Log(pivot.transform.rotation.z);
        if (pivot.transform.rotation.z > 0.6f)
        {
            rotation = -0.25f;
        }
        if (pivot.transform.rotation.z < -0.6f)
        {
            rotation = 0.25f;
        }
        pivot.transform.Rotate(new Vector3(0, 0, rotation * (progress/25)));
    }

    public void changeProgressRate(float change)
    {
        progressRate = change;
    }

    
    
}
