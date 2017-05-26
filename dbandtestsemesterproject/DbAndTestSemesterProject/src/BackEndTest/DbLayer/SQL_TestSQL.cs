using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BackEnd;

namespace BackEndTest
{
    public class SQL_TestSQL
    {
        [Fact]
        public void ResetDatabase()
        {
            SQL sql = new SQL();
            Assert.True(sql.ResetDatabase());
        }

        [Fact]
        public void InsertBook()
        {
            SQL sql = new SQL();
            Assert.True(sql.InsertBook("Coolness", "Mikkel Djurhuus") > 0);
            sql.ResetDatabase();
        }
        [Fact]
        public void InsertCity()
        {
            SQL sql = new SQL();
            Assert.True(sql.InsertCity("My City", new MySql.Data.Types.MySqlGeometry(10.342, 32.234)) > 0);
            sql.ResetDatabase();
        }
        public void DeleteBook()
        {
            SQL sql = new SQL();
            Assert.True(sql.Delete("book", "id = 1") > 0);
            sql.ResetDatabase();
        }
        public void DeleteCity()
        {
            SQL sql = new SQL();
            Assert.True(sql.Delete("city", "id = 1") > 0);
            sql.ResetDatabase();
        }
        public void Update()
        {
            SQL sql = new SQL();
            Assert.True(sql.Update("book", "title = 'New title'", "id = 2") > 0);
            sql.ResetDatabase();
        }

        [Fact]
        public async void TestGetBooksFromCityName()
        {
            var _sql = new SQL();

            var _res = await _sql.GetBooksFromCityName("Abu Dhabi");

            Assert.True(_res.Count != 0);
        }

        [Fact]
        public async void TestGetAuthorQueryListBooks()
        {
            var _sql = new SQL();

            var _res = await _sql.GetAuthorQueryList("James Oliver Curwood");

            Assert.True(_res.BookList.Count != 0);
        }

        [Fact]
        public async void TestGetAuthorQueryListCities()
        {
            var _sql = new SQL();

            var _res = await _sql.GetAuthorQueryList("James Oliver Curwood");

            Assert.True(_res.CityList.Count != 0);
        }

        [Fact]
        public async void TestGetGeolocationsFromBookTitle()
        {
            var _sql = new SQL();

            var _res = await _sql.GetGeolocationsFromBookTitle("Buddy And Brighteyes Pigg");

            Assert.True(_res.Count != 0);
        }

        [Fact]
        public async void TestGetGeolocationMarkersBook()
        {
            var _sql = new SQL();

            var _res = await _sql.GetGetolocationMarkers(10, 45);

            Assert.True(_res.BookList.Count != 0);
        }

        [Fact]
        public async void TestGetGeolocationMarkersCity()
        {
            var _sql = new SQL();

            var _res = await _sql.GetGetolocationMarkers(10, 45);

            Assert.True(_res.CityList.Count != 0);
        }
    }
}
