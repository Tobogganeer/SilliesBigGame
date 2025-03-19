using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public float levelTimer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        levelTimer += Time.deltaTime;

        if (PauseMenu.IsPaused)
        {
            TelemetryLogger.Log(this, "Game Paused - Time in Seconds:" + levelTimer);
        }

        
    }
}
