delimiter $$
/* insert */
create trigger Cons_Insert before insert on business for each row begin        

if ((new.Alcohol<>'none' and new.Alcohol<>'full_bar' and new.Alcohol<>'beer_and_wine' and new.Alcohol is not NULL)
	or (new.Attire<>'casual' and new.Attire<>'dressy' and new.Attire<>'formal' and new.Attire is not NULL)
	or (new.Wi_Fi<>'no' and new.Wi_Fi<>'free' and new.Wi_Fi<>'paid' and new.Wi_Fi is not NULL)
    or (new.BYOB_Corkage<>'yes_free' and new.BYOB_Corkage<>'no' and new.BYOB_Corkage<>'yes_corkage' and new.BYOB_Corkage is not NULL)
    or (new.Noise_Level<>'average' and new.Noise_Level<>'loud' and new.Noise_Level<>'very_loud' and new.Noise_Level<>'quiet'  and new.Noise_Level is not NULL)
    or (new.type<>'business' and new.type<>'review' and new.type<>'user' and new.type is not NULL)
)     
then      

delete from business where business.business_id=new.business_id;

end if;   

end$$

/* update */
create trigger Cons_Update after update on business for each row begin        

if ((new.Alcohol<>'none' and new.Alcohol<>'full_bar' and new.Alcohol<>'beer_and_wine' and new.Alcohol is not NULL)
	or (new.Attire<>'casual' and new.Attire<>'dressy' and new.Attire<>'formal' and new.Attire is not NULL)
	or (new.Wi_Fi<>'no' and new.Wi_Fi<>'free' and new.Wi_Fi<>'paid' and new.Wi_Fi is not NULL)
    or (new.BYOB_Corkage<>'yes_free' and new.BYOB_Corkage<>'no' and new.BYOB_Corkage<>'yes_corkage' and new.BYOB_Corkage is not NULL)
    or (new.Noise_Level<>'average' and new.Noise_Level<>'loud' and new.Noise_Level<>'very_loud' and new.Noise_Level<>'quiet'  and new.Noise_Level is not NULL)
    or (new.type<>'business' and new.type<>'review' and new.type<>'user' and new.type is not NULL)
)     
then       

update business set business.Alcohol=old.Alcohol, business.Attire=old.Attire, business.Wi_Fi=old.Wi_Fi, 
business.BYOB_Corkage=old.BYOB_Corkage,
business.Noise_Level = old.Noise_Level, business.type=old.type;

end if;   

end$$

