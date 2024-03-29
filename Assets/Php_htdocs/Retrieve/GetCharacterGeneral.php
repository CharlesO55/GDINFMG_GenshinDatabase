<?php
require("../Connect.php");

$FIELD_character_name = $_POST["FIELD_character_name"];

$sql = "SELECT * FROM `view_character_general` WHERE character_name = '".$FIELD_character_name."';";

$query = mysqli_query($CONNECTION, $sql) or die("[1] Character General Query failed");

if(mysqli_num_rows($query) == 1){
    $result = mysqli_fetch_assoc($query);


    $OUT_JSON = new stdClass();
    $OUT_JSON->Character_name = $result["character_name"];
    $OUT_JSON->Rarity = $result["rarity"];
    $OUT_JSON->Vision = $result["vision"];
    $OUT_JSON->Weapon = $result["weapon_type"];
    $OUT_JSON->Region = $result["region"];
    $OUT_JSON->Constellation = $result["constellation"];
    $OUT_JSON->Affiliation = $result["affiliation"];
    $OUT_JSON->Description = $result["character_description"];

    echo json_encode($OUT_JSON);
} 
else{
    die("[2] Query returned incorrect: " . mysqli_num_rows($query) . " results.");
}
?>