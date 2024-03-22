# GDINFMG_GenshinDatabase
 Genshin database tool inside Unity

## Setting up MAMP/PhpMyAdmin...
1. Open PhpMyAdmin and create a Database.
2. Import the sql file Setup/ImportSetup.sql.
3. Verify that table_masterlist is intact with 88 rows.

## Important notes:
1. Always retrieve only from the views.
2. Only ever perform CUD operations on table_masterlist.