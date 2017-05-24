using BackEnd.DbStuff;
using BackEnd.ReturnEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd
{
    public class DbHandler
    {
        public async Task<List<object>> GetBooksFromCityName(string cityName, DbTypesEnum dbType)
        {
            switch (dbType)
            {
                case DbTypesEnum.Graph:
                    var _graph = new Graph();
                    var _gRes = await _graph.GetBooksFromCityName(cityName);

                    return _gRes;
                case DbTypesEnum.Document:
                    var _doc = new Document();
                    var _dRes = await _doc.GetBooksFromCityName(cityName);

                    return _dRes;
                case DbTypesEnum.SQL:
                    var _sql = new SQL();
                    var _sRes = await _sql.GetBooksFromCityName(cityName);

                    return _sRes;
                case DbTypesEnum.Mock:
                    var _mock = new MockDbData();
                    var _res = await _mock.GetMockCityNameQueryList(cityName);

                    return _res;
                default:
                    break;
            }

            return await Task.FromResult(new List<object>());
        }

        public async Task<AuthorStruct> GetBooksFromAuthor(string author, DbTypesEnum dbType)
        {
            switch (dbType)
            {
                case DbTypesEnum.Graph:
                    var _graph = new Graph();
                    var _gRes = await _graph.GetAuthorQueryList(author);

                    return _gRes;
                case DbTypesEnum.Document:
                    var _doc = new Document();
                    var _dRes = await _doc.GetAuthorQueryList(author);

                    return _dRes;
                case DbTypesEnum.SQL:
                    var _sql = new SQL();
                    var _sRes = await _sql.GetAuthorQueryList(author);

                    return _sRes;
                case DbTypesEnum.Mock:
                    var _mock = new MockDbData();
                    var _res = await _mock.GetMockAuthorQueryList(author);

                    return _res;
                default:
                    break;
            }
            return new AuthorStruct { };
        }

        public async Task<List<CityEntityModel>> GetGeolocationsFromBookTitle(string bookTitle, DbTypesEnum dbType)
        {
            switch (dbType)
            {
                case DbTypesEnum.Graph:
                    var _graph = new Graph();
                    var _gRes = await _graph.GetGeolocationsFromBookTitle(bookTitle);

                    return _gRes;
                case DbTypesEnum.Document:
                    var _doc = new Document();
                    var _dRes = await _doc.GetGeolocationsFromBookTitle(bookTitle);

                    return _dRes;
                case DbTypesEnum.SQL:
                    var _sql = new SQL();
                    var _sRes = await _sql.GetGeolocationsFromBookTitle(bookTitle);

                    return _sRes;
                case DbTypesEnum.Mock:
                    var _mock = new MockDbData();
                    var _res = await _mock.GetMockGeolocationsFromBookTitle(bookTitle);

                    return _res;

                default:
                    break;
            }

            return null;
        }

        public async Task<AuthorStruct> GetGeoMarkersFromGeolocations(double lon, double lat, DbTypesEnum dbType)
        {
            switch (dbType)
            {
                case DbTypesEnum.Graph:
                    var _graph = new Graph();
                    var _gRes = await _graph.GetGetolocationMarkers(lon, lat);

                    return _gRes;
                case DbTypesEnum.Document:
                    var _doc = new Document();
                    var _dRes = await _doc.GetGetolocationMarkers(lon, lat);

                    return _dRes;
                case DbTypesEnum.SQL:
                    var _sql = new SQL();
                    var _sRes = await _sql.GetGetolocationMarkers(lon, lat);

                    return _sRes;
                case DbTypesEnum.Mock:
                    var _mock = new MockDbData();
                    var _res = await _mock.CreateMockGeolocationMarkers(lon, lat);

                    return _res;
                default:
                    break;
            }

            return new AuthorStruct(null, null);
        }
    }
}
