drop database milestone2;
create database milestone2;
use milestone2;

source C:/Users/junhao.zhang.freddie/gitHub/Cpts451/JunhaoZhang_milestone2/Required_SQL_Files/JunhaoZhang_DDL.sql;
source C:/Users/junhao.zhang.freddie/gitHub/Cpts451/JunhaoZhang_milestone2/Cpts_451_Project/1/ParseYelpData-CptS451/ParseYelp/yelp/sub/req/Create_Cats.sql;
source C:/Users/junhao.zhang.freddie/gitHub/Cpts451/JunhaoZhang_milestone2/Cpts_451_Project/1/ParseYelpData-CptS451/ParseYelp/yelp/business.sql;
alter table business add column zipcode char(5) default NULL;
source C:/Users/junhao.zhang.freddie/gitHub/Cpts451/JunhaoZhang_milestone2/Cpts_451_Project/1/ParseYelpData-CptS451/ParseYelp/yelp/sub/req/zip.sql;
source C:/Users/junhao.zhang.freddie/gitHub/Cpts451/JunhaoZhang_milestone2/Cpts_451_Project/1/ParseYelpData-CptS451/ParseYelp/yelp/sub/Cats.sql;

source C:/Users/junhao.zhang.freddie/gitHub/Cpts451/JunhaoZhang_milestone2/Cpts_451_Project/1/ParseYelpData-CptS451/ParseYelp/yelp/sub/insertIntoCatTables.sql;
source C:/Users/junhao.zhang.freddie/gitHub/Cpts451/JunhaoZhang_milestone2/Cpts_451_Project/1/ParseYelpData-CptS451/ParseYelp/yelp/sub/Category.sql;

source C:/Users/junhao.zhang.freddie/gitHub/Cpts451/JunhaoZhang_milestone2/Cpts_451_Project/1/ParseYelpData-CptS451/ParseYelp/yelp/User.sql;
source C:/Users/junhao.zhang.freddie/gitHub/Cpts451/JunhaoZhang_milestone2/Cpts_451_Project/1/ParseYelpData-CptS451/ParseYelp/yelp/review.sql;

source C:/Users/junhao.zhang.freddie/gitHub/Cpts451/JunhaoZhang_milestone2/Required_SQL_Files/JunhaoZhang_TRIGGER.sql;
source C:/Users/junhao.zhang.freddie/gitHub/Cpts451/JunhaoZhang_milestone2/Required_SQL_Files/JunhaoZhang_UPDATE.sql;



/*
source C:/Users/junhao.zhang.freddie/Desktop/cons.sql;
*/
