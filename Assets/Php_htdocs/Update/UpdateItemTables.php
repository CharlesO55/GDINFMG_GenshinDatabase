<?php
require('../Connect.php');

$FIELD_BossItems = $_POST["FIELD_BossItems"];
$arrayItems = json_decode($FIELD_BossItems);


foreach($arrayItems->Items as $item){
    $itemName = $item->ItemName;
    $itemID = $item->ItemID;
    
    $checkQuery = "SELECT * FROM table_itemid WHERE itemName = '".$itemName."';";
    $resulltQuery = mysqli_query($CONNECTION, $checkQuery) or die("[1] Failed to search for existing record");
    
    
    if (mysqli_num_rows($resulltQuery) == 1){
        $sql = "UPDATE table_itemid SET itemID = '".$itemID."' WHERE itemName = '".$itemName."';";
        mysqli_query($CONNECTION, $sql) or die("[3] Failed to update item for: " . $itemName);
    }
    else{
        $sql = "INSERT INTO table_itemid(itemName, itemID) VALUES ('".$itemName."', '".$itemID."');";
        mysqli_query($CONNECTION, $sql) or die("[2] Failed to insert item: " . $itemName);
        echo "Insert " . $itemName;
    }
}


echo "SUCCESS~Updated items table";
?>