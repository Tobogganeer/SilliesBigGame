using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public string description;
    public Texture2D icon;
}
