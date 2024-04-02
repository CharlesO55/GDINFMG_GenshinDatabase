<?php
require("../Connect.php");

$FIELD_ItemID = $_POST['FIELD_ItemID'];//100021;

$sql = "SELECT * FROM table_masterlist WHERE 
ascension_specialty = (SELECT itemName FROM table_itemid WHERE itemID = '".$FIELD_ItemID."') OR
ascension_material = (SELECT itemName FROM table_itemid WHERE itemID = '".$FIELD_ItemID."') OR
ascension_boss = (SELECT itemName FROM table_itemid WHERE itemID = '".$FIELD_ItemID."') OR
ascension_gem  = (SELECT itemName FROM table_itemid WHERE itemID = '".$FIELD_ItemID."')";

$result = mysqli_query($CONNECTION, $sql) or die("[1] Sql query failed");


// $OUT_Result = new stdClass();
// $OUT_Result->Names = array();
// $OUT_Result->Rarities = array();

$OUT_List = array();

while($parsed = mysqli_fetch_assoc($result)){
    // array_push($OUT_Result->Names, $parsed["character_name"]);
    // array_push($OUT_Result->Rarities, $parsed["rarity"]);

    array_push($OUT_List, [$parsed["character_name"], $parsed["rarity"]] );
}
#echo json_encode($OUT_Result);
echo json_encode($OUT_List);

?>