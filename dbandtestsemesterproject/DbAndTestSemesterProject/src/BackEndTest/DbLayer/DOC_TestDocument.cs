using BackEnd;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BackEndTest
{
    public class DOC_TestDocument
    {
        [Fact]
        public void Insert()
        {
            Document db = new Document();
            Assert.True(db.Insert("Book", new BsonDocument { { "id", "999999" }, { "title", "something cool" }, { "author", "Mikkel Djurhuus" }}));
            Assert.True(db.FindDocument("Book", new BsonDocument { { "id", "999999" }, { "title", "something cool" }, { "author", "Mikkel Djurhuus" } }).Count > 0);
            db.ResetDatabase();
        }
        [Fact]
        public void Update()
        {
            Document db = new Document();
            Assert.True(db.Update("Book", new BsonDocument { { "id", "1" }}, new BsonDocument { { "title", "WTF SO AWSOME" } }));
            Assert.True(db.FindDocument("Book", new BsonDocument { { "id", "1" },{ "title", "WTF SO AWSOME" } }).Count > 0);
            db.ResetDatabase();
        }
        [Fact]
        public void Remove()
        {

            Document db = new Document();
            Assert.True(db.Remove("Book", new BsonDocument { { "id", "1" }}));
            Assert.True(db.FindDocument("Book", new BsonDocument { { "id", "1" } }).Count < 1);
            db.ResetDatabase();
        }

        public void ResetDatabase()
        {
            Document db = new Document();
            Assert.True(db.ResetDatabase());
        }

        [Fact]
        public async void TestGetBooksFromCityName()
        {
            var _doc = new Document();

            var _res = await _doc.GetBooksFromCityName("Abu Dhabi");

            Assert.True(_res.Count != 0);
        }

        [Fact]
        public async void TestGetAuthorQueryListBooks()
        {
            var _doc = new Document();

            var _res = await _doc.GetAuthorQueryList("James Oliver Curwood");

            Assert.True(_res.BookList.Count != 0);
        }

        [Fact]
        public async void TestGetAuthorQueryListCities()
        {
            var _doc = new Document();

            var _res = await _doc.GetAuthorQueryList("James Oliver Curwood");

            Assert.True(_res.CityList.Count != 0);
        }

        [Fact]
        public async void TestGetGeolocationsFromBookTitle()
        {
            var _doc = new Document();

            var _res = await _doc.GetGeolocationsFromBookTitle("Buddy And Brighteyes Pigg");

            Assert.True(_res.Count != 0);
        }

        [Fact]
        public async void TestGetGeolocationMarkersBook()
        {
            var _doc = new Document();

            var _res = await _doc.GetGetolocationMarkers(10, 45);

            Assert.True(_res.BookList.Count != 0);
        }

        [Fact]
        public async void TestGetGeolocationMarkersCity()
        {
            var _doc = new Document();

            var _res = await _doc.GetGetolocationMarkers(10, 45);

            Assert.True(_res.CityList.Count != 0);
        }
    }
}
