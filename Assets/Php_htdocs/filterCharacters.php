<?php
require('connect.php');
include('distinctValues.php');




$FIELD_Rarities = $_POST["FIELD_Rarities"];
$FIELD_Visions = $_POST["FIELD_Visions"];
$FIELD_Weapons = $_POST["FIELD_Weapons"];
$FIELD_Regions = $_POST["FIELD_Regions"];
$FIELD_Models = $_POST["FIELD_Models"];

echo $FIELD_Regions;
$testquery=('SELECT * FROM view_character_queries WHERE 
                vision IN ('.$FIELD_Visions.') AND 
                weapon_type IN ('.$FIELD_Weapons.') AND 
                region IN ('.$FIELD_Regions.') AND
                model IN ('.$FIELD_Models.')' ); 



$queriedResults = mysqli_query($CONNECTION, $testquery); 

if(mysqli_num_rows($queriedResults) > 0){
    //FOUND SOMETHING
    echo "SUCCESS~";

    while ($currRes = mysqli_fetch_assoc($queriedResults)){
        $currChar = $currRes["character_name"];
    
        echo "@" . $currChar;
    }
}
else{
    echo "NONE FOUND";
}
?>