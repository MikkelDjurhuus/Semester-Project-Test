using BackEnd.DbStuff;
using DbAndTestSemesterProject.Controllers.ApiControllers;
using System;
using System.Linq;
using Xunit;

namespace Generic_TestController
{
    public class Generic_TestController
    {
        DatabaseApiController _controller = new DatabaseApiController();

        [Fact]
        public async void TestGetAllDbTypes()
        {
            var _res = await _controller.GetAllDbTypes();

            Assert.True(_res.Count == 4);
        }

        [Fact]
        public async void TestGetAllDbTypesGraph()
        {
            var _dbList = await _controller.GetAllDbTypes();

            var _res = (from x in _dbList
                        where x.dbType == DbTypesEnum.Graph
                        select x).FirstOrDefault();

            Assert.True(_res != null);
        }

        [Fact]
        public async void TestGetAllDbTypesDocument()
        {
            var _dbList = await _controller.GetAllDbTypes();

            var _res = (from x in _dbList
                        where x.dbType == DbTypesEnum.Document
                        select x).FirstOrDefault();

            Assert.True(_res != null);
        }

        [Fact]
        public async void TestGetAllDbTypesSQL()
        {
            var _dbList = await _controller.GetAllDbTypes();

            var _res = (from x in _dbList
                        where x.dbType == DbTypesEnum.SQL
                        select x).FirstOrDefault();

            Assert.True(_res != null);
        }
    }
}
