using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ElementColors
{
    public static Dictionary<string, Color32> Dict = new()
    {
        {"Cryo" , new Color32(80, 160, 255, 255) },
        {"Anemo" , new Color32(80, 200, 170, 255) },
        {"Dendro" , new Color32(40, 200, 80, 255) },
        {"Electro" , new Color32(180, 80, 220, 255) },
        {"Geo" , new Color32(230, 150, 10, 255) },
        {"Hydro" , new Color32(60, 120, 255, 255) },
        {"Pyro" , new Color32(210, 80, 80, 255) }
    };
}