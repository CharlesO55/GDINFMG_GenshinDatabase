<?php
    //CHANGE THIS DEPENDING ON THE NAME ASSIGNED IF DIFFERENT
    $DATABASE_NAME = "genshin_db";

    $CONNECTION = mysqli_connect("localhost", "root", "root", $DATABASE_NAME);

    if(mysqli_connect_errno()) {
        die("Failed to connect. Recheck server database name");
    }
?>