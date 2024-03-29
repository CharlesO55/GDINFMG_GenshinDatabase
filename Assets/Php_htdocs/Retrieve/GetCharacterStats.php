<?php
require("../Connect.php");

$FIELD_character_name = $_POST["FIELD_character_name"];

$sql = "SELECT * FROM `view_character_stats` WHERE character_name = '".$FIELD_character_name."';";

$query = mysqli_query($CONNECTION, $sql) or die("[1] Character Stats Query failed");




if(mysqli_num_rows($query) == 1){
    $result = mysqli_fetch_assoc($query);

    $OUT_JSON = new stdClass();
    $OUT_JSON->Character_name = $result["character_name"];
    $OUT_JSON->Atk_base = $result["atk_1_20"];
    $OUT_JSON->Def_base = $result["def_1_20"];
    $OUT_JSON->Hp_base = $result["hp_1_20"];
    $OUT_JSON->Atk_max = $result["atk_90_90"];
    $OUT_JSON->Def_max = $result["def_90_90"];
    $OUT_JSON->Hp_max = $result["hp_90_90"];
    $OUT_JSON->Ascension_stat = $result["ascension"];
    $OUT_JSON->Ascension_base = $result["special_0"];
    $OUT_JSON->Ascension_max = $result["special_6"];

    echo json_encode($OUT_JSON);
} 
else{
    die("[2] Query returned incorrect: " . mysqli_num_rows($query) . " results.");
}
?>