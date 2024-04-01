<?php
require('../Connect.php');


$JSON_GenData = $_POST["JSON_GenData"];
$JSON_StatsData = $_POST["JSON_StatsData"];
$JSON_LvlData = $_POST["JSON_LvlData"];


$GenData = json_decode($JSON_GenData);
$StatsData = json_decode($JSON_StatsData);
$LvlData = json_decode($JSON_LvlData);

$sql = "UPDATE table_masterlist SET 
        vision = '".$GenData->Vision."',
        weapon_type = '".$GenData->Weapon."',
        region = '".$GenData->Region."',
        constellation = '".$GenData->Constellation."',
        affiliation = '".$GenData->Affiliation."',
        character_description = '".$GenData->Description."',
        rarity = '".$GenData->Rarity."'
WHERE character_name = '".$GenData->Character_name."';";

// atk_1_20 = '".$GenData->Atk_base."'

// public int Atk_base;
// public int Def_base;
// public int Hp_base;
// public int Atk_max;
// public int Def_max;
// public int Hp_max;
// public string Ascension_stat;
// public string Ascension_base;
// public string Ascension_max;



// $FIELD_CharacterName = $_POST["FIELD_CharacterName"];

// $FIELD_Rarity = $_POST["FIELD_Rarity"];
// $FIELD_Region = $_POST["FIELD_Region"];
// $FIELD_Vision = $_POST["FIELD_Vision"];
// $FIELD_Arkhe = $_POST["FIELD_Arkhe"];
// $FIELD_Weapon = $_POST["FIELD_Weapon"];
// $FIELD_Model = $_POST["FIELD_Model"];
// $FIELD_Constellation = $_POST["FIELD_Constellation"];
// $FIELD_Birthday = $_POST["FIELD_Birthday"];
// $FIELD_Dish = $_POST["FIELD_Dish"];
// $FIELD_Affiliation = $_POST["FIELD_Affiliation"];
// $FIELD_Limited = $_POST["FIELD_Limited"];
// $FIELD_Voice_en = $_POST["FIELD_Voice_en"];
// $FIELD_Voice_cn = $_POST["FIELD_Voice_cn"];
// $FIELD_Voice_jp = $_POST["FIELD_Voice_jp"];
// $FIELD_Voice_kr = $_POST["FIELD_Voice_kr"];
// $FIELD_Ascension = $_POST["FIELD_Ascension"];
// $FIELD_Ascension_specialty = $_POST["FIELD_Ascension_specialty"];
// $FIELD_Ascension_material = $_POST["FIELD_Ascension_material"];
// $FIELD_Ascension_boss = $_POST["FIELD_Ascension_boss"];
// $FIELD_Talent_material = $_POST["FIELD_Talent_material"];
// $FIELD_Talent_book_1 = $_POST["FIELD_Talent_book_1"];
// $FIELD_Talent_book_2 = $_POST["FIELD_Talent_book_2"];
// $FIELD_Talent_book_3 = $_POST["FIELD_Talent_book_3"];
// $FIELD_Talent_book_4 = $_POST["FIELD_Talent_book_4"];
// $FIELD_Talent_book_5 = $_POST["FIELD_Talent_book_5"];
// $FIELD_Talent_book_6 = $_POST["FIELD_Talent_book_6"];
// $FIELD_Talent_book_7 = $_POST["FIELD_Talent_book_7"];
// $FIELD_Talent_book_8 = $_POST["FIELD_Talent_book_8"];
// $FIELD_Talent_book_9 = $_POST["FIELD_Talent_book_9"];
// $FIELD_Talent_book_10 = $_POST["FIELD_Talent_book_10"];
// $FIELD_Talent_weekly = $_POST["FIELD_Talent_weekly"];
// $FIELD_hp_90_90 = $_POST["FIELD_hp_90_90"];
// $FIELD_def_90_90 = $_POST["FIELD_def_90_90"];
// $FIELD_atk_90_90 = $_POST["FIELD_atk_90_90"];
// $FIELD_hp_1_20 = $_POST["FIELD_hp_1_20"];
// $FIELD_def_1_20 = $_POST["FIELD_def_1_20"];
// $FIELD_atk_1_20 = $_POST["FIELD_atk_1_20"];
// $FIELD_special_0 = $_POST["FIELD_special_0"];
// $FIELD_special_1 = $_POST["FIELD_special_1"];
// $FIELD_special_2 = $_POST["FIELD_special_2"];
// $FIELD_special_3 = $_POST["FIELD_special_3"];
// $FIELD_special_4 = $_POST["FIELD_special_4"];
// $FIELD_special_5 = $_POST["FIELD_special_5"];
// $FIELD_special_6 = $_POST["FIELD_special_6"];
// $FIELD_character_description = $_POST["FIELD_character_description"];
// $FIELD_total_banner_runs = $_POST["FIELD_total_banner_runs"];
// $FIELD_last_banner_appearance = $_POST["FIELD_last_banner_appearance"];
// $FIELD_total_revenue = $_POST["FIELD_total_revenue"];

