using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionDisplay : MonoBehaviour
{
    public void OnConnected(int sessionID)
    {
        PopUp.Show("Session #" + sessionID);
    }

    public void OnConnectionFailed(string reason)
    {
        PopUp.Show("Telemetry connection failed: " + reason);
    }
}
