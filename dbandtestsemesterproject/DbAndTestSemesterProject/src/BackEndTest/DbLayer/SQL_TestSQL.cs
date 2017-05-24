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
