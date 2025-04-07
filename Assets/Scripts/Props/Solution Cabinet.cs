using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionCabinet : MonoBehaviour
{
    public GameObject newCG;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(newCG.active == true)
        {
            gameObject.GetComponent<EnableObjectOnClick>().obj = newCG;
        }
    }

}
