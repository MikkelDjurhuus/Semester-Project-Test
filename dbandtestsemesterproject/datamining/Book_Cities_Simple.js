var count = db.getCollection("Book").count();
db.getCollection("refs").find().forEach(function (el) {
    db.getCollection("Book").update({id:el.book_id},{"$push":{location:el.city_id}})
    print(el.book_id + "/" + count);
})