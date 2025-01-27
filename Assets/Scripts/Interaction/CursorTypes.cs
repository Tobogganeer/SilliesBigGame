using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Cursor Types")]
public class CursorTypes : ScriptableObject
{
    
}

public enum CursorType
{
    Default,
    InteractHand,
    LeftArrow,
    RightArrow,
    DownArrow,
    UpArrow,
    Lock
}
