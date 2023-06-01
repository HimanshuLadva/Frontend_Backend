USE MOVIES_W3;

----------------------------------------------------------------
--query--
--1
select mov_title, mov_year from movie;

--2
select mov_year from movie where mov_title = 'American Beauty';

--3
select mov_title from movie where mov_year = 1999;

--4
select mov_title from movie where mov_year < 1998;

--5
select mov_title , rev_name from movie M
inner join rating R on M.mov_id = R.mov_id
inner join reviewer RV on R.rev_id = RV.rev_id;

--6
select rev_name from reviewer	
where  rev_id = ANY (select rev_id from rating where rev_stars >= 7);

--7
select  mov_title , num_o_ratings from movie
inner join rating on movie.mov_id = rating.mov_id 
where rating.num_o_ratings IS NULL;
					
--8
select mov_title from movie where mov_id =905 OR mov_id =907 OR mov_id = 917;

--9
select mov_title,mov_id ,mov_year from movie where mov_title like '%Boogie nights%' ORDER BY mov_year;

--10
select act_id from actor where act_fname ='Woody' AND act_lname='Alle';

---------------------------------------------------------------------------------
--SUB QUERIES--
--1
select * from actor 
where act_id in 
(select act_id from movie_cast 
where mov_id in (select mov_id from movie where mov_title ='Annie Hall'));

--2
select dir_fname, dir_lname from director
where dir_id in (select dir_id from movie_direction 
where mov_id in (select mov_id from movie where mov_title='Eyes Wide Shut'));

--3
select mov_title , mov_year ,mov_time,mov_dt_rel,mov_rel_country from movie 
where mov_rel_country!='UK';

--4
select   mov_title,mov_year,mov_dt_rel,dir_fname,dir_lname,act_fname,act_lname from movie, director ,actor
where mov_id in (select mov_id from rating 
where rev_id in (select rev_id from reviewer 
where rev_name = ' ' )) AND act_id in(select act_id from movie_cast 
where mov_id = movie.mov_id) AND dir_id in (select dir_id from movie_direction 
where mov_id = movie.mov_id);

--5
select mov_title from movie	
where mov_id in (select mov_id from movie_direction 
where dir_id in (select dir_id from director 
where dir_fname='Woody' AND dir_lname = 'Allen'));

--6
select mov_year from movie
where mov_id in (select mov_id from rating where rev_stars >3 ) ORDER BY mov_year;

--7
select mov_title ,mov_id from movie
where mov_id not in (select mov_id from rating where mov_id = movie.mov_id)

--8
select rev_name from reviewer
where rev_id in (select rev_id from rating where rev_stars is null)

--9
select  rev_name, mov_title, rev_stars from reviewer, movie, rating
where rev_stars in ( select rev_stars from rating 
where mov_id= movie.mov_id AND rev_stars!='' AND num_o_ratings!=' ' )AND reviewer.rev_id in (select rev_id from rating 
where mov_id = movie.mov_id)AND rev_name in (select rev_name from reviewer 
where rev_id= rating.rev_id  AND reviewer.rev_name!=' ')
ORDER BY rev_name,mov_title,rev_stars;

--10
select rev_name ,mov_title from reviewer , movie
where rev_id in (select R.rev_id from rating R GROUP BY rev_id HAVinG  count(r.rev_id) >1  )
AND rev_id in (select rev_id from rating where mov_id = movie.mov_id) GROUP BY rev_name,mov_title;

--11
select mov_title ,MAX(rev_stars) AS maximum_stars from movie ,rating
where movie.mov_id in(select mov_id from rating 
where rev_stars <= ANY(select rev_stars from rating))AND rev_stars in( select rev_stars from rating 
where mov_id= movie.mov_id) GROUP BY mov_title,rev_stars;

--12
select rev_name ,rev_id from reviewer
where rev_id in (select rev_id from rating 
where mov_id in (select mov_id from movie 
where mov_title ='American Beauty'));

--13
select mov_title from movie 
where mov_id in (select mov_id from rating 
where rev_id in (select rev_id from reviewer 
where rev_name != 'Paul Monks'));

--14
select mov_title, Min(rev_stars) AS minimum_stars, rev_name from movie, rating, reviewer
where movie.mov_id in (select mov_id from rating 
where rev_stars >= ANY (select rev_stars from rating)) AND rev_stars in (select rev_stars from rating 
where mov_id = movie.mov_id) AND rev_name in (select rev_name from reviewer
where rev_id in (select rev_id from rating 
where mov_id = movie.mov_id)) GROUP BY mov_title, rev_stars, rev_name;

--15
select mov_title from movie 
where mov_id in (select mov_id from movie_direction 
where dir_id in (select dir_id from director
where dir_fname = 'James' AND dir_lname = 'Cameron'));

--16
select mov_title from movie 
where mov_id in (select mov_id from movie_cast GROUP BY mov_id, act_id 
HAVinG COUNT(mov_id) >= 1 AND act_id in (select act_id from movie_cast GROUP BY act_id 
HAVinG COUNT(act_id) >= 2));

