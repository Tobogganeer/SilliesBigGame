using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobo.Util;

public class MoveCommand : MonoBehaviour
{
    public static MoveCommand instance;
    private void Awake()
    {
        instance = this;
    }

    // Just store camera positions with a string so we have autocomplete
    public SerializableDictionary<string, CameraPosition> cameras; 
}
