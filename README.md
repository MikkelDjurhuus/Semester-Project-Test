# DBHandin
Report Results
By Mikkel Djurhuus & Theis Rye 
## Which database engines are used.  
We are using MySQL, MongoDB and Neo4j.  

## How data is modeled.

### Document
The City model has a 2dSphere index on the “Location” field.

### Graph
(Book {id, author, title})-[mentions {count}]->(City {id, name, latitude, longitude}).  
Book(id) ONLINE (For uniqueness).  
City(id) ONLINE (For uniqueness).  

### Neo4j

The City model has a spatial index on the field “Location”.  
All the primary keys are indexed, and are index as unique as well.

## How data is imported.
We import data to the database using a CSV file we generated, from our data mined data from Gutenberg.  
Below is the specific queries used to populate the different databases.
### Document
mongoimport -d Guttenberg -c Book --type csv --file  
C:\Users\Theko\OneDrive\Dokumenter\GitHub\datamining\book.csv --headerline  
mongoimport -d Guttenberg -c City --type csv --file  
C:\Users\Theko\OneDrive\Dokumenter\GitHub\datamining\cities15000.csv --headerline  
mongoimport -d Guttenberg -c refs --type csv --file  
C:\Users\Theko\OneDrive\Dokumenter\GitHub\datamining\mentions.csv --headerline  

To set the relative city into the books, we run the logic below.
```javascript
var cities = {};
db.City.find().forEach(function (city) {
    cities[city.id] = city._id;
});
db.refs.find().forEach(function (el) {
    var city = el.cityid.toString();
    var b = db.Book.findOne({
        id: el.bookid
    });
    if (b.cities) {
        b.cities.push(cities[city]);
    } else {
        b.cities = [cities[city]];
    }
    db.Book.save(b);
})
```
### Graph
LOAD CSV WITH HEADERS FROM "file:///cities15000.csv" AS row  
MERGE (:City {id: toInt(row.id), name: row.name, latitude: row.latitude, longitude: row.longitude});  

LOAD CSV WITH HEADERS FROM "file:///books-nocities.csv" AS row  
MERGE (:Book {id: toInt(row.id), title: row.title, author: row.author});  

LOAD CSV WITH HEADERS FROM "file:///mentioned.csv" AS row  
Match (b:Book {id: row.book_id})  
Match (c:City {id: row.city_id})  
Merge (b)-[r:mentions]->(c)  
ON CREATE SET r.count = 1  
ON MATCH SET r.count = r.count + 1  

### SQL
CREATE CONSTRAINT ON (book:Book) ASSERT book.id IS UNIQUE  
CREATE CONSTRAINT ON (city:City) ASSERT city.id IS UNIQUE  
 
## Behavior of query test set.

Query | SQL median | SQL avg | MongoDB median | MongoDB avg | Neo4j median | Neo4j avg
--- | --- | --- | --- | --- | --- | --- 
GetAuthorQueryList | 183 | 199 | 12 | 78 | 17 | 31
GetBooksFromCityName | 343 | 353 | 50 | 56 | 35 | 37
GetGeolocationsFromBookTitle | 182 | 183 | 11 | 11 | 19 | 19
GetGetolocationMarkers | 529 | 533 | 0 | 277 | 354 | 354

What might immediate spring to attention, is MongoDB median of 0 in “GetLocationMarkers”. All of these queries run on the same data sets. These include a lot of null values. Apparently MongoDB handles this better than the others. 
This might be because of the $near operator, used for geospatial queries. 
Our SQL is the slowest of them all, in every query. MongoDB(Document) and Neo4j(Graph) has some close calls. In the end, the overall winner must be considered MongoDB. At least for speed.
 
Our Recommendation Of Database Engine In Similar Production Projects
Our recommendation is the usage of MongoDB, due to its test results. And since we’re working with geospatial data, the $near operator included in the database is superior to the others we’ve tested in both SQL and Graph. 
 
