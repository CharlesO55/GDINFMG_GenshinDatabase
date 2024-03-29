<?php
require("../Connect.php");

$FIELD_character_name = "Furina";#$_POST["FIELD_character_name"];

$sql = "SELECT * FROM `view_character_stats` WHERE character_name = '".$FIELD_character_name."';";

$query = mysqli_query($CONNECTION, $sql) or die("[1] Character Stats Query failed");




if(mysqli_num_rows($query) == 1){
    $result = mysqli_fetch_assoc($query);

    $name = $result["character_name"];
    $ascTrait = $result["ascension"];
    $ascValue = $result["special_6"];

    echo $name . "|" . $ascTrait . "|". $ascValue;
} 
else{
    die("[2] Query returned incorrect: " . mysqli_num_rows($query) . " results.");
}
?>