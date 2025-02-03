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
    }

    void IInteractable.OnClicked()
    {
        OnClicked?.Invoke();
    }

    public void SetColliderEnabled(bool enabled) => coll.enabled = enabled;

}
