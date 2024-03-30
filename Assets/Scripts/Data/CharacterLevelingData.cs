using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterLevelingData
{
    public string Character_Name;
    public string Req_Gather = "None";
    public string ID_Gather = "None";
    public string Req_Mob = "None";
    public string ID_Mob = "None";
    public string Req_Boss = "None";
    public string ID_Boss = "None";
    public string Req_Gem = "None";
    public string ID_Gem = "None";

    private Dictionary<EElements, string> DictGemNames = new()
    {
        { EElements.ANEMO, "Vayuda Turquoise Gemstone"},
        { EElements.GEO, "Prithiva Topaz Gemstone"},
        { EElements.ELECTRO, "Vajrada Amethyst Gemstone"},
        { EElements.DENDRO, "Nagadus Emerald Gemstone"},
        { EElements.HYDRO, "Varunada Lazurite Gemstone"},
        { EElements.CRYO, "Shivada Jade Gemstone"},
        { EElements.PYRO, "Agnidus Agate Gemstone"},
        { EElements.NA, "Brilliant Diamond Gemstone"}
    };

    public string SetGem(string vision)
    {
        EElements element = vision switch
        {
            "Anemo" => EElements.ANEMO,
            "Geo" => EElements.GEO,
            "Electro" => EElements.ELECTRO,
            "Dendro" => EElements.DENDRO,
            "Hydro" => EElements.HYDRO,
            "Pyro" => EElements.PYRO,
            "Cryo" => EElements.CRYO,
            _ => EElements.NA
        };

        this.Req_Gem = this.DictGemNames[element];
        return this.Req_Gem;
    }
}