/*

create trigger TF_Insert before insert on business for each row begin          

if (

(new.Take_out <> 'true' and  new.Take_out <> 'false' and new.Take_out is not NULL) or 
(new.Drive_Thru <> 'true' and new.Drive_Thru <> 'false' and new.Drive_Thru is not NULL) or 
(new.dessert <> 'true' and new.dessert <> 'false' and new.dessert is not NULL) or
(new.latenight <> 'true' and new.latenight <> 'false' and new.latenight is not NULL) or
(new.lunch <> 'true' and new.lunch <> 'false' and new.lunch is not NULL) or
(new.dinner <> 'true' and new.dinner <> 'false' and new.dinner is not NULL) or 
(new.brunch  <> 'true' and new.brunch <> 'false' and new.brunch is not NULL) or 
(new.breakfast <> 'true' and new.breakfast <> 'false' and new.breakfast is not NULL) or 
(new.Caters  <> 'true' and new.Caters <> 'false' and new.Caters is not NULL) or 
(new.Takes_Reservations  <> 'true' and new.Takes_Reservations <> 'false' and new.Takes_Reservations  is not NULL) or 
(new.Delivery  <> 'true' and new.Delivery <> 'false' and new.Delivery is not NULL) or 
(new.Ambience  <> 'true' and new.Ambience <> 'false' and new.Ambience is not NULL) or 
(new.romantic  <> 'true' and new.romantic <> 'false' and new.romantic is not NULL) or 
(new.intimate  <> 'true' and new.intimate <> 'false' and new.intimate is not NULL) or 
(new.classy  <> 'true' and new.classy  <> 'false' and new.classy  is not NULL) or 
(new.hipster  <> 'true' and new.hipster <> 'false' and new.hipster is not NULL) or 
(new.divey  <> 'true' and new.divey <> 'false' and new.divey is not NULL) or 
(new.touristy  <> 'true' and new.touristy <> 'false' and new.touristy is not NULL) or 
(new.trendy  <> 'true' and new.trendy  <> 'false' and new.trendy  is not NULL) or 
(new.upscale  <> 'true' and new.upscale <> 'false' and new.upscale is not NULL) or 
(new.casual  <> 'true' and new.casual <> 'false' and new.casual is not NULL) or 
(new.garage  <> 'true' and new.garage <> 'false' and new.garage is not NULL) or 
(new.street  <> 'true' and new.street <> 'false' and new.street is not NULL) or 
(new.validated  <> 'true' and new.validated <> 'false' and new.validated is not NULL) or 
(new.lot  <> 'true' and new.lot <> 'false' and new.lot is not NULL) or 
(new.valet  <> 'true' and new.valet <> 'false' and new.valet is not NULL) or 

(new.Music  <> 'true' and new.Music <> 'false' and new.Music is not NULL) or 
(new.dj  <> 'true' and new.dj <> 'false' and new.dj is not NULL) or 
(new.BYOB  <> 'true' and new.BYOB <> 'false' and new.BYOB is not NULL) or 
(new.Corkage <> 'true' and new.Corkage <> 'false' and new.Corkage is not NULL) or 
(new.BYOB_Corkage <> 'true' and new.BYOB_Corkage <>'false' and new.BYOB_Corkage is not NULL) or 
(new.background_music  <> 'true' and new.background_music <> 'false' and new.background_music is not NULL) or 
(new.jukebox  <> 'true' and new.jukebox <> 'false' and new.jukebox is not NULL) or 
(new.live  <> 'true' and new.live <> 'false' and new.live is not NULL) or 
(new.video  <> 'true' and new.video <> 'false' and new.video is not NULL) or 
(new.karaoke <> 'true' and new.karaoke <> 'false' and new.karaoke is not NULL) or 
(new.dairy_free <> 'true' and new.dairy_free <> 'false' and new.dairy_free is not NULL) or 
(new.gluten_free  <> 'true' and new.gluten_free <> 'false' and new.gluten_free is not NULL) or 
(new.vegan  <> 'true' and new.vegan <> 'false' and new.vegan is not NULL) or 
(new.kosher <> 'true' and new.kosher <> 'false' and new.kosher is not NULL) or 
(new.halal  <> 'true' and new.halal <> 'false' and new.halal is not NULL) or 
(new.soy_free  <> 'true' and new.soy_free <> 'false' and new.soy_free is not NULL) or 
(new.vegetarian  <> 'true' and new.vegetarian <> 'false' and new.vegetarian is not NULL) or 

(new.Waiter_Service <> 'true' and new.Waiter_Service <> 'false' and new.Waiter_Service is not NULL) or 
(new.Has_TV <> 'true' and new.Has_TV <> 'false' and new.Has_TV is not NULL) or 
(new.Outdoor_Seating <> 'true' and new.Outdoor_Seating <> 'false' and new.Outdoor_Seating is not NULL) or 
(new.Accepts_Credit_Cards <> 'true' and new.Accepts_Credit_Cards <> 'false' and new.Accepts_Credit_Cards is not NULL) or 
(new.Good_for_Kids <> 'true' and new.Good_for_Kids <> 'false' and new.Good_for_Kids is not NULL) or 
(new.Good_For_Groups  <> 'true' and new.Good_For_Groups <> 'false' and new.Good_For_Groups is not NULL) 

)
then

delete from business where business.business_id=new.business_id;

end if;  
end$$



create trigger TF_Update after update on business for each row begin        

if (

(new.Take_out <> 'true' and  new.Take_out <> 'false' and new.Take_out is not NULL) or 
(new.Drive_Thru <> 'true' and new.Drive_Thru <> 'false' and new.Drive_Thru is not NULL) or 
(new.dessert <> 'true' and new.dessert <> 'false' and new.dessert is not NULL) or
(new.latenight <> 'true' and new.latenight <> 'false' and new.latenight is not NULL) or
(new.lunch <> 'true' and new.lunch <> 'false' and new.lunch is not NULL) or
(new.dinner <> 'true' and new.dinner <> 'false' and new.dinner is not NULL) or 
(new.brunch  <> 'true' and new.brunch <> 'false' and new.brunch is not NULL) or 
(new.breakfast <> 'true' and new.breakfast <> 'false' and new.breakfast is not NULL) or 
(new.Caters  <> 'true' and new.Caters <> 'false' and new.Caters is not NULL) or 
(new.Takes_Reservations  <> 'true' and new.Takes_Reservations <> 'false' and new.Takes_Reservations  is not NULL) or 
(new.Delivery  <> 'true' and new.Delivery <> 'false' and new.Delivery is not NULL) or 
(new.Ambience  <> 'true' and new.Ambience <> 'false' and new.Ambience is not NULL) or 
(new.romantic  <> 'true' and new.romantic <> 'false' and new.romantic is not NULL) or 
(new.intimate  <> 'true' and new.intimate <> 'false' and new.intimate is not NULL) or 
(new.classy  <> 'true' and new.classy  <> 'false' and new.classy  is not NULL) or 
(new.hipster  <> 'true' and new.hipster <> 'false' and new.hipster is not NULL) or 
(new.divey  <> 'true' and new.divey <> 'false' and new.divey is not NULL) or 
(new.touristy  <> 'true' and new.touristy <> 'false' and new.touristy is not NULL) or 
(new.trendy  <> 'true' and new.trendy  <> 'false' and new.trendy  is not NULL) or 
(new.upscale  <> 'true' and new.upscale <> 'false' and new.upscale is not NULL) or 
(new.casual  <> 'true' and new.casual <> 'false' and new.casual is not NULL) or 
(new.garage  <> 'true' and new.garage <> 'false' and new.garage is not NULL) or 
(new.street  <> 'true' and new.street <> 'false' and new.street is not NULL) or 
(new.validated  <> 'true' and new.validated <> 'false' and new.validated is not NULL) or 
(new.lot  <> 'true' and new.lot <> 'false' and new.lot is not NULL) or 
(new.valet  <> 'true' and new.valet <> 'false' and new.valet is not NULL) or 

(new.Music  <> 'true' and new.Music <> 'false' and new.Music is not NULL) or 
(new.dj  <> 'true' and new.dj <> 'false' and new.dj is not NULL) or 
(new.BYOB  <> 'true' and new.BYOB <> 'false' and new.BYOB is not NULL) or 
(new.Corkage <> 'true' and new.Corkage <> 'false' and new.Corkage is not NULL) or 
(new.BYOB_Corkage <> 'true' and new.BYOB_Corkage <>'false' and new.BYOB_Corkage is not NULL) or 
(new.background_music  <> 'true' and new.background_music <> 'false' and new.background_music is not NULL) or 
(new.jukebox  <> 'true' and new.jukebox <> 'false' and new.jukebox is not NULL) or 
(new.live  <> 'true' and new.live <> 'false' and new.live is not NULL) or 
(new.video  <> 'true' and new.video <> 'false' and new.video is not NULL) or 
(new.karaoke <> 'true' and new.karaoke <> 'false' and new.karaoke is not NULL) or 
(new.dairy_free <> 'true' and new.dairy_free <> 'false' and new.dairy_free is not NULL) or 
(new.gluten_free  <> 'true' and new.gluten_free <> 'false' and new.gluten_free is not NULL) or 
(new.vegan  <> 'true' and new.vegan <> 'false' and new.vegan is not NULL) or 
(new.kosher <> 'true' and new.kosher <> 'false' and new.kosher is not NULL) or 
(new.halal  <> 'true' and new.halal <> 'false' and new.halal is not NULL) or 
(new.soy_free  <> 'true' and new.soy_free <> 'false' and new.soy_free is not NULL) or 
(new.vegetarian  <> 'true' and new.vegetarian <> 'false' and new.vegetarian is not NULL) or 

(new.Waiter_Service <> 'true' and new.Waiter_Service <> 'false' and new.Waiter_Service is not NULL) or 
(new.Has_TV <> 'true' and new.Has_TV <> 'false' and new.Has_TV is not NULL) or 
(new.Outdoor_Seating <> 'true' and new.Outdoor_Seating <> 'false' and new.Outdoor_Seating is not NULL) or 
(new.Accepts_Credit_Cards <> 'true' and new.Accepts_Credit_Cards <> 'false' and new.Accepts_Credit_Cards is not NULL) or 
(new.Good_for_Kids <> 'true' and new.Good_for_Kids <> 'false' and new.Good_for_Kids is not NULL) or 
(new.Good_For_Groups  <> 'true' and new.Good_For_Groups <> 'false' and new.Good_For_Groups is not NULL) 

)
then

update business set business.Alcohol=old.Alcohol, business.Attire=old.Attire, business.Wi_Fi=old.Wi_Fi, 
business.BYOB_Corkage=old.BYOB_Corkage,
business.Noise_Level = old.Noise_Level, business.type=old.type;

delete from business where business.business_id=new.business_id;

end if;  
end$$



*/




delimiter ;