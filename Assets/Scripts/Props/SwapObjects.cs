using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Tobo.Audio;

public class SwapObjects : MonoBehaviour, IInteractable
{
    public GameObject[] objectsToSwapBetween;
    public Sound soundGoingToSecondState;
    public Sound soundGoingToFirstState;

    [HideInInspector]
    public bool inDefaultState;

    public Func<SwapObjects, bool> onTrySwap = (s) => true;

    private void Start()
    {
        // TODO: Make it cycle through more later (lazy rn)
        if (objectsToSwapBetween.Length != 2)
            throw new ArgumentOutOfRangeException(nameof(objectsToSwapBetween), "SwapObjects should have exactly 2 states");

        objectsToSwapBetween[1].SetActive(false);
        inDefaultState = true;
    }

    public void OnClicked()
    {
        if (!enabled || !onTrySwap(this))
            return;

        inDefaultState = !inDefaultState;
        if (inDefaultState && soundGoingToFirstState != null)
            soundGoingToFirstState.Play(transform.position);
        else if (!inDefaultState && soundGoingToSecondState != null)
            soundGoingToSecondState.Play(transform.position);

        objectsToSwapBetween[0].SetActive(objectsToSwapBetween[1].activeSelf); // Set first to second's current state
        objectsToSwapBetween[1].SetActive(!objectsToSwapBetween[1].activeSelf); // Flip second's state
    }
}
