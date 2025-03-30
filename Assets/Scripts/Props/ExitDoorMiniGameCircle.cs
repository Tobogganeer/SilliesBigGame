using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExitDoorMiniGameCircle : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public ExitDoorMiniGame minigame;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("entered");
        minigame.timer = 0f;
        minigame.changeProgressRate(3f);
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        //Debug.Log("exited");
        minigame.changeProgressRate(-3f);
    }


}
