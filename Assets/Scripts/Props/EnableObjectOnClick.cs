using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectOnClick : MonoBehaviour, IInteractable
{
    public GameObject obj;
    public bool enable = true;

    public void OnClicked() => obj.SetActive(enable);
}
