using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DbStuff
{
    public class CityEntityModel
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual double Longitude { get; set; }
        public virtual double Latitude { get; set; }
    }
}
