<?php
require("../Connect.php");

$FIELD_character_name = $_POST["FIELD_character_name"];

$sql = "SELECT * FROM `view_leveling_reqs` WHERE character_name = '".$FIELD_character_name."';";

$query = mysqli_query($CONNECTION, $sql) or die("[1] Character Stats Query failed");


if(mysqli_num_rows($query) == 1){
    $result = mysqli_fetch_assoc($query);

    $OUT_JSON = new stdClass();
    $OUT_JSON->Character_Name = $result["character_name"];
    $OUT_JSON->Name_Gather = $result["ascension_specialty"];
    $OUT_JSON->Name_Mob = $result["ascension_material"];
    $OUT_JSON->Name_Boss = $result["ascension_boss"];
    $OUT_JSON->Name_Gem = $result["ascension_gem"];

    echo json_encode($OUT_JSON);
} 
else{
    die("[2] Query returned incorrect: " . mysqli_num_rows($query) . " results.");
}
?>