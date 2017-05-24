using BackEnd.DbStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BackEndTest
{
    public class TestMockDbData
    {
        MockDbData _mockDbData = new MockDbData();

        [Fact]
        public async void TestGetMockCityNameQueryList()
        {
            var _res = await _mockDbData.GetMockCityNameQueryList("Buddy And Brighteyes Pigg");

            Assert.True(_res.Count == 1000);
        }

        [Fact]
        public async void TestGetMockGeolocationsFromBookTitle()
        {
            var _res = await _mockDbData.GetMockGeolocationsFromBookTitle("Buddy And Brighteyes Pigg");

            Assert.True(_res.Count == 15);
        }

        [Fact]
        public async void TestGetMockGeoLocations()
        {
            var _res = await _mockDbData.GetMockGeoLocations();

            Assert.True(_res.Count == 5);
        }

        [Fact]
        public async void TestGetMockAuthorQueryListBook()
        {
            var _authorStruct = await _mockDbData.GetMockAuthorQueryList("Buddy And Brighteyes Pigg");
            var _res = false;

            if (_authorStruct.BookList.Count == 1000)
            {
                _res = true;
            }

            Assert.True(_res);
        }

        [Fact]
        public async void TestGetMockAuthorQueryListCities()
        {
            var _authorStruct = await _mockDbData.GetMockAuthorQueryList("Buddy And Brighteyes Pigg");
            var _res = false;

            if (_authorStruct.CityList.Count >= 5 && _authorStruct.CityList.Count <= 50)
            {
                _res = true;
            }

            Assert.True(_res);
        }

        [Fact]
        public async void TestCreateMockGeolocationMarkersBook()
        {
            var _data = await _mockDbData.CreateMockGeolocationMarkers(10, 45);

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
            var _data = await _mockDbData.CreateMockGeolocationMarkers(10, 45);

            var _res = false;

            if (_data.CityList.Count > 0)
            {
                _res = true;
            }

            Assert.True(_res);
        }
    }
}
