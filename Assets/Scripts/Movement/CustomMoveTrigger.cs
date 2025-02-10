using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMoveTrigger : MonoBehaviour, IInteractable
{
    public event Action OnClicked;

    Collider coll;
    Collider GetCollider()
    {
        if (coll == null)
            coll = GetComponent<Collider>();
        return coll;
    }

    void IInteractable.OnClicked()
    {
        OnClicked?.Invoke();
    }

    public void SetColliderEnabled(bool enabled) => GetCollider().enabled = enabled;

    public void RemoveAllListeners() => OnClicked = null;
}
