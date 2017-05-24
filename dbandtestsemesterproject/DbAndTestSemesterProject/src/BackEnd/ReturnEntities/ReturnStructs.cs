using BackEnd.DbStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.ReturnEntities
{
    public struct AuthorStruct
    {
        public List<BookEntityModel> BookList;
        public List<CityEntityModel> CityList;

        public AuthorStruct(List<BookEntityModel> bookList, List<CityEntityModel> cityList)
        {
            BookList = bookList;
            CityList = cityList;
        }
    }

    public struct CityGeolocations
    {
        public List<double> Longitudes;
        public List<double> Latitudes;

        public CityGeolocations(List<double> longitudes, List<double> latitude)
        {
            Longitudes = longitudes;
            Latitudes = latitude;
        }
    }
}
