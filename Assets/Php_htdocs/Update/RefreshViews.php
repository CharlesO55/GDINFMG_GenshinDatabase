<?php
require("../Connect.php");



/*************************
* #3 Create Query View   *
*************************/
$sql3 = "CREATE OR REPLACE VIEW view_character_queries AS
	SELECT character_name, rarity, vision, weapon_type, region, model FROM table_masterlist";
/*****************************
* #4 Create Max Stats View   *
******************************/
$sql4 = "DROP VIEW IF EXISTS view_max_stats";/*"CREATE OR REPLACE VIEW view_max_stats AS
	SELECT character_name, atk_90_90, def_90_90, hp_90_90 FROM table_masterlist";*/

/*****************************
* #5 Create Base Stats View  *
******************************/    
$sql5 = "DROP VIEW IF EXISTS view_base_stats";/*"CREATE OR REPLACE VIEW view_base_stats AS
	SELECT character_name, atk_1_20, def_1_20, hp_1_20 FROM table_masterlist";*/

/***********************************
* #6 CREATING ASCENSION REQS VIEW  *
************************************/
$sql6 = "DROP VIEW IF EXISTS view_ascension_reqs"; /*"CREATE OR REPLACE VIEW view_ascension_reqs AS
	SELECT character_name, ascension, ascension_specialty, ascension_material, ascension_boss FROM table_masterlist";*/
    
/***********************************
* #7 CREATING SPECIAL STATS VIEW   *
************************************/
$sql7 = "DROP VIEW IF EXISTS view_special_stats";/*"CREATE OR REPLACE VIEW view_special_stats AS
	SELECT character_name, ascension, special_0, special_1, special_2, special_3, special_4, special_5, special_6 FROM table_masterlist";*/
    
/***********************************
* #8 CREATING TALENTS REQS VIEW    *
************************************/
$sql8 = "DROP VIEW IF EXISTS view_talent_reqs";/*"CREATE OR REPLACE VIEW view_talent_reqs AS
	SELECT character_name, talent_material, `talent_book_1-2`, `talent_book_2-3`, `talent_book_3-4`, `talent_book_4-5`, `talent_book_5-6`, 				`talent_book_6-7`, `talent_book_7-8`, `talent_book_8-9`, `talent_book_9-10`, talent_weekly 
    FROM table_masterlist";*/
    
/***********************************
* #9 CREATING CHARACTER RELEASE VIEW*
************************************/
$sql9 = "CREATE OR REPLACE VIEW view_character_release AS
	SELECT character_name, release_date, limited, voice_en, voice_cn, voice_jp, voice_kr 
    FROM table_masterlist";
    
/***********************************
* #10 CREATING CHARACTER DATA VIEW *
************************************/
$sql10 = "CREATE OR REPLACE VIEW view_character_general AS
	SELECT character_name, rarity, vision, weapon_type, region, model, arkhe, constellation, birthday, special_dish, affiliation, character_description
    FROM table_masterlist";

/***********************************
* #11 CREATING LEVELING REQS       *
************************************/
$sql11 = "CREATE OR REPLACE VIEW view_leveling_reqs AS
	SELECT character_name, ascension_specialty, ascension_material, ascension_boss, ascension_gem,
			talent_material, `talent_book_1-2`, `talent_book_2-3`, `talent_book_3-4`, `talent_book_4-5`, `talent_book_5-6`, `talent_book_6-7`, `talent_book_7-8`, `talent_book_8-9`, `talent_book_9-10`, talent_weekly
	FROM table_masterlist;";

/***********************************
* #12 CREATING CHARACTER STATS     *
************************************/
$sql12 = "CREATE OR REPLACE VIEW view_character_stats AS
	SELECT character_name, atk_1_20, def_1_20, hp_1_20, atk_90_90, def_90_90, hp_90_90, ascension, special_0, special_6
	FROM table_masterlist";


mysqli_query($CONNECTION, $sql3) or die("[3] Query view failed");
mysqli_query($CONNECTION, $sql4) or die("[4] Max stats view failed");
mysqli_query($CONNECTION, $sql5) or die("[5] Base stats view failed");
mysqli_query($CONNECTION, $sql6) or die("[6] Ascension reqs view failed");
mysqli_query($CONNECTION, $sql7) or die("[7] Special reqs view failed");
mysqli_query($CONNECTION, $sql8) or die("[8] Talent reqs view failed");
mysqli_query($CONNECTION, $sql9) or die("[9] Char release view failed");
mysqli_query($CONNECTION, $sql10) or die("[10] Char general view failed");
mysqli_query($CONNECTION, $sql11) or die("[11] Char leveing reqs view failed");
mysqli_query($CONNECTION, $sql12) or die("[12] Char stats view failed");

echo "SUCCESS~Refresh views updated";
?>