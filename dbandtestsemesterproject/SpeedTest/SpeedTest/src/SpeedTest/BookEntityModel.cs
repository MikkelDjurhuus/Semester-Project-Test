using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DbStuff
{
    public class BookEntityModel
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Author { get; set; }
        public virtual List<CityEntityModel> Cities { get; set; }
    }
}
