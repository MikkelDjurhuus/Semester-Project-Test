using BackEnd;
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
