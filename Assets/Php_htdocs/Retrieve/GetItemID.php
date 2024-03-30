<?php
require('../Connect.php');

$FIELD_Name = $_POST['FIELD_Name'];
$FIELD_Id = $_POST['FIELD_Id'];
if($FIELD_Name != ''){
    $sql = "SELECT * FROM table_itemid WHERE itemName = '".$FIELD_Name."';";
}
else{
    $sql = "SELECT * FROM table_itemid WHERE itemID = '".$FIELD_Id."';";
}


$result = mysqli_query($CONNECTION, $sql) or die("[1]: Failed to search item");

$parsedResults = mysqli_fetch_assoc($result);
echo $parsedResults['itemName'] . '|' . $parsedResults['itemID'];
?>