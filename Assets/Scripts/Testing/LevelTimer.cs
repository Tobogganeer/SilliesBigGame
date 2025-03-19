using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public static float levelTimer;
    public int timeStarted = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        levelTimer += Time.deltaTime * timeStarted;

        if (PauseMenu.IsPaused)
        {
            timeStarted = 0;
        }
        else
        {
            timeStarted = 1;
        }

        
    }
}
