using System.Collections;
using System.Collections.Generic;
using Tobo.Util;
using UnityEngine;

public class CameraBob : MonoBehaviour
{
    public float speed = 1f;
    public float bobSize = 0.1f;

    Vector3 target;
    Vector3 direction;

    private void Update()
    {
        if (Vector3.Distance(transform.localPosition, target) < 0.01f)
            target = Random.insideUnitSphere * bobSize;

        direction = Vector3.Lerp(direction, transform.localPosition.Dir(target), speed * Time.deltaTime);
        transform.position += direction * speed * bobSize * Time.deltaTime;
    }
}
