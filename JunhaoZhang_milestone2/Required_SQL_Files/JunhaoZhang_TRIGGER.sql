delimiter $$

create trigger updates after update on review for each row begin                 

set @counts_insert=(select count(*) from review where new.business_id=business_id);

set @sums_insert=(select avg(stars) from review where new.business_id=business_id);

update business set stars=@sums_insert, review_count=@counts_insert where business_id=new.business_id;

end$$


create trigger inserts after insert on review for each row begin       

set @counts_insert=(select count(*) from review where new.business_id=business_id);

set @sums_insert=(select avg(stars) from review where new.business_id=business_id);

update business set stars=@sums_insert, review_count=@counts_insert where business_id=new.business_id;


end$$

create trigger deletes after delete on review for each row begin       

set @counts_insert=(select count(*) from review where old.business_id=business_id);

set @sums_insert=(select avg(stars) from review where old.business_id=business_id);

update business set stars=@sums_insert, review_count=@counts_insert where business_id=old.business_id;

end$$


create trigger checkOpen before insert on review for each row begin      

set @opens=(select open from business where business.business_id = new.business_id);


if (@opens=0) then    

delete from review where review.review_id=new.review_id;/*, review.business_id=new.business_id, review.user_id=new.user_id;*/

end if;

end$$

/* 

Create trigger t1 after update on GameStats for each row begin                                
	
Set @team=(select teamcode from Players where Players.playerID=new.playerID);
Set @home=(select hometeam from games where GameS.gameID = new.gameID);
Set @away=(select awayteam from games where GameS.gameID = new.gameID);

If (@team=@home) then                          

Update games set homescore=homescore+new.points-old.points where games.gameID = new.gameID;
    
Else If (@team=@away) then                          

	Update games set awayscore=awayscore+new.points-old.points where games.gameID = new.gameID;

End if;

end if;

end$$


Create trigger t2 after update on gamestats for each row begin                             

If (new.points < old.points) then               

update gamestats set points=old.points where playerid=new.playerid;

end if;

end$$

*/

delimiter ;