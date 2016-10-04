create table business(

business_id char(22) primary key,
open bool default NULL,
type varchar(8),
city varchar(25),
longitude double,
state char(2),
stars float default 0,
latitude double,
review_count integer default 0,
name tinytext,
full_address tinytext

);


create table Cats(

	Parent char(100),
	Child Char(40),
    primary key(Parent, Child)
    
);

create table user(

user_id char(22) primary key,
yelping_since DATe,
funny integer default 0,
useful integer default 0,
cool integer default 0,
review_count integer default 0,
name tinytext,
fans integer default 0,
average_stars float default 0,
type varchar(8)

);

create table review(

funny integer default 0,
useful integer default 0,
cool integer default 0,
stars float default 0,
date date,
text text,
type varchar(8),

business_id char(22),
user_id char(22),
review_id char(22) unique,

foreign key (business_id) references business(business_id),
foreign key (user_id) references user(user_id), 
primary key (business_id, user_id, review_id)

);

create table Category(

	business_id char(22),
    category char(40),
    foreign key (business_id) references business(business_id),
    primary key(business_id, category)

);

