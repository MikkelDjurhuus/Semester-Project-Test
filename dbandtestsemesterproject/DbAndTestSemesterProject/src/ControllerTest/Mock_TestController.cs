using BackEnd.DbStuff;
using DbAndTestSemesterProject.Controllers.ApiControllers;
using System;
using System.Linq;
using Xunit;

namespace ControllerTest
{
    public class Mock_TestController
    {

        DatabaseApiController _controller = new DatabaseApiController();

        [Fact]
        public async void TestGetBooksFromCityMock()
        {
            var _res = await _controller.LoadBooksFromCity("Abu Dhabi", DbTypesEnum.Mock);

            Assert.True(_res != null);
        }


        [Fact]
        public async void TestLoadBooksFromAuthorBook()
        {
            var _authorStruct = await _controller.LoadBooksFromAuthor("J. Arthur Gibbs", DbTypesEnum.Mock);
            var _res = false;

            if (_authorStruct.BookList.Count == 1000)
            {
                _res = true;
            }

            Assert.True(_res);
        }

        [Fact]
        public async void TestLoadBooksFromAuthorCities()
        {
            var _authorStruct = await _controller.LoadBooksFromAuthor("J. Arthur Gibbs", DbTypesEnum.Mock);
            var _res = false;

            if (_authorStruct.CityList.Count >= 5 && _authorStruct.CityList.Count <= 50)
            {
                _res = true;
            }

            Assert.True(_res);
        }

        [Fact]
        public async void TestGetAllDbTypesMock()
        {
            var _dbList = await _controller.GetAllDbTypes();

            var _res = (from x in _dbList
                        where x.dbType == DbTypesEnum.Mock
                        select x).FirstOrDefault();

            Assert.True(_res != null);
        }

        [Fact]
        public async void TestLoadGeolocationsFromBookTitle()
        {
            var _res = await _controller.LoadGeolocationsFromBookTitle("Buddy And Brighteyes Pigg", DbTypesEnum.Mock);

            Assert.True(_res.Count == 15);
        }

        [Fact]
        public async void TestLoadGeoMarkersFromGeolocationsBook()
        {
            var _data = await _controller.LoadGeoMarkersFromGeolocations(10, 45, DbTypesEnum.Mock);

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
            var _data = await _controller.LoadGeoMarkersFromGeolocations(10, 45, DbTypesEnum.Mock);

            var _res = false;

            if (_data.CityList.Count != 0)
            {
                _res = true;
            }

            Assert.True(_res);
        }
    }
}
