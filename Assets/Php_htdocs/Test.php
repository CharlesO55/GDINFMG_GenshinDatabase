<?php

require('Connect.php');

header('Content-Type: application/json; charset=utf-8');
$OUT_JSON = new stdClass();
$OUT_JSON->Name = "Jim";
$OUT_JSON->WifeName = "KIm";
$OUT_JSON->Age = 8;


echo json_encode($OUT_JSON);

?>