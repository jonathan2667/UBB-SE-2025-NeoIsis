using System;
using System.Data.SqlClient;
using System.Diagnostics;

/// <summary>
/// In the case of an error caused by SQLConnection
/// try this (Toold -> NuGet Package Manager -> Package Manager Console):
/// Install-Package System.Data.SqlClient
/// </summary>

namespace NeoIsisJob.Data
{
    internal class DatabaseHelper
    {
        private readonly string connectionString;

        public DatabaseHelper()
        {
            // change this to your own connection string
            connectionString = @"Server=DESKTOP-4FNFF2T;Database=Workout;Integrated Security=True;TrustServerCertificate=True;";
        }

        public void OpenConnection()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Debug.WriteLine("Database connection opened successfully.");
                }
                catch (SqlException sqlEx)
                {
                    Debug.WriteLine($"SQL Error: {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"General Error: {ex.Message}");
                }
            }
        }

        public void CloseConnection()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Close();
                    Debug.WriteLine("Database connection closed successfully.");
                }
                catch (SqlException sqlEx)
                {
                    Debug.WriteLine($"SQL Error: {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"General Error: {ex.Message}");
                }
            }
        }
    }
}
