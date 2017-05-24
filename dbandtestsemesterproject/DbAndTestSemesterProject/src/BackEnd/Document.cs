using BackEnd.DbStuff;
using BackEnd.ReturnEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

namespace BackEnd
{
    public class Document
    {
        IMongoClient client = null;
        IMongoDatabase database = null;

        public IMongoDatabase GetConnection()
        {
            if (database == null)
            {
                client = new MongoClient();
                database = client.GetDatabase("Guttenberg");
            }
            return database;
        }

        public async Task<List<object>> GetBooksFromCityName(string cityName)
        {
            IMongoDatabase database = GetConnection();
            List<object> _returnData = new List<object>();
            try
            {
                var books = database.GetCollection<BsonDocument>("Book");
                var cities = database.GetCollection<BsonDocument>("City");
                var nameFilter = Builders<BsonDocument>.Filter.Eq("name", cityName);
                var result = cities.Find(nameFilter).FirstOrDefault();
                var objID = result[0].AsObjectId;
                var match = Builders<BsonDocument>.Filter.AnyEq("cities",objID);
                var result1 = books.Find(match).ToList();
                foreach (var item in result1)
                {
                    var _tempBook = new BookEntityModel
                    {
                        ID = item["id"].AsInt32,
                        Author = item["author"].AsString,
                        Name = item["title"].AsString
                    };
                    _returnData.Add(_tempBook);
                }
                return await Task.FromResult(_returnData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AuthorStruct> GetAuthorQueryList(string author)
        {
            List<BookEntityModel> _bookList = new List<BookEntityModel>();
            List<CityEntityModel> _cityList = new List<CityEntityModel>();
            IMongoDatabase database = GetConnection();
            List<object> _returnData = new List<object>();
            try
            {
                var books = database.GetCollection<BsonDocument>("Book");
                var cities = database.GetCollection<BsonDocument>("City");
                var authorFilter = Builders<BsonDocument>.Filter.Eq("author", author);
                var result = books.Find(authorFilter).ToList();
                foreach (var book in result)
                {
                    var _tempBook = new BookEntityModel
                    {
                        ID = book["id"].AsInt32,
                        Author = book["author"].AsString,
                        Name = book["title"].AsString
                    };
                    _bookList.Add(_tempBook);

                    var _cities = book["cities"].AsBsonArray;
                    var cityFilter = Builders<BsonDocument>.Filter.In("_id", _cities);
                    var result1 = cities.Find(cityFilter).ToList();
                    foreach (var city in result1)
                    {
                        var _tempCity = new CityEntityModel
                        {
                            ID = city["id"].AsInt32,
                            Latitude = city["location"]["coordinates"][1].AsDouble,
                            Longitude = city["location"]["coordinates"][0].AsDouble,
                            Name = city["name"].AsString,
                        };
                        _cityList.Add(_tempCity);
                    }
                }

                var _returnStruct = new AuthorStruct(_bookList, _cityList);

                return await Task.FromResult(_returnStruct);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<CityEntityModel>> GetGeolocationsFromBookTitle(string title)
        {
            IMongoDatabase database = GetConnection();
            var _cityList = new List<CityEntityModel>();
            try
            {
                var books = database.GetCollection<BsonDocument>("Book");
                var cities = database.GetCollection<BsonDocument>("City");
                var titleFilter = Builders<BsonDocument>.Filter.Eq("title", title);
                var result = books.Find(titleFilter).ToList();
                foreach (var book in result)
                {/*
                    var _tempBook = new BookEntityModel
                    {
                        ID = book["id"].AsInt32,
                        Author = book["author"].AsString,
                        Name = book["title"].AsString
                    };
                    _bookList.Add(_tempBook);*/

                    var _cities = book["cities"].AsBsonArray;
                    var cityFilter = Builders<BsonDocument>.Filter.In("_id", _cities);
                    var result1 = cities.Find(cityFilter).ToList();
                    foreach (var city in result1)
                    {
                        var _tempCity = new CityEntityModel
                        {
                            ID = city["id"].AsInt32,
                            Latitude = city["location"]["coordinates"][1].AsDouble,
                            Longitude = city["location"]["coordinates"][0].AsDouble,
                            Name = city["name"].AsString,
                        };
                        _cityList.Add(_tempCity);
                    }
                }

                return await Task.FromResult(_cityList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AuthorStruct> GetGetolocationMarkers(double lon, double lat)
        {
            List<BookEntityModel> _bookList = new List<BookEntityModel>();
            List<CityEntityModel> _cityList = new List<CityEntityModel>();
            IMongoDatabase database = GetConnection();
            try
            {
                var books = database.GetCollection<BsonDocument>("Book");
                var cities = database.GetCollection<BsonDocument>("City");
                var gp = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(lon, lat));
                var locationFilter = Builders<BsonDocument>.Filter.Near("location", gp, 100000);
                var result = cities.Find(locationFilter).ToList();
                foreach (var city in result)
                {
                    var objID = city["_id"].AsObjectId;
                    var cityIDFilter = Builders<BsonDocument>.Filter.AnyEq("cities", objID);
                    var result1 = books.Find(cityIDFilter).ToList();
                    foreach (var book in result1)
                    {
                        var _tempBook = new BookEntityModel
                        {
                            ID = book["id"].AsInt32,
                            Author = book["author"].AsString,
                            Name = book["title"].AsString
                        };
                        _bookList.Add(_tempBook);
                    }
                    if (result1.Count > 0)
                    {
                        var _tempCity = new CityEntityModel
                        {
                            ID = city["id"].AsInt32,
                            Latitude = city["location"]["coordinates"][1].AsDouble,
                            Longitude = city["location"]["coordinates"][0].AsDouble,
                            Name = city["name"].AsString,
                        };
                        _cityList.Add(_tempCity);
                    }
                }

                var _returnStruct = new AuthorStruct(_bookList, _cityList);

                return await Task.FromResult(_returnStruct);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
