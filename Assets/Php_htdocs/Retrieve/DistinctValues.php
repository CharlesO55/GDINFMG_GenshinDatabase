<?php
require('../Connect.php');

$FIELD_ColName = $_POST["FIELD_ColName"];

$sql = "SELECT DISTINCT `".($FIELD_ColName)."` FROM table_masterlist";
$queriedResults = mysqli_query($CONNECTION, $sql) or die("[1] Failed to query distinct values for " . $FIELD_ColName);

while ($row = mysqli_fetch_array($queriedResults)){
    echo $row[$FIELD_ColName] . '|';
}
?>