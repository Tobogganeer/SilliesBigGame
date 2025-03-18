using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionDisplay : MonoBehaviour
{
    public TMPro.TMP_Text sessionIDText;

    public void OnConnected(int sessionID)
    {
        PopUp.Show("Session #" + sessionID);
        sessionIDText.text = "Session #" + sessionID;
    }

    public void OnConnectionFailed(string reason)
    {
        PopUp.Show("Telemetry connection failed: " + reason);
        sessionIDText.text = "Telemetry connection failed: " + reason;
    }
}
