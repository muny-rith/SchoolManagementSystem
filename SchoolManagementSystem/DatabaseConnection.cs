using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.Data;


namespace SchoolManagementSystem
{
    public sealed class DatabaseConnection
    {

        //MSSQLLocalDB
        private static readonly Lazy<DatabaseConnection> _instance = new(() => new DatabaseConnection());
        private SqlConnection _connection;
        private string _connectionString => $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"Data\DB\{Constants.DB}")};Integrated Security=True;";

        //private readonly string _connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\parttime\DB\dbSMS.mdf;Integrated Security=True;"; // ✅ store this
        private DatabaseConnection()
        {

            _connection = new SqlConnection(_connectionString);
        }

        public static DatabaseConnection Instance => _instance.Value;

        public SqlConnection GetConnection()
        {

            if (_connection == null || _connection.State == ConnectionState.Closed) {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
            } 

            return _connection;
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
                _connection.Close();
        }
    }
}
