var cities = {};
var count = db.getCollection("Book").count();
db.getCollection("City").find().forEach(function (city) {
    cities[city.id] = city._id;
});
db.getCollection("refs").find().forEach(function (el) {
    db.getCollection("Book").update({id:el.book_id},{"$push":{location:cities[el.city_id]}})
    print(el.book_id + "/" + count);
})