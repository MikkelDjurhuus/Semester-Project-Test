using System;
using BackEnd;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SpeedTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Document DocDB = new Document();
            Graph GDB = new Graph();
            SQL SQLDB = new SQL();
            for (int i = 0; i < 4; i++)
            {
                GetQuery(DocDB, i, "DOCUMENT");
            }
            Console.WriteLine("");
            Console.WriteLine("");
            for (int i = 0; i < 4; i++)
            {
                GetQuery(GDB, i, "GRAPH");
            }
            Console.WriteLine("");
            Console.WriteLine("");
            for (int i = 0; i < 4; i++)
            {
                GetQuery(SQLDB, i, "SQL");
            }
            Console.WriteLine("");
            Console.WriteLine("DONE!");
            Console.ReadKey();
        }
        public static void GetQuery(db db, int t, string dbName)
        {
            string[] authors = { "Lindsay, Anna Robertson Brown", "George Tucker", "Giovanni Boccaccio", "Stewart Edward White and Samuel Hopkins Adams", "S. H. Hammond", "John Muir", "Charles S. Brooks", "F. J. Cross", "Fannie  Hurst", "William F. Cody", "Edith Ferguson Black", "Elizabeth Robins (C. E. Raimond)", "Aphra Behn", "James Branch Cabell", "E.R. Murray and Henrietta Brown Smith", "Joseph Ladue", "John Buchan", "Dante Alighieri", "Edith Van Dyne", "Thomas H. Huxley" };
            string[] titles = { "Affair in Araby", "Right Ho, Jeeves", "The Life of Marie Antoinette, Queen of France", "The Old Man in the Corner", "Martin Eden", "The World Set Free", "Punch, or the London Charivari, Vol. 153, Sept. 12, 1917", "A General History and Collection of Voyages and Travels, Vol. 1", "The Poetical Works of Edmund Spenser, Volume 5", "English Literature", "Myths and Myth-Makers", "An Englishman's Travels in America", "The History of the Rise, Progress and Accomplishment of the", "The Uprising of a Great People", "Beacon Lights of History, Volume IX", "Snake and Sword", "At Last", "The Reign of Greed", "Biographical Memorials of James Oglethorpe", "A History of Freedom of Thought" };
            string[] cityNames = { "Grand Junction", "Myrtle Grove", "Pleasant Hill", "Rapid City", "Rio Grande", "Saint Paul", "Salt Lake City", "West Hartford", "Yerba Buena", "Poplar Bluff", "Nuevo Laredo", "Newton Abbot", "New York City", "Morris Heights", "Maple Grove", "Las Delicias", "La Plata", "La Marque", "Paso Robles", "Plant City" };
            int[] lon = { 10, 4, 6, 8, 10, 12, 14, 16, 18, 20, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };
            int[] lat = { 45, 4, 6, 8, 10, 12, 14, 16, 18, 20, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };
            long total = 0;
            List<long> elapsed = new List<long>();

            Console.WriteLine(dbName);
            Stopwatch sw = new Stopwatch();
            switch (t)
            {
                case 0:
                    for (int i = 0; i < 20; i++)
                    {
                        sw.Start();
                        db.GetAuthorQueryList(authors[i]);
                        sw.Stop();
                        elapsed.Add(sw.ElapsedMilliseconds);
                        total += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    GetStats(elapsed, total, "GetAuthorQueryList");
                    break;
                case 1:
                    for (int i = 0; i < 20; i++)
                    {
                        sw.Start();
                        db.GetBooksFromCityName(cityNames[i]);
                        sw.Stop();
                        elapsed.Add(sw.ElapsedMilliseconds);
                        total += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    GetStats(elapsed, total, "GetBooksFromCityName");
                    break;
                case 2:
                    for (int i = 0; i < 20; i++)
                    {
                        sw.Start();
                        db.GetGeolocationsFromBookTitle(titles[i]);
                        sw.Stop();
                        elapsed.Add(sw.ElapsedMilliseconds);
                        total += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    GetStats(elapsed, total, "GetGeolocationsFromBookTitle");
                    break;
                case 3:
                    for (int i = 0; i < 20; i++)
                    {
                        sw.Start();
                        var a = db.GetGetolocationMarkers(lon[i], lat[i]);
                        sw.Stop();
                        elapsed.Add(sw.ElapsedMilliseconds);
                        total += sw.ElapsedMilliseconds;
                        sw.Reset();
                    }
                    GetStats(elapsed, total, "GetGetolocationMarkers");
                    break;
                default:
                    break;
            }
        }
        public static void GetStats(List<long> elapsed, long total, string qname)
        {
            elapsed.Sort();
            long median = elapsed[9];
            long avg = total / 20;
            Console.WriteLine(qname);
            Console.WriteLine($"total: {total}");
            Console.WriteLine($"median: {median}");
            Console.WriteLine($"avg: {avg}");
            Console.WriteLine("");
        }
    }
}
