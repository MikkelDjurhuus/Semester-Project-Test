using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackEnd.DbStuff;
using BackEnd;
using BackEnd.ReturnEntities;

namespace DbAndTestSemesterProject.Controllers.ApiControllers
{
    public class DatabaseApiController : Controller
    {
        [HttpGet]
        public async Task<List<object>> LoadBooksFromCity(string cityName, DbTypesEnum dbType)
        {
            var _dbHandler = new DbHandler();

            var _returnList = await _dbHandler.GetBooksFromCityName(cityName, dbType);

            return await Task.FromResult(_returnList);
        }

        [HttpGet]
        public async Task<AuthorStruct> LoadBooksFromAuthor(string author, DbTypesEnum dbType)
        {
            DbHandler _dbHandler = new DbHandler();

            var _res = await _dbHandler.GetBooksFromAuthor(author, dbType);

            return await Task.FromResult(_res);
        }

        [HttpGet]
        public async Task<List<CityEntityModel>> LoadGeolocationsFromBookTitle(string bookTitle, DbTypesEnum dbType)
        {
            DbHandler _dbHandler = new DbHandler();

            var _res = await _dbHandler.GetGeolocationsFromBookTitle(bookTitle, dbType);

            return await Task.FromResult(_res);
        }

        [HttpGet]
        public async Task<List<DbTypes>> GetAllDbTypes()
        {
            DbTypesHandler _dbTypesHandler = new DbTypesHandler();

            var _res = await _dbTypesHandler.GetDbTypes();

            return await Task.FromResult(_res);
        }

        [HttpGet]
        public async Task<AuthorStruct> LoadGeoMarkersFromGeolocations(double lon, double lat, DbTypesEnum dbType)
        {
            DbHandler _dbTypesHandler = new DbHandler();

            var _res = await _dbTypesHandler.GetGeoMarkersFromGeolocations(lon, lat, dbType);

            return await Task.FromResult(_res);
        }
    }
}
