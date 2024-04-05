<?php
    //CHANGE THIS DEPENDING ON THE NAME ASSIGNED IF DIFFERENT
    $DATABASE_HOST = "sql209.infinityfree.com";
    $DATABASE_USERNAME = "if0_36311094";
    $DATABASE_PASSWORD = "GDINFMGPassword";
    $DATABASE_NAME = "if0_36311094_genshin_db";

    $CONNECTION = mysqli_connect($DATABASE_HOST, $DATABASE_USERNAME, $DATABASE_PASSWORD, $DATABASE_NAME);

    if(mysqli_connect_errno()) {
        die("Failed to connect. Recheck server database name");
    }
?>