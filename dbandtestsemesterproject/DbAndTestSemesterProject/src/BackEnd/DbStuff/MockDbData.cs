using BackEnd.ReturnEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DbStuff
{
    public class MockDbData
    {
        Random _random = new Random();

        public async Task<List<object>> GetMockCityNameQueryList(string cityName)
        {
            List<object> _returnData = new List<object>();

            if (cityName != null)
            {
                for (int i = 0; i < 1000; i++)
                {
                    var _tempBook = new BookEntityModel
                    {
                        ID = i,
                        Author = "Author" + i,
                        Name = "Name" + i
                    };

                    _returnData.Add(_tempBook);
                }
            }

            return await Task.FromResult(_returnData);
        }

        public async Task<List<CityEntityModel>> GetMockGeolocationsFromBookTitle(string bookTitle)
        {
            var _returnList = new List<CityEntityModel>();

            if (bookTitle != null)
            {
                for (int i = 0; i < 15; i++)
                {
                    var _cityList = await CreateMockCity(i);

                    _returnList.Add(_cityList);
                }
            }
            else
            {
                //Nothing...
            }

            return await Task.FromResult(_returnList);
        }


        public async Task<AuthorStruct> GetMockAuthorQueryList(string author)
        {
            List<BookEntityModel> _bookList = new List<BookEntityModel>();
            List<CityEntityModel> _cityList = new List<CityEntityModel>();

            if (author != null)
            {
                for (int i = 0; i < 1000; i++)
                {
                    var _tempBook = new BookEntityModel
                    {
                        ID = i,
                        Author = "Author" + i,
                        Name = "Name" + i,
                        //Cities = new List<CityEntityModel>()
                    };

                    _bookList.Add(_tempBook);
                }
                for (int a = 0; a < _random.Next(5, 50); a++)
                {
                    var _tempCity = await CreateMockCity(a);
                    _cityList.Add(_tempCity);
                }
            }

            var _returnStruct = new AuthorStruct(_bookList, _cityList);

            return await Task.FromResult(_returnStruct);
        }

        public async Task<CityEntityModel> CreateMockCity(int count, double lon = 0, double lat = 0)
        {
            var _returnCity = new CityEntityModel
            {
                ID = count,
                Latitude = lat > 0 ? lat :_random.Next(-90, 90),
                Longitude = lon > 0 ? lon :_random.Next(-90, 90),
                Name = "CityName" + count
            };

            return await Task.FromResult(_returnCity);
        }

        public async Task<List<object>> GetMockGeoLocations()
        {
            List<object> _returnData = new List<object>();

            for (int i = 0; i < 5; i++)
            {
                _returnData.Add(new CityEntityModel
                {
                    Latitude = _random.Next(-90, 90),
                    Longitude = _random.Next(-90, 90)
                });
            }

            return await Task.FromResult(_returnData);
        }

        public async Task<AuthorStruct> CreateMockGeolocationMarkers(double lon, double lat)
        {
            List<BookEntityModel> _bookList = new List<BookEntityModel>();
            List<CityEntityModel> _cityList = new List<CityEntityModel>();

            if (lon > 0 && lat > 0)
            {
                for (int i = 0; i < 100; i++)
                {
                    var _tempBook = new BookEntityModel
                    {
                        ID = i,
                        Author = "Author" + i,
                        Name = "Name" + i,
                        //Cities = new List<CityEntityModel>()
                    };

                    _bookList.Add(_tempBook);
                }
                for (int a = 0; a < _random.Next(5, 50); a++)
                {
                    var _tempCity = await CreateMockCity(a, lon, lat);
                    _cityList.Add(_tempCity);
                }
            }

            var _returnStruct = new AuthorStruct(_bookList, _cityList);

            return await Task.FromResult(_returnStruct);
        }
    }
}
