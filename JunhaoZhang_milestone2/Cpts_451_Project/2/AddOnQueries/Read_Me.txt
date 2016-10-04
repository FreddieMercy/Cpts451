1. All required SQL files and ER model diagram and JunhaoZhang_TableSizes.txt are in the folder /../Milestone2/Required_SQL_Files
2. Run /../Milestone2/ParseYelpData-CptS451/ParseYelpData to generate additional files that are needed
3. After finish running /../Milestone2/ParseYelpData-CptS451/ParseYelpData,it may take about 5 mins, then run the following query in MySQL:

create database milestone2; /* this is the database we are going to use in this milestone */

use milestone2;

source /../Milestone2/Required_SQL_Files/JunhaoZhang_DDL.sql;

source /../Milestone2/ParseYelpData-CptS451/ParseYelp/yelp/sub/req/Create_Cats.sql;

source /../Milestone2/ParseYelpData-CptS451/ParseYelp/yelp/business.sql;

source /../Milestone2/ParseYelpData-CptS451/ParseYelp/yelp/sub/Cats.sql;
source /../Milestone2/ParseYelpData-CptS451/ParseYelp/yelp/sub/insertIntoCatTables.sql;

source /../Milestone2/ParseYelpData-CptS451/ParseYelp/yelp/sub/Category.sql;


source /../Milestone2/ParseYelpData-CptS451/ParseYelp/yelp/User.sql;

source /../Milestone2/ParseYelpData-CptS451/ParseYelp/yelp/review.sql;


source /../Milestone2/Required_SQL_Files/JunhaoZhang_TRIGGER.sql;

source /../Milestone2/Required_SQL_Files/JunhaoZhang_UPDATE.sql;







source C:/Users/junhao.zhang.freddie/gitHub/Cpts451/JunhaoZhang_milestone2/ClearUnnecessaryTables/ParseYelp/yelp/ClearUnnecessaryTables.sql;






4. Then, run /../Milestone2/Cpts451_Project/Cpts451_Project to use the GUI