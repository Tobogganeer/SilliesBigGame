using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobo.Util;

[CreateAssetMenu(menuName = "Scriptable Objects/Cursor Types")]
public class CursorTypes : ScriptableObject
{
    public SerializableDictionary<CursorType, CursorPreset> cursorPresets =
        new SerializableDictionary<CursorType, CursorPreset>();

    public void SetCursorType(CursorType cursor)
    {
        if (cursorPresets.TryGetValue(cursor, out CursorPreset preset))
            preset.SetAsActive();
        else
        {
            Debug.LogWarning("No cursor defined for CursorType." + cursor);
            cursorPresets[CursorType.Default].SetAsActive();
        }
    }

    [System.Serializable]
    public class CursorPreset
    {
        public Texture2D texture;
        public Vector2 hotspot;

        public void SetAsActive()
        {
            Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
        }
    }
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
