using BackEnd;
using BackEnd.DbStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BackEndTest.DbHandlerLayer
{
    public class DOC_TestDbHandler
    {
        [Fact]
        public async void TestGetBooksFromAuthorCities()
        {
            var _dbHandler = new DbHandler();

            var _authorStruct = await _dbHandler.GetBooksFromAuthor("J. Arthur Gibbs", DbTypesEnum.Document);

            Assert.True(_authorStruct.CityList.Count != 0);
        }

        [Fact]
        public async void TestGetBooksFromAuthorBooks()
        {
            var _dbHandler = new DbHandler();

            var _authorStruct = await _dbHandler.GetBooksFromAuthor("J. Arthur Gibbs", DbTypesEnum.Document);

            Assert.True(_authorStruct.BookList.Count != 0);
        }

        [Fact]
        public async void TestGetMockGeolocationsFromBookTitle()
        {
            var _dbHandler = new DbHandler();

            var _res = await _dbHandler.GetGeolocationsFromBookTitle("Buddy And Brighteyes Pigg", DbTypesEnum.Document);

            Assert.True(_res.Count != 0);
        }

        [Fact]
        public async void TestCreateMockGeolocationMarkersBook()
        {
            var _dbHandler = new DbHandler();

            var _data = await _dbHandler.GetGeoMarkersFromGeolocations(10, 45, DbTypesEnum.Document);

            var _res = false;

            if (_data.BookList.Count != 0)
            {
                _res = true;
            }

            Assert.True(_res);
        }

        [Fact]
        public async void TestCreateMockGeolocationMarkersCity()
        {
            var _dbHandler = new DbHandler();

            var _data = await _dbHandler.GetGeoMarkersFromGeolocations(10, 45, DbTypesEnum.Document);

            var _res = false;

            if (_data.CityList.Count != 0)
            {
                _res = true;
            }

            Assert.True(_res);
        }
    }
}
