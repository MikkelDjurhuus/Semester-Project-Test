using NUnit.Framework;
using Datamining;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datamining.Tests
{
    [TestFixture()]
    public class RegexHelperTests
    {
        //string sourceDirectory = "C:/Users/Theko/Desktop/books";

        [Test()]
        public void GetAuthorTest()
        {
            string bookExcerp = "asdasod asd asd fasflae ose ASd asd AD Title: asdawdas /n asdas d";
            Assert.AreEqual(RegexHelper.GetAuthor(bookExcerp), "asdawdas");
        }
    }
}