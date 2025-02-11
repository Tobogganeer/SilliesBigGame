using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectAtLocation : MonoBehaviour
{
    public Transform lookTarget;

    public List<GameObject> objects;
    public bool enabledWhenPlayerIsHere = true;

    bool wasPlayerHere;

    private void Update()
    {
        bool playerHere = PlayerMovement.instance.CurrentPosRot.Equals(new PosRot(transform.position, lookTarget.position));

        if (playerHere != wasPlayerHere)
        {
            wasPlayerHere = playerHere;

            foreach (GameObject obj in objects)
            {
                obj.SetActive(playerHere == enabledWhenPlayerIsHere);
            }
        }
    }
}
