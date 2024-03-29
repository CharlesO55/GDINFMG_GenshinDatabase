<?php
require("../Connect.php");

$FIELD_character_name = $_POST["FIELD_character_name"];

$sql1 = "SELECT * FROM `view_character_stats` WHERE character_name = '".$FIELD_character_name."';";

$query1 = mysqli_query($CONNECTION, $sql1) or die("[1] Character General Query failed");




if(mysqli_num_rows($query1) == 1){
    $result = mysqli_fetch_assoc($query1);

    $name = $result["character_name"];
    $ascTrait = $result["ascension"];
    $ascValue = $result["special_6"];

    echo $name . "|" . $ascTrait . "|". $ascValue;
} 
else{
    die("[2] Query returned incorrect: " . mysqli_num_rows($query1) . " results.");
}
?>