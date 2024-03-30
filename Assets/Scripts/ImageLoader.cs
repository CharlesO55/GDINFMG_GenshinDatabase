using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ImageLoader
{
    public static bool LoadImage(Sprite targetSprite, string filename, string altFile = "Assets/Sprites/Regions/Unknown.png")
    {
        if(!System.IO.File.Exists(filename))
        {
            Debug.LogWarning($"Failed to load image {filename}");
            filename = altFile;
            if (!System.IO.File.Exists(filename))
            {
                Debug.LogError($"Neither files work {altFile}");
                return false;
            }
        }

        byte[] imgData = System.IO.File.ReadAllBytes(filename);

        targetSprite.texture.LoadImage(imgData);

        return true;
    }


    public static bool LoadImageInResources(UnityEngine.UI.Image targetImage, string filename)
    {
        targetImage.sprite = Resources.Load<Sprite>(filename);
        return targetImage.sprite != null;
    }
}