-----------------------------------------------------------------------------------------
--SQL joinS--
--1
select rev_name from reviewer 
inner join rating on reviewer.rev_id = rating.rev_id
where rating.rev_stars IS NULL;

--2
select actor.act_fname, actor.act_lname, movie_cast.role from actor 
inner join movie_cast on actor.act_id = movie_cast.act_id 
inner join movie on movie_cast.mov_id = movie.mov_id 
where movie.mov_title = 'Annie Hall';

--3
select dir_fname,dir_lname ,mov_title from movie_direction
inner join movie on movie_direction.mov_id = movie.mov_id 
inner join director on movie_direction.dir_id = director.dir_id
where mov_title = 'Eyes Wide Shut';

--4
select dir_fname,dir_lname ,mov_title from movie
inner join movie_direction on movie.mov_id = movie_direction.mov_id
inner join director on movie_direction.dir_id = director.dir_id
inner join movie_cast on  movie.mov_id = movie_cast.mov_id
where role ='Sean Maguire';

--5
select act_fname,act_lname,mov_title,mov_year from movie	
inner join movie_cast on movie.mov_id = movie_cast.mov_id 
inner join actor on movie_cast.act_id = actor.act_id
where movie.mov_year not between 1990 AND 2000;

--6
select dir_fname, dir_lname, COUNT(gen_title) AS number_of_generic_movies from director 
inner join movie_direction on director.dir_id = movie_direction.dir_id
inner join movie_genres on movie_direction.mov_id = movie_genres.mov_id
inner join genres on movie_genres.gen_id = genres.gen_id 
GROUP BY dir_fname,dir_lname,gen_title
ORDER BY dir_fname,dir_lname;

--7
select movie.mov_title, movie.mov_year, genres.gen_title from movie	
inner join movie_genres on movie.mov_id = movie_genres.mov_id
inner join genres on movie_genres.gen_id = genres.gen_id;

--8
select movie.mov_title, movie.mov_year, genres.gen_title, ConCAT(director.dir_fname, ' ', director.dir_lname) AS Director_Name from movie
inner join movie_direction on movie.mov_id = movie_direction.mov_id 
inner join director on movie_direction.dir_id = director.dir_id
inner join movie_genres on movie.mov_id = movie_genres.mov_id 
inner join genres on movie_genres.gen_id = genres.gen_id;

--9
select movie.mov_title, movie.mov_year, movie.mov_dt_rel, movie.mov_time, director.dir_fname, director.dir_lname from movie
inner join movie_direction on movie.mov_id = movie_direction.mov_id
inner join director on movie_direction.dir_id = director.dir_id 
where movie.mov_year <= 1988
ORDER BY movie.mov_year DESC, movie.mov_dt_rel DESC

--10
select gen_title, AVG(mov_time) AS avg_mov_time, COUNT(movie_genres.mov_id) AS number_of_mov from movie	
inner join movie_genres on movie.mov_id =  movie_genres.mov_id
inner join genres on movie_genres. gen_id = genres.gen_id
GROUP BY gen_title;

--11
select movie.mov_title, movie.mov_year, director.dir_fname, director.dir_lname, actor.act_fname, actor.act_lname, movie_cast.role from movie	
inner join movie_direction on movie.mov_id = movie_direction.mov_id
inner join director on movie_direction.dir_id = director.dir_id
inner join movie_cast on movie.mov_id = movie_cast.mov_id
inner join actor on movie_cast.act_id= actor.act_id
where mov_time in (select min(mov_time) from movie);

--12
select mov_year, rating.rev_stars from movie 
inner join rating on movie.mov_id = rating.mov_id
where rating.rev_stars BETWEEN 3 AND 4
ORDER BY movie.mov_year;

--13
select reviewer.rev_name, movie.mov_title, rating.rev_stars from movie
inner join rating on movie.mov_id = rating.mov_id 
inner join reviewer on rating.rev_id = reviewer.rev_id 
ORDER BY reviewer.rev_name, movie.mov_title, rating.rev_stars;

--14
select movie.mov_title, MAX(rating.rev_stars) AS Max_Stars from movie
inner join rating on movie.mov_id = rating.mov_id
where rating.num_o_ratings IS NOT NULL
GROUP BY movie.mov_title;

--15
select movie.mov_title, director.dir_fname, director.dir_lname, rating.rev_stars from movie
inner join movie_direction on movie.mov_id = movie_direction.mov_id 
inner join director on movie_direction.dir_id = director.dir_id
inner join rating on movie.mov_id = rating.mov_id
where rating.rev_stars IS NOT NULL;

--16
select movie.mov_title, actor.act_fname, actor.act_lname, movie_cast.role from movie
inner join movie_cast on movie.mov_id = movie_cast.mov_id 
inner join actor on movie_cast.act_id = actor.act_id
where movie_cast.act_id in (select act_id from movie_cast 
GROUP BY act_id HAVinG COUNT(act_id) > 1);