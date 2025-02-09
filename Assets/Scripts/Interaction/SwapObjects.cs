using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SwapObjects : MonoBehaviour, IInteractable
{
    public GameObject[] objectsToSwapBetween;

    private void Start()
    {
        // TODO: Make it cycle through more later (lazy rn)
        if (objectsToSwapBetween.Length != 2)
            throw new ArgumentOutOfRangeException(nameof(objectsToSwapBetween), "SwapObjects should have exactly 2 states");

        objectsToSwapBetween[1].SetActive(false);
    }

    public void OnClicked()
    {
        objectsToSwapBetween[0].SetActive(objectsToSwapBetween[1].activeSelf); // Set first to second's current state
        objectsToSwapBetween[1].SetActive(!objectsToSwapBetween[1].activeSelf); // Flip second's state
    }
}
