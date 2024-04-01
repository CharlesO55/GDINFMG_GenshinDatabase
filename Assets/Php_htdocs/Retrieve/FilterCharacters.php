<?php
require('../Connect.php');

$FIELD_Rarities = $_POST["FIELD_Rarities"];
$FIELD_Visions = $_POST["FIELD_Visions"];
$FIELD_Weapons = $_POST["FIELD_Weapons"];
$FIELD_Regions = $_POST["FIELD_Regions"];
$FIELD_Models = $_POST["FIELD_Models"];
$FIELD_CharacterName = $_POST["FIELD_CharacterName"];


$sqlQuery=('SELECT character_name, rarity FROM view_character_queries WHERE 
                rarity IN ('.$FIELD_Rarities.') AND
                vision IN ('.$FIELD_Visions.') AND 
                weapon_type IN ('.$FIELD_Weapons.') AND 
                region IN ('.$FIELD_Regions.') AND
                model IN ('.$FIELD_Models.') AND
                (CASE WHEN ("'.$FIELD_CharacterName.'") != "" THEN character_name LIKE ("'.$FIELD_CharacterName.'") ELSE TRUE END)
            ');

$queriedResults = mysqli_query($CONNECTION, $sqlQuery) or die("[1] Query filter Failed"); 

if(mysqli_num_rows($queriedResults) > 0){
    //FOUND SOMETHING
    echo "SUCCESS~";

    while ($currRes = mysqli_fetch_assoc($queriedResults)){
        $charName = $currRes["character_name"];
        $charRarity = $currRes["rarity"];

        echo "@" . $charName . "|" . $charRarity;
    }
}
else{
    echo "NONE FOUND";
}
?>