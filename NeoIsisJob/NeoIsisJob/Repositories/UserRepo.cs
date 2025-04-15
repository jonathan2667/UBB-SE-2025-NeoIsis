using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NeoIsisJob.Models;
using System.Data.SqlClient;
using NeoIsisJob.Data;
using System.Diagnostics;

namespace NeoIsisJob.Repositories
{
    public class UserRepo
    {
        private readonly DatabaseHelper _databaseHelper;

        public UserRepo(DatabaseHelper dbHelper)  { _databaseHelper = dbHelper; }

        public UserModel GetUserById(int userId)
        {
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                // open connection to database
                connection.Open();

                // query to get user by id
                string query = "SELECT UID FROM Users WHERE UID = @Id";

                // create command and set parameters
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", userId);
                SqlDataReader reader = command.ExecuteReader();

                // if user found return user
                if (reader.Read())
                {
                    return new UserModel(Convert.ToInt32(reader["UID"]));
                }

                // if no user found return null
                return new UserModel();
            }
        }

        public int InsertUser()
        {
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                // open connection to database
                connection.Open();

                // query to insert user
                string query = "INSERT INTO Users DEFAULT VALUES; SELECT SCOPE_IDENTITY();";

                // create command and set parameters
                SqlCommand command = new SqlCommand(query, connection);

                // execute command and get new user id
                int newUserId = Convert.ToInt32((decimal)command.ExecuteScalar());
                return newUserId;
            }
        }

        public bool DeleteUserById(int userId)
        {
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                // open connection to database
                connection.Open();

                // query to delete user by id
                string query = "DELETE FROM Users WHERE UID = @Id";

                // create command and set parameters
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", userId);

                // execute command and return true if user deleted
                return command.ExecuteNonQuery() > 0;
            }
        }

        public List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();

            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                try
                {
                    // open connection to database
                    connection.Open();

                    // query to get all users
                    string query = "SELECT UID FROM Users";

                    // create command and set parameters
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    // add all users to list
                    while (reader.Read())
                    {
                        users.Add(new UserModel(Convert.ToInt32(reader["UID"])));
                    }
                }
                catch (SqlException sqlEx)
                {
                    Debug.WriteLine($"SQL Error: {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"General Error: {ex.Message}");
                }
                // Connection automatically closes here
            }
            return users;
        }
    }
}
