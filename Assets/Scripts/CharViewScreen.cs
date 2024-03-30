using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharViewScreen : MonoBehaviour
{
    [Header("Info fields")]
    [SerializeField] TextMeshProUGUI _charName;
    [SerializeField] TextMeshProUGUI _charRegion;
    [SerializeField] TextMeshProUGUI _charConstellation;
    [SerializeField] TextMeshProUGUI _charAffiliation;
    [SerializeField] TextMeshProUGUI _charDescription;

    [Header("Stat fields")]
    [SerializeField] TextMeshProUGUI _statAtk;
    [SerializeField] TextMeshProUGUI _statDef;
    [SerializeField] TextMeshProUGUI _statHp;
    [SerializeField] TextMeshProUGUI _statSpecial;
    [SerializeField] TextMeshProUGUI _statSpecialVal;

    [Header("Rarity")]
    [SerializeField] GameObject _starPrefab;
    [SerializeField] Transform _starContainer;

    [Header("Images")]
    [SerializeField] Image _bgColor;
    [SerializeField] Image _splashArt;
    [SerializeField] Image _visionIcon;
    [SerializeField] Image _weaponIcon;


    private void Start()
    {
        CharacterGeneralData gen_Data = SelectCharacter.Instance.GeneralData;
        this._charName.text = gen_Data.Character_name;
        this._charRegion.text = gen_Data.Region;
        this._charConstellation.text = gen_Data.Constellation;
        this._charAffiliation.text = gen_Data.Affiliation;
        this._charDescription.text = gen_Data.Description;


        CharacterStatsData stat_Data = SelectCharacter.Instance.StatsData;
        this._statAtk.text = stat_Data.Atk_base.ToString();
        this._statDef.text = stat_Data.Def_base.ToString();
        this._statHp.text = stat_Data.Hp_base.ToString();
        this._statSpecial.text = stat_Data.Ascension_stat;
        this._statSpecialVal.text = stat_Data.Ascension_base;

        this.AssignRarity(gen_Data.Rarity);
        this.AssignBGColor(gen_Data.Vision);
        
        ImageLoader.LoadImage(this._visionIcon.sprite, "Assets/Sprites/Elements/UI_Buff_Element_" + gen_Data.Vision + ".png");
        ImageLoader.LoadImage(this._weaponIcon.sprite, "Assets/Sprites/Weapons/UI_GachaTypeIcon_" + gen_Data.Weapon + ".png");
        if(!ImageLoader.LoadImage(this._splashArt.sprite, "Assets/Sprites/Splash/UI_Gacha_AvatarImg_" + gen_Data.Character_name + ".png"))
        {
            Debug.LogWarning("No splash art found");
        }
    }

    void AssignRarity(int rarity)
    {
        for(int i = 0; i < rarity; i++)
        {
            GameObject.Instantiate(_starPrefab, this._starContainer);
        }
    }

    void AssignBGColor(string element)
    {
        this._bgColor.color = ElementColors.Dict[element];
    }
}
