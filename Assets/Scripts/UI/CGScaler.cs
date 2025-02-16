using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CGScaler : MonoBehaviour
{
    public float aspectRatio;
    public ExpandTo expandTo;

    RectTransform rectTransform;

    Image image;
    RawImage rawImage;

    private void Start()
    {
        SetSize();
    }

    void Init()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
            image = GetComponent<Image>();
            rawImage = GetComponent<RawImage>();
        }
    }

    private void OnValidate()
    {
        SetSize();
    }

    void SetSize()
    {
        Init();
        
    }

    public enum ExpandTo
    {
        Width,
        Height
    }
}
