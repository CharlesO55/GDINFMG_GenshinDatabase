<?php
require('../Connect.php');

$sql1 = "DROP TABLE IF EXISTS table_admins;";
$sql2 = "CREATE TABLE table_admins(
	id int UNIQUE,
    salt varchar(50),
    hash varchar(200)
);";

$admins = array([123456, 'Password'], [123, '123']);

mysqli_query($CONNECTION, $sql1) or die("[1] Failed to check existing admins");
mysqli_query($CONNECTION, $sql2) or die("[2] Failed to create admin table");


foreach($admins as $x){
    $id = $x[0];

    $salt = "\$5\$rounds=5000\$" . "any_value_here" . "\$";
    $hash = crypt($x[1], $salt);

    $sql3 = "INSERT INTO table_admins(id, salt, hash) VALUES('".$id."', '".$salt."', '".$hash."');";
    mysqli_query($CONNECTION, $sql3) or die("[3] Failed to insert admins");
}

echo "[SUCCESS] Created admins table";
?>