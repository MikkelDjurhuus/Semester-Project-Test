LOAD DATA LOCAL INFILE 'C:/Users/Mikkel Djurhuus/Documents/Github/datamining/book.csv' 
INTO TABLE book
FIELDS TERMINATED BY ','
ENCLOSED BY '"'
LINES TERMINATED BY '\n'
IGNORE 1 LINES
(id,title,author);