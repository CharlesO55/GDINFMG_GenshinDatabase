/***********************************
* #1 CREATING REVENUE SUMMARY VIEW *
************************************/
CREATE OR REPLACE VIEW view_revenue_summary AS SELECT 
	`5_star_characters` AS `character_name`, 
	COUNT(`5_star_characters`) AS total_banner_runs,
    MAX(version) AS last_banner_appearance,
    SUM(revenue) AS total_revenue
FROM import_revenue
GROUP BY `5_star_characters`;



/*********************************************
* #2 CREATING MASTERLIST FOR BETTER UPDATING *
**********************************************/
DROP TABLE IF EXISTS table_test;
CREATE TABLE table_test AS
SELECT 	import_general.* , 
		import_descriptions.character_description, 
        view_revenue_summary.total_banner_runs, view_revenue_summary.last_banner_appearance, view_revenue_summary.total_revenue FROM 
	import_general LEFT JOIN import_descriptions 
		ON import_general.character_name = import_descriptions.character_name
    LEFT JOIN view_revenue_summary
    	ON import_general.character_name = view_revenue_summary.character_name;

# Remove unnecessary columns
ALTER TABLE table_test 
	DROP COLUMN hp_80_90, 
    DROP COLUMN atk_80_90, 
    DROP COLUMN def_80_90, 
    DROP COLUMN hp_80_80, 
    DROP COLUMN atk_80_80, 
    DROP COLUMN def_80_80, 
    DROP COLUMN hp_70_80, 
    DROP COLUMN atk_70_80, 
    DROP COLUMN def_70_80, 
    DROP COLUMN hp_70_70, 
    DROP COLUMN atk_70_70, 
    DROP COLUMN def_70_70, 
    DROP COLUMN hp_60_70,	
    DROP COLUMN atk_60_70,	
    DROP COLUMN def_60_70,	
    DROP COLUMN hp_60_60,
    DROP COLUMN atk_60_60,
    DROP COLUMN def_60_60,
    DROP COLUMN hp_50_60,
    DROP COLUMN atk_50_60,
    DROP COLUMN def_50_60,
    DROP COLUMN hp_50_50,
    DROP COLUMN atk_50_50,
    DROP COLUMN def_50_50,
    DROP COLUMN hp_40_50,
    DROP COLUMN atk_40_50,
    DROP COLUMN def_40_50,
    DROP COLUMN hp_40_40,
    DROP COLUMN atk_40_40,
    DROP COLUMN def_40_40,
    DROP COLUMN hp_20_40,
    DROP COLUMN atk_20_40,
    DROP COLUMN def_20_40,
    DROP COLUMN hp_20_20,
    DROP COLUMN atk_20_20,
    DROP COLUMN def_20_20,
    DROP COLUMN special_1,
    DROP COLUMN special_2,
    DROP COLUMN special_3,
    DROP COLUMN special_4,
    DROP COLUMN special_5;



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
	SELECT character_name, rarity, vision, weapon_type, region, model, arkhe, constellation, birthday, special_dish, affiliation, character_description, talent_material, `talent_book_1-2`, `talent_book_2-3`, `talent_book_3-4`, `talent_book_4-5`, `talent_book_5-6`, `talent_book_6-7`, `talent_book_7-8`, `talent_book_8-9`, `talent_book_9-10`, talent_weekly
    FROM table_masterlist";

/***********************************
* #11 CREATING LEVELING REQS       *
************************************/
$sql11 = "CREATE OR REPLACE VIEW view_leveling_reqs AS
	SELECT character_name, ascension_specialty, ascension_material, ascension_boss, 
    FROM table_masterlist";