var count = db.getCollection("City").count();
db.getCollection("City").find().forEach(function(city){
    city.location = {"type": "Point", "coordinates" : [city.lon,city.lat]};
    db.City.save(city);
    print(city.id + "/" + count);
})    