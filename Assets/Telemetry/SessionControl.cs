using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class SessionControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnConnectionSuccess(int sessionID)
    {
        var displayField = GetComponent<TextMeshProUGUI>();
        if(sessionID < 0)
        {
            displayField.text = $"Logging locally (Session {sessionID})";
        }
        else
        {
            displayField.text = $"Connected to Server (Session {sessionID})";
        }
    }

    public void OnConnectionFail(string errorMessage)
    {
        var displayField = GetComponent<TextMeshProUGUI>();

        displayField.text = $"Failed to Connect to Server";
    }
}
