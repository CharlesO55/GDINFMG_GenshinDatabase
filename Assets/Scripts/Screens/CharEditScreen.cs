using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharEditScreen : MonoBehaviour
{
    private string _compareStartingCharName;

    [Header("Text Info")]
    [SerializeField] TMP_InputField _charName;
    [SerializeField] TMP_Dropdown _rarity;
    [SerializeField] TMP_Dropdown _region;
    [SerializeField] TMP_InputField _constellation;
    [SerializeField] TMP_InputField _affiliation;
    [SerializeField] TMP_InputField _charDescription;

    [Header("Stats Info")]
    [SerializeField] TMP_InputField _atkVal;
    [SerializeField] TMP_InputField _defVal;
    [SerializeField] TMP_InputField _hpVal;
    [SerializeField] TMP_InputField _speicalVal;
    [SerializeField] TMP_Dropdown _specialStatLabel;

    [Header("Image Info")]
    [SerializeField] Image _charElement;
    [SerializeField] Image _charWeapon;
    [SerializeField] Image _itemBoss;
    [SerializeField] Image _itemMob;
    [SerializeField] Image _itemGem;
    [SerializeField] Image _itemGather;
    [SerializeField] Image _splashImage;
    private void Start()
    {
        CharacterGeneralData gen_Data = SelectCharacter.Instance.GeneralData;
        CharacterStatsData stats_Data = SelectCharacter.Instance.StatsData;
        CharacterLevelingData lvl_Data = SelectCharacter.Instance.LevelingData;

        this._compareStartingCharName = gen_Data.Character_name;

        this.LoadTextData(gen_Data);
        this.LoadStatsData(stats_Data);
        this.LoadRarityStars(gen_Data.Rarity);

        ImageLoader.LoadImageInResources(this._charElement, "Elements/UI_Buff_Element_" + gen_Data.Vision);
        ImageLoader.LoadImageInResources(this._charWeapon, "Weapons/UI_GachaTypeIcon_" + gen_Data.Weapon);
        ImageLoader.LoadImageInResources(this._itemBoss, "Mats_BossDrops/UI_ItemIcon_" + lvl_Data.ID_Boss);
        ImageLoader.LoadImageInResources(this._itemMob, "Mats_MobDrops/UI_ItemIcon_" + lvl_Data.ID_Mob);
        ImageLoader.LoadImageInResources(this._itemGem, "Mats_Gem/UI_ItemIcon_" + lvl_Data.ID_Gem);
        ImageLoader.LoadImageInResources(this._itemGather, "Mats_Gather/UI_ItemIcon_" + lvl_Data.ID_Gather);


        //SPLASH ART
        ImageLoader.LoadImageInResources(this._splashImage, "Splash/UI_Gacha_AvatarImg_" + gen_Data.Character_name);


        _speicalVal.onSubmit.AddListener(value => 
        { 
            this._speicalVal.text = TextCleaner.CleanSpecialPercentage(value);
        });
        _charName.onSubmit.AddListener(value =>
        {
            this.CheckName();
        });
    }

    void LoadTextData(CharacterGeneralData gen_Data)
    {
        this._charName.text = gen_Data.Character_name;
        this._constellation.text = gen_Data.Constellation;
        this._affiliation.text = gen_Data.Affiliation;
        this._charDescription.text = gen_Data.Description;

        this._rarity.value = _rarity.options.FindIndex(option => option.text == gen_Data.Rarity.ToString() + " Star");


        this._region.options.Clear();
        foreach (string s in DistinctValues.DistinctColValues[EColNames.REGIONS])
        {
            this._region.options.Add(new TMP_Dropdown.OptionData(s));
        }
        this._region.value = _region.options.FindIndex(option => option.text == gen_Data.Region);

    }

    void LoadStatsData(CharacterStatsData stats_Data)
    {
        this._atkVal.text = stats_Data.Atk_max.ToString();
        this._defVal.text = stats_Data.Def_base.ToString();
        this._hpVal.text = stats_Data.Hp_max.ToString();
        this._speicalVal.text = stats_Data.Ascension_max;

        this._specialStatLabel.options.Clear();
        foreach (string opt in DistinctValues.DistinctColValues[EColNames.SPECIAL_STATS])
        {   
            this._specialStatLabel.options.Add(new TMP_Dropdown.OptionData(opt));
        }
        
        this._specialStatLabel.value = _specialStatLabel.options.FindIndex(option => option.text == stats_Data.Ascension_stat);
    }

    void LoadRarityStars(int nStars)
    {
        /*foreach(Transform child in _rarityStarsContainer)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < nStars; i++)
        {
            Instantiate(_rarityStarPrefab, _rarityStarsContainer);
        }*/
    }



    public void SaveData()
    {
        this.CheckName();
        this.SaveGeneralData();
        this.SaveStatsData();
        this.SaveLevelingData();

        SelectCharacter.Instance.UpdateItemIDs(EColNames.BOSS_MATS);
        SelectCharacter.Instance.UpdateItemIDs(EColNames.MOB_MATS);
        SelectCharacter.Instance.UpdateItemIDs(EColNames.GATHER_MATS);
        SelectCharacter.Instance.UpdateItemIDs(EColNames.GEM_MATS);

        SelectCharacter.Instance.OnLoadingFinished.AddListener(SwapToMainScreen);
    }

    void SwapToMainScreen()
    {
        SelectCharacter.Instance.OnDatabaseUpdateFinished.AddListener(() => {
            SelectCharacter.Instance.OnDatabaseUpdateFinished.RemoveAllListeners();
            SceneLoader.Instance.LoadScene(EScenes.LOADING_SCREEN, new LoadingParams(EScenes.MAIN_SCREEN, false, false, true, true));
        });

        SelectCharacter.Instance.CallUpdateCharacterDatabase(); 
    }

    
    

    void CheckName()
    {
        this._charName.text = TextCleaner.RegexAlphaNumeric(this._charName.text);


        //DO NOT ALLOW SETTING NEW NAME TO AN EXISTING OLD ONE
        if (this._compareStartingCharName != _charName.text && DistinctValues.DICT_COUNT_CHAR_NAMES.ContainsKey(_charName.text))
        {
            this._charName.text += System.DateTime.Now.ToString("HHmmsstt");
            Debug.LogWarning("Existing name! Changed to " + _charName.text);
        }
    }

    void SaveGeneralData()
    {
        CharacterGeneralData gen_Data = SelectCharacter.Instance.GeneralData;

        
        gen_Data.Character_name = this._charName.text;

        gen_Data.Vision = this._charElement.mainTexture.name.Replace("UI_Buff_Element_", "");
        gen_Data.Weapon = this._charWeapon.mainTexture.name.Replace("UI_GachaTypeIcon_", "");

        gen_Data.Rarity = this._rarity.options[_rarity.value].text[0] - '0';

        gen_Data.Region = this._region.options[_region.value].text;
        gen_Data.Constellation = this._constellation.text;
        gen_Data.Affiliation = this._affiliation.text;
        gen_Data.Description = this._charDescription.text;

        gen_Data.CleanStrings();
}
    void SaveStatsData()
    {
        CharacterStatsData stats_Data = SelectCharacter.Instance.StatsData;
        stats_Data.Character_name = this._charName.text;
        stats_Data.Atk_max = TextCleaner.CleanNumbers(this._atkVal.text);
        stats_Data.Def_max = TextCleaner.CleanNumbers(this._defVal.text);
        stats_Data.Hp_max = TextCleaner.CleanNumbers(this._hpVal.text);

        stats_Data.Ascension_max = TextCleaner.CleanSpecialPercentage(this._speicalVal.text);
        stats_Data.Ascension_stat = this._specialStatLabel.options[_specialStatLabel.value].text;
    }

    void SaveLevelingData()
    {
        CharacterLevelingData lvl_Data = SelectCharacter.Instance.LevelingData;

        lvl_Data.Character_Name = this._charName.text;


        lvl_Data.Name_Gather = "";
        lvl_Data.ID_Gather = this._itemGather.sprite.name.Replace("UI_ItemIcon_", "");
        
        lvl_Data.Name_Mob = "";
        lvl_Data.ID_Mob = this._itemMob.sprite.name.Replace("UI_ItemIcon_", "");
        
        lvl_Data.Name_Boss = "";
        lvl_Data.ID_Boss = this._itemBoss.sprite.name.Replace("UI_ItemIcon_", ""); ;
        
        lvl_Data.Name_Gem = "";
        lvl_Data.ID_Gem = this._itemGem.sprite.name.Replace("UI_ItemIcon_", "");
    }


    private void OnDestroy()
    {
        if (_speicalVal != null)
            _speicalVal.onSubmit.RemoveAllListeners();
        if(_charName != null) 
            _charName.onSubmit.RemoveAllListeners();
    }
}