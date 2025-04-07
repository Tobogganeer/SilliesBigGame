using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swingCGAnimation : MonoBehaviour
{
    public GameObject self;
    public Animator animator;
    AnimatorStateInfo animStateInfo;
    public float NTime;

    bool animationFinished;

    // Start is called before the first frame update
    void Start()
    {
        NTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        NTime = animStateInfo.normalizedTime;

        if (NTime > 1.0f) self.SetActive(false);
    }

}
