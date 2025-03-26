using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace NeoIsisJob.Data
{
    public class DatabaseHelper
    {
        private readonly string connectionString;

        // Store the connection string in one place
        public DatabaseHelper()
        {
            connectionString = @"Server=DESKTOP-4FNFF2T;Database=Workout;Integrated Security=True;TrustServerCertificate=True;";
        }

        // Provide an open connection to be used in repositories
        public SqlConnection GetConnection() { return new SqlConnection(connectionString); }
    }
}
