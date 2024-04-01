<?php
require('../Connect.php');

$FIELD_ColName = 'character_name';

//SELECT COUNT(region) FROM `table_masterlist` WHERE region = 'Liyue';

/*
$sql = "SELECT character_name, atk_90_90, def_90_90 FROM table_masterlist WHERE atk_90_90 = (SELECT MAX(atk_90_90) FROM table_masterlist) OR def_90_90 = (SELECT MAX(def_90_90) FROM table_masterlist);";
$query = mysqli_query($CONNECTION, $sql) or die("[1] Failed to count values for " . $FIELD_ColName);

echo json_encode($query);
while ($result = mysqli_fetch_assoc($query)){
    echo $result['character_name'] . '|';
    echo $result['atk_90_90'] . '|';
    echo $result['def_90_90'] . '|';
}*/



$sql = "SELECT `".$FIELD_ColName."`, COUNT(`".($FIELD_ColName)."`) AS `occurences` FROM table_masterlist GROUP BY `".($FIELD_ColName)."`";
$query = mysqli_query($CONNECTION, $sql) or die("[1] Failed to count values for " . $FIELD_ColName);

if(mysqli_num_rows($query) < 1){
    die("[2] Failed to find any counts for " .$FIELD_ColName);
}

$OUT_JSON = new stdClass();
$OUT_JSON->Results = array();

while ($result = mysqli_fetch_assoc($query)){
    $key = $result[$FIELD_ColName];
    $value = $result['occurences'];
    $OUT_JSON->Results[$key] = $value;
}
echo json_encode($OUT_JSON->Results);
?>