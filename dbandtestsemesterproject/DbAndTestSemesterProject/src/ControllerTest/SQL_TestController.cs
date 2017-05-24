using BackEnd.DbStuff;
using DbAndTestSemesterProject.Controllers.ApiControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ControllerTest
{
    public class SQL_TestController
    {
        [Fact]
        public async void TestGetBooksFromCityMock()
        {
            var _controller = new DatabaseApiController();

            var _res = await _controller.LoadBooksFromCity("Abu Dhabi", DbTypesEnum.SQL);

            Assert.True(_res.Count != 0);
        }


        [Fact]
        public async void TestLoadBooksFromAuthorBook()
        {
            var _controller = new DatabaseApiController();

            var _authorStruct = await _controller.LoadBooksFromAuthor("J. Arthur Gibbs", DbTypesEnum.SQL);

            Assert.True(_authorStruct.BookList.Count != 0);
        }

        [Fact]
        public async void TestLoadBooksFromAuthorCities()
        {
            var _controller = new DatabaseApiController();

            var _authorStruct = await _controller.LoadBooksFromAuthor("J. Arthur Gibbs", DbTypesEnum.SQL);

            Assert.True(_authorStruct.CityList.Count != 0);
        }

        [Fact]
        public async void TestGetAllDbTypesSQL()
        {
            var _controller = new DatabaseApiController();

            var _dbList = await _controller.GetAllDbTypes();

            var _res = (from x in _dbList
                        where x.dbType == DbTypesEnum.SQL
                        select x).FirstOrDefault();

            Assert.True(_res != null);
        }

        [Fact]
        public async void TestLoadGeolocationsFromBookTitle()
        {
            var _controller = new DatabaseApiController();

            var _res = await _controller.LoadGeolocationsFromBookTitle("Buddy And Brighteyes Pigg", DbTypesEnum.SQL);

            Assert.True(_res.Count != 0);
        }

        [Fact]
        public async void TestLoadGeoMarkersFromGeolocationsBook()
        {
            var _controller = new DatabaseApiController();

            var _data = await _controller.LoadGeoMarkersFromGeolocations(10, 45, DbTypesEnum.SQL);

            var _res = false;

            if (_data.BookList.Count != 0)
            {
                _res = true;
            }

            Assert.True(_res);
        }

        [Fact]
        public async void TestLoadGeoMarkersFromGeolocationsCity()
        {
            var _controller = new DatabaseApiController();

            var _data = await _controller.LoadGeoMarkersFromGeolocations(10, 45, DbTypesEnum.SQL);

            var _res = false;

            if (_data.CityList.Count != 0)
            {
                _res = true;
            }

            Assert.True(_res);
        }
    }
}
