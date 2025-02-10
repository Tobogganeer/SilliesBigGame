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

    bool isEnabled;
    private void Awake()
    {
        // This way, if we try to enable/disable the collider before the object is enabled (starts disabled),
        // we still maintain the correct state (off by default)
        GetCollider().enabled = isEnabled;
    }

    void IInteractable.OnClicked()
    {
        OnClicked?.Invoke();
    }

    public void SetColliderEnabled(bool enabled)
    {
        isEnabled = enabled;
        GetCollider().enabled = enabled;
    }


    public void RemoveAllListeners() => OnClicked = null;
}
