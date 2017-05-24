using BackEnd;
using BackEnd.DbStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BackEndTest
{
    public class TestDbTypesHandler
    {
        DbTypesHandler _dbTypes = new DbTypesHandler();

        [Fact]
        public async void TestGetDbTypesCount()
        {
            var _res = await _dbTypes.GetDbTypes();

            Assert.True(_res.Count == 4);
        }

        [Fact]
        public async void TestGetDbTypesDocument()
        {
            var _dbList = await _dbTypes.GetDbTypes();

            var _res = (from a in _dbList
                        where a.dbType == DbTypesEnum.Document
                        select a).FirstOrDefault();

            Assert.True(_res != null);
        }

        [Fact]
        public async void TestGetDbTypesGraph()
        {
            var _dbList = await _dbTypes.GetDbTypes();

            var _res = (from a in _dbList
                        where a.dbType == DbTypesEnum.Graph
                        select a).FirstOrDefault();

            Assert.True(_res != null);
        }

        [Fact]
        public async void TestGetDbTypesSQL()
        {
            var _dbList = await _dbTypes.GetDbTypes();

            var _res = (from a in _dbList
                        where a.dbType == DbTypesEnum.SQL
                        select a).FirstOrDefault();

            Assert.True(_res != null);
        }

        [Fact]
        public async void TestGetDbTypesMock()
        {
            var _dbList = await _dbTypes.GetDbTypes();

            var _res = (from a in _dbList
                        where a.dbType == DbTypesEnum.Mock
                        select a).FirstOrDefault();

            Assert.True(_res != null);
        }
    }
}
