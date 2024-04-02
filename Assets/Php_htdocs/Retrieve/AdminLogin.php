<?php
require('../Connect.php');

$FIELD_ID = $_POST["FIELD_ID"];
$FIELD_Password = $_POST["FIELD_Password"];

$sql = "SELECT * FROM table_admins WHERE id = '".$FIELD_ID."';";
$result = mysqli_query($CONNECTION, $sql) or die("[1] Sql failed");

if(mysqli_num_rows($result) != 1){
    die("[2]: No user found");
}


$parsedRes = mysqli_fetch_assoc($result);

//RECREATE THE HASH
if(crypt($FIELD_Password, $parsedRes['salt']) == $parsedRes['hash']){
    echo 'SUCCESS' . ' Admin logged in';
}
else{
    echo 'INCORRECT';
}
?>