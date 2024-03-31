using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharEditScreen : MonoBehaviour
{
    [Header("Text Info")]
    [SerializeField] TMP_InputField _charName;
    [SerializeField] TMP_InputField _region;
    [SerializeField] TMP_InputField _constellation;
    [SerializeField] TMP_InputField _affiliation;
    [SerializeField] TMP_InputField _charDescription;

    [Header("Stats Info")]
    [SerializeField] TMP_InputField _atkVal;
    [SerializeField] TMP_InputField _defVal;
    [SerializeField] TMP_InputField _hpVal;
    [SerializeField] TMP_InputField _speicalVal;
    [SerializeField] TMP_Dropdown _specialStatLabel;


    private void Start()
    {
        CharacterGeneralData gen_Data = SelectCharacter.Instance.GeneralData;
        CharacterStatsData stats_Data = SelectCharacter.Instance.StatsData;
        CharacterLevelingData lvl_Data = SelectCharacter.Instance.LevelingData;

        this.LoadTextData(gen_Data);
        this.LoadStatsData(stats_Data);

        _speicalVal.onSubmit.AddListener(CleanSpecialPercentage);
    }

    void LoadTextData(CharacterGeneralData gen_Data)
    {
        this._charName.text = gen_Data.Character_name;
        this._region.text = gen_Data.Region;
        this._constellation.text = gen_Data.Constellation;
        this._affiliation.text = gen_Data.Affiliation;
        this._charDescription.text = gen_Data.Description;
    }

    void LoadStatsData(CharacterStatsData stats_Data)
    {
        this._atkVal.text = stats_Data.Atk_max.ToString();
        this._defVal.text = stats_Data.Def_base.ToString();
        this._hpVal.text = stats_Data.Hp_max.ToString();
        this._speicalVal.text = stats_Data.Ascension_max;

        this._specialStatLabel.options.Clear();

        foreach (string opt in DistinctValues.Instance.DistinctColValues[EColNames.SPECIAL_STATS])
        {   
            this._specialStatLabel.options.Add(new TMP_Dropdown.OptionData(opt));
        }
        
        this._specialStatLabel.value = _specialStatLabel.options.FindIndex(option => option.text == stats_Data.Ascension_stat);
    }



    public void SaveData()
    {
        CharacterStatsData stats_Data = SelectCharacter.Instance.StatsData;

        stats_Data.Atk_max = CleanNumbers(this._atkVal.text);
        stats_Data.Def_max = CleanNumbers(this._defVal.text);
        stats_Data.Hp_max = CleanNumbers(this._hpVal.text);

        this.CleanSpecialPercentage(this._speicalVal.text);
        stats_Data.Ascension_max = this._speicalVal.text;
        stats_Data.Ascension_stat = this._specialStatLabel.options[_specialStatLabel.value].text;


        SceneLoader.Instance.LoadScene("MainScreen");
    }

    void CleanSpecialPercentage(string s)
    {
        string newS = Regex.Replace(s, "[^0-9]", "");

        newS = newS.Length switch
        {
            0 => "0.00",
            1 => "0.0" + newS,
            2 => "0." + newS,
            _ => newS.Insert(newS.Length - 2, ".")
        };

        this._speicalVal.text = newS + "%";
    }

    int CleanNumbers(string s)
    {
        string cleaned = Regex.Replace(s, "[^0-9]", "");
        cleaned = (cleaned == "") ? "0" : cleaned;

        return int.Parse(cleaned);
    }
}
