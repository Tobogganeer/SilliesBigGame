using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSprites
{
    static Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

    /// <summary>
    /// Returns the sprite of the passed <paramref name="item"/>
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static Sprite Get(string item)
    {
        if (sprites.TryGetValue(item, out Sprite sprite))
            return sprite;

        sprite = Resources.Load<Sprite>(ItemData.instance.itemData[item]["itemImage"]);
        sprites.Add(item, sprite);
        return sprite;
    }
}
