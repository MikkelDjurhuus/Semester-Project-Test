LOAD DATA LOCAL INFILE 'C:/Users/Mikkel Djurhuus/Documents/Github/datamining/mentioned.csv' 
INTO TABLE mentions
FIELDS TERMINATED BY ','
ENCLOSED BY '"'
LINES TERMINATED BY '\n'
IGNORE 1 LINES
(book_id, city_id);