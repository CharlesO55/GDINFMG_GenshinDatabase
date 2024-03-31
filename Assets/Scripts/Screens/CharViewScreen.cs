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

    [Header("Leveling")]
    [SerializeField] Image _itemBoss;
    [SerializeField] Image _itemMob;
    [SerializeField] Image _itemGather;
    [SerializeField] Image _itemGem;

    private void Start()
    {
        CharacterGeneralData gen_Data = SelectCharacter.Instance.GeneralData;
        CharacterStatsData stat_Data = SelectCharacter.Instance.StatsData;
        CharacterLevelingData lvl_Data = SelectCharacter.Instance.LevelingData;


        this.AssignDescriptions(gen_Data);
        this.AssignStats(stat_Data);
        this.AssignRarity(gen_Data.Rarity);
        this.AssignBGColor(gen_Data.Vision);
        this.AssignImages(gen_Data, lvl_Data);
    }

    void AssignRarity(int rarity)
    {
        for(int i = 0; i < rarity; i++)
        {
            Instantiate(_starPrefab, this._starContainer);
        }
    }

    void AssignBGColor(string element)
    {
        this._bgColor.color = ElementColors.Dict[element];
    }


    void AssignDescriptions(CharacterGeneralData gen_Data)
    {
        this._charName.text = gen_Data.Character_name;
        this._charRegion.text = gen_Data.Region;
        this._charConstellation.text = gen_Data.Constellation;
        this._charAffiliation.text = gen_Data.Affiliation;
        this._charDescription.text = gen_Data.Description;
    }
    void AssignStats(CharacterStatsData stat_Data)
    {
        this._statAtk.text = stat_Data.Atk_max.ToString();
        this._statDef.text = stat_Data.Def_max.ToString();
        this._statHp.text = stat_Data.Hp_max.ToString();
        this._statSpecial.text = stat_Data.Ascension_stat;
        this._statSpecialVal.text = stat_Data.Ascension_max;
    }
    void AssignImages(CharacterGeneralData gen_Data, CharacterLevelingData lvl_Data)
    {
        ImageLoader.LoadImageInResources(this._visionIcon, "Elements/UI_Buff_Element_" + gen_Data.Vision);
        ImageLoader.LoadImageInResources(this._weaponIcon, "Weapons/UI_GachaTypeIcon_" + gen_Data.Weapon);
        ImageLoader.LoadImageInResources(this._itemBoss, "Mats_BossDrops/UI_ItemIcon_" + lvl_Data.ID_Boss);
        ImageLoader.LoadImageInResources(this._itemMob, "Mats_MobDrops/UI_ItemIcon_" + lvl_Data.ID_Mob);
        ImageLoader.LoadImageInResources(this._itemGem, "Mats_Gem/UI_ItemIcon_" + lvl_Data.ID_Gem);
        ImageLoader.LoadImageInResources(this._itemGather, "Mats_Gather/UI_ItemIcon_" + lvl_Data.ID_Gather);



        /*
        ImageLoader.LoadImage(this._visionIcon.sprite, "Assets/Sprites/Elements/UI_Buff_Element_" + gen_Data.Vision + ".png");
        ImageLoader.LoadImage(this._weaponIcon.sprite, "Assets/Sprites/Weapons/UI_GachaTypeIcon_" + gen_Data.Weapon + ".png");
        ImageLoader.LoadImage(this._itemBoss.sprite, "Assets/Sprites/Mats_BossDrops/UI_ItemIcon_" + lvl_Data.ID_Boss + ".png");
        ImageLoader.LoadImage(this._itemMob.sprite, "Assets/Sprites/Mats_MobDrops/UI_ItemIcon_" + lvl_Data.ID_Mob + ".png");
        ImageLoader.LoadImage(this._itemGem.sprite, "Assets/Sprites/Mats_Gem/UI_ItemIcon_" + lvl_Data.ID_Gem + ".png");
        ImageLoader.LoadImage(this._itemGather.sprite, "Assets/Sprites/Mats_Gather/UI_ItemIcon_" + lvl_Data.ID_Gather + ".png");
        */

        //SPECIAL CHECK FOR TRAVELER NAME
        string splashName = gen_Data.Character_name;
        if (splashName.Contains("Aether"))
            splashName = "Aether";
        else if (splashName.Contains("Lumine"))
            splashName = "Lumine";

        ImageLoader.LoadImageInResources(this._splashArt, "Splash/UI_Gacha_AvatarImg_" + splashName);
        //ImageLoader.LoadImage(this._splashArt.sprite, "Assets/Sprites/Splash/UI_Gacha_AvatarImg_" + splashName + ".png");
    }

}
