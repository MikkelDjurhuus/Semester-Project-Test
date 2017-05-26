using BackEnd.DbStuff;
using BackEnd.ReturnEntities;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace BackEnd
{
    public class SQL
    {
        const string user = "root";
        const string password = "root";
        const string server = "localhost";
        const string database = "gutenberg";
        MySqlConnection conn;

        public MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                conn = new MySqlConnection(string.Format("Server=localhost;Database=gutenberg;Uid=root;Pwd=root;"));
            }
            try
            {
                conn.Open();
                return conn;
            }
            catch (MySqlException e)
            {
                throw e;
            }
        }

        public bool ResetDatabase()
        {
            try
            {
                DropTable("book");
                DropTable("city");
                DropTable("mentions");

                LoadTable("book", "book", "(id,title,author)");
                LoadTable("city", "cities15000", "(@id,@name,@lat,@lon)", "set id=@id,name=@name,location = Point(@lon,@lat)");
                LoadTable("mentions", "mentioned", "(book_id,city_id)");
                return true;
            }
            catch (Exception e)
            {
                throw;
                return false;
            }
        }

        public void DropTable(string name)
        {
            MySqlConnection connection = GetConnection();
            List<object> _returnData = new List<object>();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "TRUNCATE gutenberg.@name";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteReader();
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

        public void LoadTable(string csv, string table, string cols, string set = "")
        {
            MySqlConnection connection = GetConnection();
            List<object> _returnData = new List<object>();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "LOAD DATA LOCAL INFILE 'C:/Users/Mikkel Djurhuus/Documents/Github/datamining/@csv.csv' " +
                    "INTO TABLE @table" +
                    "FIELDS TERMINATED BY ','" +
                    "ENCLOSED BY '\"'" +
                    "LINES TERMINATED BY '\n'" +
                    "IGNORE 1 LINES" +
                    "@cols" + 
                    "@set";
                cmd.Parameters.AddWithValue("@csv", csv);
                cmd.Parameters.AddWithValue("@table", table);
                cmd.Parameters.AddWithValue("@cols", cols);
                cmd.ExecuteNonQuery();
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
        public int InsertCity(string name, MySql.Data.Types.MySqlGeometry location)
        {
            MySqlConnection connection = GetConnection();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"INSERT INTO city (name, location) VALUES (@name, @location)";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@location", location);
                return cmd.ExecuteNonQuery();
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
        public int InsertBook(string title, string author)
        {
            MySqlConnection connection = GetConnection();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"INSERT INTO city (title, author) VALUES (@title, @author)";
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@author", author);
                return cmd.ExecuteNonQuery();
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
        public int Delete(string table, string where)
        {
            MySqlConnection connection = GetConnection();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"Delete FROM {table} where {where}";
                return cmd.ExecuteNonQuery();
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
        public int Update(string table, string set, string where)
        {
            MySqlConnection connection = GetConnection();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"UPDATE {table} SET {set} WHERE {where}";
                return cmd.ExecuteNonQuery();
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

        public async Task<List<BookEntityModel>> FindBook(string where, string select = "*")
        {
            MySqlConnection connection = GetConnection();
            List<BookEntityModel> _returnData = new List<BookEntityModel>();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"SELECT {select} FROM gutenberg.book b where {where} ";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var _tempBook = new BookEntityModel
                    {
                        ID = reader.GetInt16(0),
                        Author = reader.GetString(1),
                        Name = reader.GetString(2)
                    };
                    _returnData.Add(_tempBook);
                }
                return await Task.FromResult(_returnData);
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
        public async Task<List<CityEntityModel>> FindCity(string where, string select = "*")
        {
            MySqlConnection connection = GetConnection();
            List<CityEntityModel> _returnData = new List<CityEntityModel>();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"SELECT {select} FROM gutenberg.city b where {where} ";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var location = reader.GetMySqlGeometry(2);
                    var _returnCity = new CityEntityModel
                    {
                        ID = reader.GetInt16(0),
                        Latitude = (double)location.YCoordinate,
                        Longitude = (double)location.XCoordinate,
                        Name = reader.GetString(1)
                    };
                    _returnData.Add(_returnCity);
                }
                return await Task.FromResult(_returnData);
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

        public async Task<List<object>> GetBooksFromCityName(string cityName)
        {

            MySqlConnection connection = GetConnection();
            List<object> _returnData = new List<object>();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT distinct b.id, b.author, b.title FROM gutenberg.mentions m " +
                    "join gutenberg.book b on b.id = m.book_id " +
                    "join gutenberg.city c on c.id = m.city_id " +
                    "where c.name = @name " +
                    "order by b.title";
                cmd.Parameters.AddWithValue("@name", cityName);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var _tempBook = new BookEntityModel
                    {
                        ID = reader.GetInt16(0),
                        Author = reader.GetString(1),
                        Name = reader.GetString(2)
                    };
                    _returnData.Add(_tempBook);
                }
                return await Task.FromResult(_returnData);
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

        public async Task<AuthorStruct> GetAuthorQueryList(string author)
        {
            MySqlConnection connection = GetConnection();
            List<BookEntityModel> _bookList = new List<BookEntityModel>();
            List<CityEntityModel> _cityList = new List<CityEntityModel>();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT distinct b.id, b.author, b.title, c.name, c.location, c.id FROM gutenberg.mentions m " +
                    "join gutenberg.book b on b.id = m.book_id " +
                    "join gutenberg.city c on c.id = m.city_id " +
                    "where b.author = @author " +
                    "order by c.name";
                cmd.Parameters.AddWithValue("@author", author);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var _tempBook = new BookEntityModel
                    {
                        ID = reader.GetInt16(0),
                        Author = reader.GetString(1),
                        Name = reader.GetString(2)
                    };
                    _bookList.Add(_tempBook);

                    var location = reader.GetMySqlGeometry(4);
                    var _returnCity = new CityEntityModel
                    {
                        ID = reader.GetInt16(5),
                        Latitude = (double)location.YCoordinate,
                        Longitude = (double)location.XCoordinate,
                        Name = reader.GetString(3)
                    };
                    _cityList.Add(_returnCity);
                }

                var _returnStruct = new AuthorStruct(_bookList, _cityList);

                return await Task.FromResult(_returnStruct);
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

        public async Task<List<CityEntityModel>> GetGeolocationsFromBookTitle(string title)
        {
            MySqlConnection connection = GetConnection();
            var _returnList = new List<CityEntityModel>();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT distinct count(*), c.name, c.location FROM gutenberg.mentions m " +
                    "join gutenberg.book b on b.id = m.book_id " +
                    "join gutenberg.city c on c.id = m.city_id " +
                    "where b.title = @title " +
                    "group by c.name";
                cmd.Parameters.AddWithValue("@title", title);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var location = reader.GetMySqlGeometry(2);
                    var _returnCity = new CityEntityModel
                    {
                        ID = reader.GetInt16(0),
                        Latitude = (double)location.YCoordinate,
                        Longitude = (double)location.XCoordinate,
                        Name = reader.GetString(1)
                    };
                    _returnList.Add(_returnCity);
                }
                return await Task.FromResult(_returnList);
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

        public async Task<AuthorStruct> GetGetolocationMarkers(double lon, double lat)
        {
            MySqlConnection connection = GetConnection();
            List<BookEntityModel> _bookList = new List<BookEntityModel>();
            List<CityEntityModel> _cityList = new List<CityEntityModel>();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT distinct b.id, b.author, b.title, c.name, c.location, c.id FROM gutenberg.mentions m " +
                    "join gutenberg.book b on b.id = m.book_id " +
                    "join gutenberg.city c on c.id = m.city_id " +
                    "where ST_Distance_Sphere(c.location,GeomFromText(CONCAT('POINT(',@lon, ' ', @lat,')')))/1000 < 100";
                cmd.Parameters.AddWithValue("@lon", lon);
                cmd.Parameters.AddWithValue("@lat", lat);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var _tempBook = new BookEntityModel
                    {
                        ID = reader.GetInt16(0),
                        Author = reader.GetString(1),
                        Name = reader.GetString(2)
                    };
                    _bookList.Add(_tempBook);

                    var location = reader.GetMySqlGeometry(4);
                    var _returnCity = new CityEntityModel
                    {
                        ID = reader.GetInt16(5),
                        Latitude = (double)location.YCoordinate,
                        Longitude = (double)location.XCoordinate,
                        Name = reader.GetString(3)
                    };
                    _cityList.Add(_returnCity);
                }
                var _returnStruct = new AuthorStruct(_bookList, _cityList);
                return await Task.FromResult(_returnStruct);
            }
            catch (Exception e)
            {
                throw;
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
