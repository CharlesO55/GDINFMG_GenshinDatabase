<?php
require('../Connect.php');

$sql1 = "DROP TABLE IF EXISTS table_masterlist;";
$sql2 = "CREATE TABLE table_masterlist AS
SELECT 	import_general.* , 
		import_descriptions.character_description, 
        view_revenue_summary.total_banner_runs, view_revenue_summary.last_banner_appearance, view_revenue_summary.total_revenue FROM 
	import_general LEFT JOIN import_descriptions 
		ON import_general.character_name = import_descriptions.character_name
    LEFT JOIN view_revenue_summary
    	ON import_general.character_name = view_revenue_summary.character_name
	ORDER BY character_name;";

# Remove unnecessary columns
$sql3 = "ALTER TABLE table_masterlist 
    ADD COLUMN `character_id` INT AUTO_INCREMENT UNIQUE FIRST,
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
    DROP COLUMN special_5;";

#COPY TRAVELER'S DESCRIPTION
$sql4 = "UPDATE table_masterlist 
	        SET character_description = (SELECT character_description FROM import_descriptions WHERE character_name = 'Traveler') 
        WHERE character_name LIKE '%Traveler%';";


mysqli_query($CONNECTION, $sql1) or die("[1]: Masterlist drop duplicate failed");
mysqli_query($CONNECTION, $sql2) or die("[2]: Masterlist copy imports failed");
mysqli_query($CONNECTION, $sql3) or die("[3]: Masterlist drop cols failed");
mysqli_query($CONNECTION, $sql4) or die("[4]: Masterlist copy traveler description failed");

echo "SUCCESS ~ MasterList Reloaded";
?>