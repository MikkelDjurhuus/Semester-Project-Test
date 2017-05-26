using BackEnd.DbStuff;
using BackEnd.ReturnEntities;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Graph
    {
        ISession conn;
        IDriver driver;

        public ISession GetConnection()
        {
            if (conn == null)
            {
                driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "root"));
                conn = driver.Session();
            }
            try
            {
                return conn;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ResetDatabase()
        {
            try
            {
                RunStatement("MATCH (n) OPTIONAL MATCH (n)-[r]-() DELETE n,r;");
                RunStatement("LOAD CSV WITH HEADERS FROM 'file:///books-nocities.csv' AS row MERGE(:Book { id: toInteger(row.id), title: row.title, author: row.author});");
                RunStatement("LOAD CSV WITH HEADERS FROM 'file:///cities15000.csv' AS row MERGE(:City { id: toInteger(row.id), name: row.name, latitude: row.latitude, longitude: row.longitude});");
                RunStatement("LOAD CSV WITH HEADERS FROM 'file:///mentioned.csv' AS row Match(b: Book { id: toInteger(row.book_id)}) Match(c: City { id: toInteger(row.city_id)}) Merge(b) -[r: mentions]->(c) ON CREATE SET r.count = 1 ON MATCH SET r.count = r.count + 1 return count(r)");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IStatementResult RunStatement(string statement)
        {
            var session = GetConnection();
            try
            {
                var result = session.Run(statement, new Dictionary<string, object>());
                result.Consume();
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public IStatementResult RunStatement(string statement, Dictionary<string,object> properties)
        {
            var session = GetConnection();
            try
            {
                var result = session.Run(statement, properties);
                result.Consume();
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<List<object>> GetBooksFromCityName(string cityName)
        {
            List<object> _returnData = new List<object>();

            var statementText = "MATCH (b:Book)-[menstions]->(City {name:{name}}) RETURN b.id as id, b.author as author, b.title as title";
            var statementParameters = new Dictionary<string, object> { { "name", cityName } };
            var session = GetConnection();
            try
            {
                var result = session.Run(statementText, statementParameters);
                foreach (var record in result)
                {
                    var _tempBook = new BookEntityModel
                    {
                        ID = record["id"].As<int>(),
                        Author = record["author"].As<string>(),
                        Name = record["title"].As<string>()
                    };

                    _returnData.Add(_tempBook);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return await Task.FromResult(_returnData);
        }

        public async Task<AuthorStruct> GetAuthorQueryList(string author)
        {
            List<BookEntityModel> _bookList = new List<BookEntityModel>();
            List<CityEntityModel> _cityList = new List<CityEntityModel>();

            var statementText = "MATCH (b:Book {author:{author}})-[menstions]->(c:City) " +
                "RETURN b.id as book_id, b.author as author, b.title as title, c.id as city_id, c.latitude as lat, c.longitude as lon, c.name as name";
            var statementParameters = new Dictionary<string, object> { { "author", author } };

            var session = GetConnection();
            try
            {
                var result = session.Run(statementText, statementParameters);
                foreach (var record in result)
                {
                    var _tempBook = new BookEntityModel
                    {
                        ID = record["book_id"].As<int>(),
                        Author = record["author"].As<string>(),
                        Name = record["title"].As<string>()
                    };
                    _bookList.Add(_tempBook);

                    var _returnCity = new CityEntityModel
                    {
                        ID = record["city_id"].As<int>(),
                        Latitude = record["lat"].As<double>(),
                        Longitude = record["lon"].As<double>(),
                        Name = record["name"].As<string>(),
                    };
                    _cityList.Add(_returnCity);
                }
            }
            catch (Exception e)
            {

                throw;
            }

            var _returnStruct = new AuthorStruct(_bookList, _cityList);

            return await Task.FromResult(_returnStruct);
        }

        public async Task<List<CityEntityModel>> GetGeolocationsFromBookTitle(string title)
        {
            var _returnList = new List<CityEntityModel>();

            var statementText = "MATCH (b:Book {title:{title}})-[menstions]->(c:City) RETURN c.id as id, c.longitude as lon, c.latitude as lat, c.name as name";
            var statementParameters = new Dictionary<string, object> { { "title", title } };
            
            var session = GetConnection();
            try
            {
                var result = session.Run(statementText, statementParameters);
                foreach (var record in result)
                {
                    var _returnCity = new CityEntityModel
                    {
                        ID = record["id"].As<int>(),
                        Latitude = record["lat"].As<double>(),
                        Longitude = record["lon"].As<double>(),
                        Name = record["name"].As<string>(),
                    };
                    _returnList.Add(_returnCity);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return await Task.FromResult(_returnList);
        }

        public async Task<AuthorStruct> GetGetolocationMarkers(double lon, double lat)
        {
            List<BookEntityModel> _bookList = new List<BookEntityModel>();
            List<CityEntityModel> _cityList = new List<CityEntityModel>();

            var statementText = "MATCH (b:Book)-[menstions]->(c:City) " +
                "with point({longitude: {lon}, latitude: {lat}}) as p1, point({longitude: c.longitude, latitude: c.latitude}) as p2, b, c " +
                "where distance(p1,p2)/1000 < 100 " +
                "RETURN b.id as book_id, b.author as author, b.title as title, c.id as city_id, c.latitude as lat, c.longitude as lon, c.name as name";
            var statementParameters = new Dictionary<string, object> { { "lon", lon }, { "lat", lat } };

            var session = GetConnection();
            try
            {
                var result = session.Run(statementText, statementParameters);
                foreach (var record in result)
                {
                    var _tempBook = new BookEntityModel
                    {
                        ID = record["book_id"].As<int>(),
                        Author = record["author"].As<string>(),
                        Name = record["title"].As<string>()
                    };
                    _bookList.Add(_tempBook);

                    var _returnCity = new CityEntityModel
                    {
                        ID = record["city_id"].As<int>(),
                        Latitude = record["lat"].As<double>(),
                        Longitude = record["lon"].As<double>(),
                        Name = record["name"].As<string>(),
                    };
                    _cityList.Add(_returnCity);
                }
            }
            catch (Exception e)
            {

                throw;
            }

            var _returnStruct = new AuthorStruct(_bookList, _cityList);

            return await Task.FromResult(_returnStruct);
        }
    }
}
