using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomMoveTrigger : MonoBehaviour, IInteractable
{
    public event Action OnClicked;

    Collider coll;
    private void Awake()
    {
        coll = GetComponent<Collider>();
        SetColliderEnabled(false);
    }

    void IInteractable.OnClicked()
    {
        OnClicked?.Invoke();
    }

    public void SetColliderEnabled(bool enabled) => coll.enabled = enabled;

    public void RemoveAllListeners() => OnClicked = null;
}
