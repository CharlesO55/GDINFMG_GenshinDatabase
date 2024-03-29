using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ImageLoader
{
    public static void LoadImage(Sprite targetSprite, string filename)
    {
        byte[] imgData = System.IO.File.ReadAllBytes(filename);
        targetSprite.texture.LoadImage(imgData);
    }
}
