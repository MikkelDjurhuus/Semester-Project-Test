using BackEnd;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BackEndTest
{
    public class GRAPH_TestGraph
    {

        [Fact]
        public void Merge()
        {
            Graph graph = new Graph();
            Assert.True(graph.RunStatement("merge (b:Book {id: 999999}) return b").ToList().Count > 0);
            ResetDatabase();
        }

        [Fact]
        public void Delete()
        {
            Graph graph = new Graph();
            Assert.True(graph.RunStatement("match (b: Book {id: 10}) delete b, count(b)").ToList().Count > 0);
            ResetDatabase();
        }

        [Fact]
        public async void TestGetBooksFromCityName()
        {
            var _graph = new Graph();

            var _res = await _graph.GetBooksFromCityName("Abu Dhabi");

            Assert.True(_res.Count != 0);
        }

        [Fact]
        public async void TestGetAuthorQueryListBooks()
        {
             var _graph = new Graph();

            var _res = await _graph.GetAuthorQueryList("James Oliver Curwood");

            Assert.True(_res.BookList.Count != 0);
        }

        [Fact]
        public async void TestGetAuthorQueryListCities()
        {
             var _graph = new Graph();

            var _res = await _graph.GetAuthorQueryList("James Oliver Curwood");

            Assert.True(_res.CityList.Count != 0);
        }

        [Fact]
        public async void TestGetGeolocationsFromBookTitle()
        {
             var _graph = new Graph();

            var _res = await _graph.GetGeolocationsFromBookTitle("Buddy And Brighteyes Pigg");

            Assert.True(_res.Count != 0);
        }

        [Fact]
        public async void TestGetGeolocationMarkersBook()
        {
             var _graph = new Graph();

            var _res = await _graph.GetGetolocationMarkers(10, 45);

            Assert.True(_res.BookList.Count != 0);
        }

        [Fact]
        public async void TestGetGeolocationMarkersCity()
        {
             var _graph = new Graph();

            var _res = await _graph.GetGetolocationMarkers(10, 45);

            Assert.True(_res.CityList.Count != 0);
        }
        [Fact]
        public void ResetDatabase()
        {
            Graph graph = new Graph();
            Assert.True(graph.ResetDatabase());
        }
    }
}
