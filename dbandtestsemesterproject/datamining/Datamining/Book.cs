using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datamining
{
    public class Books
    {
        public string Id { get; set; }
        public int RocketRequestID { get; set; }
        public int LoanRocketID { get; set; }

        public DateTime RequestDate { get; set; }

        public int CreatedByID { get; set; }

        public string RequestXML { get; set; }
    }
}
