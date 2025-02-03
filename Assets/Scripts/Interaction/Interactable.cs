using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void OnCursorEnter() { }
    public void OnCursorStay() { }
    public void OnCursorExit() { }

    public void OnClicked();
}

public interface IChangeCursor
{
    public CursorType GetCursorType();
}

