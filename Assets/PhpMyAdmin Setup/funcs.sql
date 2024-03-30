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

/**********************************
* #1.a ADD ADDITIONAL COLUMN GEMS *
***********************************/
ALTER TABLE import_general
	ADD COLUMN ascension_gem varchar(30) NOT NULL DEFAULT 'Brilliant Diamond Gemstone';
    
UPDATE import_general SET ascension_gem = 'Agnidus Agate Gemstone' WHERE vision = 'Pyro';
UPDATE import_general SET ascension_gem = 'Varunada Lazurite Gemstone' WHERE vision = 'Hydro';
UPDATE import_general SET ascension_gem = 'Nagadus Emerald Gemstone' WHERE vision = 'Dendro';
UPDATE import_general SET ascension_gem = 'Vajrada Amethyst Gemstone' WHERE vision = 'Electro';
UPDATE import_general SET ascension_gem = 'Vayuda Turquoise Gemstone' WHERE vision = 'Anemo';
UPDATE import_general SET ascension_gem = 'Shivada Jade Gemstone' WHERE vision = 'Cryo';
UPDATE import_general SET ascension_gem = 'Prithiva Topaz Gemstone' WHERE vision = 'Geo';
UPDATE import_general SET ascension_gem = 'Brilliant Diamond Gemstone' WHERE character_name LIKE '%Traveler%'; 

/*********************************************
* #2 CREATING MASTERLIST FOR BETTER UPDATING *
**********************************************/
DROP TABLE IF EXISTS table_masterlist;
CREATE TABLE table_masterlist AS
SELECT 	import_general.* , 
		import_descriptions.character_description, 
        view_revenue_summary.total_banner_runs, view_revenue_summary.last_banner_appearance, view_revenue_summary.total_revenue FROM 
	import_general LEFT JOIN import_descriptions 
		ON import_general.character_name = import_descriptions.character_name
    LEFT JOIN view_revenue_summary
    	ON import_general.character_name = view_revenue_summary.character_name
	ORDER BY character_name;

# Remove unnecessary columns
ALTER TABLE table_masterlist 
	ADD UNIQUE(`character_name`),
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
CREATE OR REPLACE VIEW view_character_queries AS
	SELECT character_name, rarity, vision, weapon_type, region, model FROM table_masterlist;
    
/***********************************
* #9 CREATING CHARACTER RELEASE VIEW*
************************************/
CREATE OR REPLACE VIEW view_character_release AS
	SELECT character_name, release_date, limited, voice_en, voice_cn, voice_jp, voice_kr 
    FROM table_masterlist;
    
/***********************************
* #10 CREATING CHARACTER DATA VIEW *
************************************/
CREATE OR REPLACE VIEW view_character_general AS
	SELECT character_name, rarity, vision, weapon_type, region, model, arkhe, constellation, birthday, special_dish, affiliation, character_description
    FROM table_masterlist;

/***********************************
* #11 CREATING LEVELING REQS       *
************************************/
CREATE OR REPLACE VIEW view_leveling_reqs AS
	SELECT character_name, ascension_specialty, ascension_material, ascension_boss, ascension_gem,
    talent_material, `talent_book_1-2`, `talent_book_2-3`, `talent_book_3-4`, `talent_book_4-5`, `talent_book_5-6`, `talent_book_6-7`, `talent_book_7-8`, `talent_book_8-9`, `talent_book_9-10`, talent_weekly
    FROM table_masterlist;
    
/***********************************
* #12 CREATING CHARACTER STATS     *
************************************/
CREATE OR REPLACE VIEW view_character_stats AS
	SELECT character_name, atk_1_20, def_1_20, hp_1_20, atk_90_90, def_90_90, hp_90_90, ascension, special_0, special_6
	FROM table_masterlist;
    
    
/************************************
* #13 CREATING THE SPRITE ID TABLE  *
************************************/
DROP TABLE IF EXISTS table_itemid;

CREATE TABLE table_itemid AS SELECT DISTINCT ascension_boss AS itemName FROM table_masterlist 
        UNION 
        SELECT DISTINCT ascension_material AS itemName FROM table_masterlist 
        UNION
        SELECT DISTINCT ascension_specialty AS itemName FROM table_masterlist;
        
ALTER TABLE table_itemid
	ADD itemID varchar(9) DEFAULT 'None';