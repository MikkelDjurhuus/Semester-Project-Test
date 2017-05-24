using BackEnd;
using BackEnd.DbStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BackEndTest
{
    public class MOCK_TestDbHandler
    {
        [Fact]
        public async void TestGetBooksFromAuthorBooks()
        {
            var _dbHandler = new DbHandler();

            var _authorStruct = await _dbHandler.GetBooksFromAuthor("J. Arthur Gibbs", DbTypesEnum.Mock);

            var _res = false;

            if (_authorStruct.BookList.Count == 1000)
            {
                _res = true;
            }

            Assert.True(_res);
        }

        [Fact]
        public async void TestGetBooksFromAuthorCities()
        {
            var _dbHandler = new DbHandler();

            var _authorStruct = await _dbHandler.GetBooksFromAuthor("J. Arthur Gibbs", DbTypesEnum.Mock);

            var _res = false;

            if (_authorStruct.CityList.Count >= 5 && _authorStruct.CityList.Count <= 50)
            {
                _res = true;
            }

            Assert.True(_res);
        }

        [Fact]
        public async void TestGetMockGeolocationsFromBookTitle()
        {
            var _dbHandler = new DbHandler();

            var _res = await _dbHandler.GetGeolocationsFromBookTitle("Buddy And Brighteyes Pigg", DbTypesEnum.Mock);

            Assert.True(_res.Count == 15);
        }

        [Fact]
        public async void TestCreateMockGeolocationMarkersBook()
        {
            var _dbHandler = new DbHandler();

            var _data = await _dbHandler.GetGeoMarkersFromGeolocations(10, 45, DbTypesEnum.Mock);

            var _res = false;

            if (_data.BookList.Count == 100)
            {
                _res = true;
            }

            Assert.True(_res);
        }

        [Fact]
        public async void TestCreateMockGeolocationMarkersCity()
        {
            var _dbHandler = new DbHandler();

            var _data = await _dbHandler.GetGeoMarkersFromGeolocations(10, 45, DbTypesEnum.Mock);

            var _res = false;

            if (_data.CityList.Count == 100)
            {
                _res = true;
            }

            Assert.True(_res);
        }
    }
}
