using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Mode mode;
    Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    private void Update()
    {
        if (mode == Mode.FaceCamera)
            transform.LookAt(cam);
        else
            transform.forward = -cam.forward;
    }

    public enum Mode
    {
        FaceCamera,
        MatchCameraForwardVector
    }
}
