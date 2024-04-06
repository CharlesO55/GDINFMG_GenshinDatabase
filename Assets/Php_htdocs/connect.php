<?php
    //CHANGE THIS DEPENDING ON THE NAME ASSIGNED IF DIFFERENT
    $MYSQL_HOST = "localhost";
    $MYSQL_USER = "id22012527_ayaya";
    $MYSQL_PASSWORD = "Ayaya_123";
    $DATABASE_NAME = "id22012527_genshin_db";

    // $MYSQL_HOST = "localhost";
    // $MYSQL_USER = "root";
    // $MYSQL_PASSWORD = "root";
    // $DATABASE_NAME = "genshin_db";


    $CONNECTION = mysqli_connect($MYSQL_HOST, $MYSQL_USER, $MYSQL_PASSWORD, $DATABASE_NAME);

    if(mysqli_connect_errno()) {
        die("Failed to connect. Recheck server database name");
    }
?>