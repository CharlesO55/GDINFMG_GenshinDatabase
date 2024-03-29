<?php
require('../Connect.php');

$FIELD_Name = $_POST["FIELD_Name"];

$sql = "INSERT INTO table_masterlist(character_name) VALUES ('".$FIELD_Name."');";

mysqli_query($CONNECTION, $sql) or die("[1]: Failed to Insert " . $FIELD_Name);

echo "SUCCESS~ ADDED " . $FIELD_Name;
?>