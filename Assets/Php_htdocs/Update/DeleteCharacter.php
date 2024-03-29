<?php
require('../Connect.php');

$FIELD_Name = $_POST["FIELD_Name"];

$sql = "DELETE FROM table_masterlist WHERE character_name = '".$FIELD_Name."';";

mysqli_query($CONNECTION, $sql) or die("[1]: Failed to Delete " . $FIELD_Name);

echo "SUCCESS~ Deleted " . $FIELD_Name;
?>