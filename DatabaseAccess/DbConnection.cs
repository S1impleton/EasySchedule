﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;
using System.Data;

namespace DatabaseAccess
{
    public class DbConnection : IDisposable
    {
        private readonly SqlConnection _connection;
        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public DbConnection()
        {
            _connection = new SqlConnection(KrakaConnectionString());
        }

        public SqlConnection GetConnection()
        {
            _connection.Open();
            return _connection;

        }

        public void OpenConnection()
        {
            try
            {
                _connection.Open();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void CloseConnection()
        {
            try
            {
                _connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool IsConnected()
        {
            return _connection.State == ConnectionState.Open;
        }

        public bool IsDisconnected()
        {
            return _connection.State == ConnectionState.Closed;
        }

        public string LocalConnectionString()
        {
            Server = ".\\sqlexpress";
            Database = "Semester3DB";
            return "Data Source=" + Server + ";Initial Catalog=" + Database + ";Integrated Security=True";
        }

        public string KrakaConnectionString()
        {
            Server = "kraka.ucn.dk";
            Database = "dmab0916_1062358";
            Username = "dmab0916_1062358";
            Password = "Password1!";
            string connectionTimeOut = "120";
            return "Data Source=" + Server + "; Initial Catalog=" + Database + ";User Id=" + Username + ";Password=" + Password + ";" + "Connection Timeout=" + connectionTimeOut +";";
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
