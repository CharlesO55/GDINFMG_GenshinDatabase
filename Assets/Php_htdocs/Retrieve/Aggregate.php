<?php
require("../Connect.php");

$FIELD_SortingColumn = $_POST['FIELD_SortingColumn'];

$sql = "SELECT (".$FIELD_SortingColumn.") AS sortingColValue, 
	COUNT('".$FIELD_SortingColumn."') AS `counts`, 
	MAX(atk_90_90) AS `max_atk`,
	MAX(def_90_90) AS `max_def`,
	MAX(hp_90_90) AS `max_hp`
FROM table_masterlist GROUP BY (".$FIELD_SortingColumn.");";

$query = mysqli_query($CONNECTION, $sql) or die("[1] Aggregation failed");


$OUT_Results = array();
while($result = mysqli_fetch_assoc($query)){
    $newRes = new stdClass();    

    $newRes->SortingColValue = $result['sortingColValue'];
    $newRes->CharacterCounts = $result['counts'];
    $newRes->MaxAtk = $result['max_atk'];
    $newRes->MaxDef = $result['max_def'];
    $newRes->MaxHP = $result['max_hp'];


    $atkQuery = mysqli_query($CONNECTION, "SELECT character_name FROM table_masterlist WHERE atk_90_90 = '".$newRes->MaxAtk."' AND (".$FIELD_SortingColumn.") = '".$newRes->SortingColValue."';") or die("[2]: Failed to query max atk name");
    $defQuery = mysqli_query($CONNECTION, "SELECT character_name FROM table_masterlist WHERE def_90_90 = '".$newRes->MaxDef."' AND (".$FIELD_SortingColumn.") = '".$newRes->SortingColValue."';") or die("[3]: Failed to query max def name");
    $hpQuery = mysqli_query($CONNECTION, "SELECT character_name FROM table_masterlist WHERE hp_90_90 = '".$newRes->MaxHP."' AND (".$FIELD_SortingColumn.") = '".$newRes->SortingColValue."';") or die("[4]: Failed to query max hp name");

    if(mysqli_num_rows($atkQuery) < 1) {die("[5] Failed to find max attacker name");}
    if(mysqli_num_rows($defQuery) < 1) {die("[6] Failed to find max def name");}
    if(mysqli_num_rows($hpQuery) < 1) {die("[7] Failed to find max hp name");}
    $newRes->MaxAtkName = mysqli_fetch_assoc($atkQuery)['character_name'];
    $newRes->MaxDefName = mysqli_fetch_assoc($defQuery)['character_name'];
    $newRes->MaxHPName = mysqli_fetch_assoc($hpQuery)['character_name'];

    array_push($OUT_Results, $newRes);
}
echo json_encode($OUT_Results);
?>