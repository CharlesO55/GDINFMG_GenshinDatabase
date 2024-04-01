using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterGeneralData
{
    public int Character_Id;
    public string Character_name;
    public string Vision;
    public string Weapon;
    public string Region;
    public string Constellation;
    public string Affiliation;
    public string Description;

    public int Rarity;


    public void CleanStrings()
    {
        Character_name = TextCleaner.RegexAlphaNumeric(Character_name);
        Constellation = TextCleaner.RegexAlphaNumeric(Constellation);
        Affiliation = TextCleaner.RegexAlphaNumeric(Affiliation);
        Description = TextCleaner.RegexAlphaNumeric(Description);
    }
}