// $FIELD_Release = date('Y-m-d',strtotime($_POST["FIELD_Release"]));

// $sql = "UPDATE table_masterlist SET
// 	rarity = '".$FIELD_Rarity."',
//     region = '".$FIELD_Region."',
//     vision = '".$FIELD_Vision."',
//     arkhe = '".$FIELD_Arkhe."',
//     weapon_type = '".$FIELD_Weapon."',
//     model = '".$FIELD_Model."',
//     constellation = '".$FIELD_Constellation."',
//     birthday = '".$FIELD_Birthday."',
//     special_dish = '".$FIELD_Dish."',
//     affiliation = '".$FIELD_Affiliation."',
//     limited = '".$FIELD_Limited."',
//     release_date = '".$FIELD_Release."',
//     voice_en = '".$FIELD_Voice_en."',
//     voice_cn = '".$FIELD_Voice_cn."',
//     voice_jp = '".$FIELD_Voice_jp."',
//     voice_kr = '".$FIELD_Voice_kr."',
//     ascension = '".$FIELD_Ascension."',
//     ascension_specialty = '".$FIELD_Ascension_specialty."',
//     ascension_material = '".$FIELD_Ascension_material."',
//     ascension_boss = '".$FIELD_Ascension_boss."',
//     talent_material = '".$FIELD_Talent_material."',
//     `talent_book_1-2` = '".$FIELD_Talent_book_1."',
//     `talent_book_2-3` = '".$FIELD_Talent_book_2."',
//     `talent_book_3-4` = '".$FIELD_Talent_book_3."',
//     `talent_book_4-5` = '".$FIELD_Talent_book_4."',
//     `talent_book_5-6` = '".$FIELD_Talent_book_5."',
//     `talent_book_6-7` = '".$FIELD_Talent_book_6."',
//     `talent_book_7-8` = '".$FIELD_Talent_book_7."',
//     `talent_book_8-9` = '".$FIELD_Talent_book_8."',
//     `talent_book_9-10` = '".$FIELD_Talent_book_9."',
//     talent_weekly = '".$FIELD_Talent_weekly."',
//     hp_90_90 = '".$FIELD_hp_90_90."',
//     def_90_90 = '".$FIELD_def_90_90."',
//     atk_90_90 = '".$FIELD_atk_90_90."',
//     hp_1_20 = '".$FIELD_hp_1_20."',
//     def_1_20 = '".$FIELD_def_1_20."',
//     atk_1_20 = '".$FIELD_atk_1_20."',
//     special_0 = '".$FIELD_special_0."',
//     special_1 = '".$FIELD_special_1."',
//     special_2 = '".$FIELD_special_2."',
//     special_3 = '".$FIELD_special_3."',
//     special_4 = '".$FIELD_special_4."',
//     special_5 = '".$FIELD_special_5."',
//     special_6 = '".$FIELD_special_6."',
//     character_description = '".$FIELD_character_description."',
//     total_banner_runs = '".$FIELD_total_banner_runs."',
//     last_banner_appearance = '".$FIELD_last_banner_appearance."',  
//     total_revenue = '".$FIELD_total_revenue."'
// WHERE character_name = '".$FIELD_CharacterName."'";


mysqli_query($CONNECTION, $sql) or die("[1] FAILED TO UPDATE CHARACTER");

echo "UPDATED:" . $GenData->Character_name;
?>