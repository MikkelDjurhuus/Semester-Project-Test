using BackEnd.DbStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd
{
    public class DbTypesHandler
    {
        public async Task<List<DbTypes>> GetDbTypes()
        {
            var _returnList = new List<DbTypes>();

            for (int i = 0; i < 4; i++)
            {
                _returnList.Add(new DbTypes { ID = i, dbType = (DbTypesEnum)i });
            }

            return await Task.FromResult(_returnList);
        }
    }
}
