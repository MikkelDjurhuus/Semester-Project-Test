using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Datamining
{
    public static class Queries
    {
        public static void Run()
        {
            /*MySQL1("Aci Castello");
            MySQL2("Three Years' War");
            MySQL3("Arthur Conan Doyle");
            MySQL4("2","3");*/
            Mongo1("Aci Castello");
            Console.WriteLine("Press key to exit");
            Console.ReadLine();
        }

        //Given a city name your application returns all book titles with corresponding authors that mention this city.
        public static void MySQL1(string name)
        {
            MySqlConnection connection = SQLDB.GetConnection();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT distinct b.author, b.title FROM gutenberg.mentions m " +
                    "join gutenberg.book b on b.id = m.book_id " +
                    "join gutenberg.city c on c.id = m.city_id " +
                    "where c.name = @name " +
                    "order by b.title";
                cmd.Parameters.AddWithValue("@name", name);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string author = reader.GetString(0);
                    string title = reader.GetString(1);
                    Console.WriteLine($"Author: {author} - Title: {title}");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        //Given a book title, your application plots all cities mentioned in this book onto a map.
        public static void MySQL2(string title)
        {
            MySqlConnection connection = SQLDB.GetConnection();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT distinct c.name, c.location FROM gutenberg.mentions m "+
                    "join gutenberg.book b on b.id = m.book_id " +
                    "join gutenberg.city c on c.id = m.city_id " +
                    "where b.title = @title ";
                cmd.Parameters.AddWithValue("@title", title);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    MySqlGeometry location = reader.GetMySqlGeometry(1);
                    Console.WriteLine($"name: {name} - location: {location}");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        //Given an author name your application lists all books written by that author and plots all cities mentioned in any of the books onto a map.
        public static void MySQL3(string author)
        {
            MySqlConnection connection = SQLDB.GetConnection();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT distinct c.name, c.location FROM gutenberg.mentions m " +
                    "join gutenberg.book b on b.id = m.book_id " +
                    "join gutenberg.city c on c.id = m.city_id " +
                    "where b.author = @author " +
                    "order by c.name";
                cmd.Parameters.AddWithValue("@author", author);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string _cityname = reader.GetString(0);
                    MySqlGeometry _location = reader.GetMySqlGeometry(1);
                    Console.WriteLine($"City: {_cityname} - Location: {_location}");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        //Given a geolocation, your application lists all books mentioning a city in vicinity of the given geolocation.
        public static void MySQL4(string lon, string lat)
        {
            MySqlConnection connection = SQLDB.GetConnection();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT distinct b.author, b.title FROM gutenberg.mentions m " +
                    "join gutenberg.book b on b.id = m.book_id " +
                    "join gutenberg.city c on c.id = m.city_id " +
                    "order by st_distance(c.location,GeomFromText(CONCAT('POINT(',@lon, ' ', @lat,')'))) limit 50";
                cmd.Parameters.AddWithValue("@lon", lon);
                cmd.Parameters.AddWithValue("@lat", lat);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string author = reader.GetString(0);
                    string title = reader.GetString(1);
                    Console.WriteLine($"Author: {author} - Title: {title}");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        //Given a city name your application returns all book titles with corresponding authors that mention this city.
        public static void Mongo1(string name)
        {
            IMongoDatabase database = DocumentDB.GetConnection();
            try
            {
                var books = database.GetCollection<BsonDocument>("Book");
                var cities = database.GetCollection<BsonDocument>("City");
                var nameFilter = Builders<BsonDocument>.Filter.Eq("name", name);
                var result = cities.Find(nameFilter).FirstOrDefault();
                var objID = result[0];
                Console.WriteLine(objID);
                var match = Builders<BsonDocument>.Filter.AnyEq("cities", objID);
                //var match = Builders<BsonDocument>.Filter.
                var result1 = books.Find(match).ToList();
                foreach (var item in result1)
                {
                    Console.WriteLine(item["title"]);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
            }
        }
        //Given a book title, your application plots all cities mentioned in this book onto a map.
        public static void Mongo2(string title)
        {
            MySqlConnection connection = SQLDB.GetConnection();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT distinct c.name, c.location FROM gutenberg.mentions m " +
                    "join gutenberg.book b on b.id = m.book_id " +
                    "join gutenberg.city c on c.id = m.city_id " +
                    "where b.title = @title ";
                cmd.Parameters.AddWithValue("@title", title);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    MySqlGeometry location = reader.GetMySqlGeometry(1);
                    Console.WriteLine($"name: {name} - location: {location}");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        //Given an author name your application lists all books written by that author and plots all cities mentioned in any of the books onto a map.
        public static void Mongo3(string author)
        {
            MySqlConnection connection = SQLDB.GetConnection();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT distinct c.name, c.location FROM gutenberg.mentions m " +
                    "join gutenberg.book b on b.id = m.book_id " +
                    "join gutenberg.city c on c.id = m.city_id " +
                    "where b.author = @author " +
                    "order by c.name";
                cmd.Parameters.AddWithValue("@author", author);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string _cityname = reader.GetString(0);
                    MySqlGeometry _location = reader.GetMySqlGeometry(1);
                    Console.WriteLine($"City: {_cityname} - Location: {_location}");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        //Given a geolocation, your application lists all books mentioning a city in vicinity of the given geolocation.
        public static void Mongo4(string lon, string lat)
        {
            MySqlConnection connection = SQLDB.GetConnection();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT distinct b.author, b.title FROM gutenberg.mentions m " +
                    "join gutenberg.book b on b.id = m.book_id " +
                    "join gutenberg.city c on c.id = m.city_id " +
                    "order by st_distance(c.location,GeomFromText(CONCAT('POINT(',@lon, ' ', @lat,')'))) limit 50";
                cmd.Parameters.AddWithValue("@lon", lon);
                cmd.Parameters.AddWithValue("@lat", lat);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string author = reader.GetString(0);
                    string title = reader.GetString(1);
                    Console.WriteLine($"Author: {author} - Title: {title}");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }


    }
}
