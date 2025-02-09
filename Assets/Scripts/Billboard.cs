using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Mode mode;
    public Vector3 rotationOffset;
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
        transform.Rotate(rotationOffset, Space.Self);
    }

    public enum Mode
    {
        FaceCamera,
        MatchCameraForwardVector
    }
}
