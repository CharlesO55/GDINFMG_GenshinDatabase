using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class TextCleaner
{
    public static string ParseAlphanumeric(string text, bool keepSpaces = true, string textIfEmpty = "None")
    {
        string cleanedText = "";

        for (int i = 0; i < text.Length; i++)
        {
            if ((keepSpaces && text[i] == ' ') || 
                (text[i] > '0' && text[i] < '9') ||
                (text[i] > 'a' && text[i] < 'z') ||
                (text[i] > 'A' && text[i] < 'Z'))
            {
                cleanedText += text[i];
            }
        }

        if(text.Length == 0 )
        {
            cleanedText = textIfEmpty;
        }
        return text;
    }

    public static int CleanNumbers(string s)
    {
        string cleaned = Regex.Replace(s, "[^0-9]", "");
        cleaned = (cleaned == "") ? "0" : cleaned;

        return int.Parse(cleaned);
    }

    public static string CleanSpecialPercentage(string s)
    {
        string newS = Regex.Replace(s, "[^0-9]", "");

        newS = newS.Length switch
        {
            0 => "0.00",
            1 => "0.0" + newS,
            2 => "0." + newS,
            _ => newS.Insert(newS.Length - 2, ".")
        };

        return newS + "%";
    }
}
