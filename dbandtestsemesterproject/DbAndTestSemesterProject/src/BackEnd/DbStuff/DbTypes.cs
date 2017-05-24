using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DbStuff
{
    public class DbTypes
    {
        public virtual int ID { get; set; }
        public virtual string DisplayName
        {
            get { return this.dbType.ToString(); }
        }
        public virtual DbTypesEnum dbType { get; set; }
    }

    public enum DbTypesEnum
    {
        Graph,
        Document,
        SQL,
        Mock
    }
}
