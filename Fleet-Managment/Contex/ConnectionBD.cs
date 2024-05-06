using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Fleet_Managment
{
    public class ConnectionBD
    {
        private readonly IConfiguration _configuration;

        public ConnectionBD(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public NpgsqlConnection OpenConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public void CloseConnection(NpgsqlConnection connection)
        {
            connection.Close();
        }
    }
}


//using Npgsql;

//namespace Fleet_Managment
//{
//    public class ConnectionBD
//    {
//        public string ConnectionString { get; } = "Server=ep-empty-sound-a4z7ib7l.us-east-1.aws.neon.tech;Port=5432;UserId=default;Password=XBZoSd2r8FuY;Database=verceldb";

//        public NpgsqlConnection OpenConnection()
//        {
//            var connection = new NpgsqlConnection(ConnectionString);
//            connection.Open();
//            return connection;
//        }

//        public void CloseConnection(NpgsqlConnection connection)
//        {
//            connection.Close();
//        }
//    }
//}



//using Microsoft.EntityFrameworkCore;
//using System;
//using Npgsql;

//namespace Fleet_Managment.Contex
//{
//    public class ConnectionBD
//    {
//        private NpgsqlConnection connet;

//        public ConnectionBD()
//        {
//            //connet = new NpgsqlConnection("Server = ep-empty-sound-a4z7ib7l.us-east-1.aws.neon.tech; Port: 5432; User Id = default; Password = XBZoSd2r8FuY; Database = verceldb");
//            connet = new NpgsqlConnection("Server=ep-empty-sound-a4z7ib7l.us-east-1.aws.neon.tech;Port=5432;UserId=default;Password=XBZoSd2r8FuY;Database=verceldb");
//        }

//        public NpgsqlConnection Open_Connection()
//        {
//            try
//            {
//                connet.Open();
//                return connet;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
//                throw;
//            }
//        }

//        public void Close_Connetion()
//        {
//            connet.Close();
//        }

//    }
//}

