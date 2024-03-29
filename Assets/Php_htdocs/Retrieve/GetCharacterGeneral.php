<?php
require("../Connect.php");

$FIELD_character_name = $_POST["FIELD_character_name"];

$sql = "SELECT * FROM `view_character_general` WHERE character_name = '".$FIELD_character_name."';";

$query = mysqli_query($CONNECTION, $sql) or die("[1] Character General Query failed");

if(mysqli_num_rows($query) == 1){
    $result = mysqli_fetch_assoc($query);

    $name = $result["character_name"]; 
    $rarity = $result["rarity"];
    $vision = $result["vision"];
    $weapon = $result["weapon_type"];
    $region = $result["region"];
    $constellation = $result["constellation"];
    $affiliation = $result["affiliation"];
    $description = $result["character_description"];

    echo $name . "|" . $rarity . "|". $vision . "|". $weapon . "|" . $region . "|". $constellation . "|". $affiliation . "|". $description;
} 
else{
    die("[2] Query returned incorrect: " . mysqli_num_rows($query) . " results.");
}
?>