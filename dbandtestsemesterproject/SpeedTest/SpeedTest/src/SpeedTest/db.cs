using BackEnd.DbStuff;
using BackEnd.ReturnEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedTest
{
    public interface db
    {
        Task<List<object>> GetBooksFromCityName(string city);
        Task<AuthorStruct> GetAuthorQueryList(string author);
        Task<List<CityEntityModel>> GetGeolocationsFromBookTitle(string title);
        Task<AuthorStruct> GetGetolocationMarkers(double lon, double lat);
    }
}
