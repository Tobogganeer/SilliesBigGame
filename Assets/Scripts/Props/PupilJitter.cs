using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupilJitter : MonoBehaviour
{
    public float amount = 0.01f;
    public Vector3 mask = new Vector3(1, 1, 0);
    public float delay = 0.2f;

    Vector3 startingPosition;

    private IEnumerator Start()
    {
        startingPosition = transform.localPosition;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            transform.localPosition = startingPosition + Vector3.Scale(Random.insideUnitSphere, mask) * amount;
            yield return wait;
        }
    }
}
