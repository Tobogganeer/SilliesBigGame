using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour, IInteractable
{
    public void OnClicked()
    {
        Destroy(gameObject);
    }
}
