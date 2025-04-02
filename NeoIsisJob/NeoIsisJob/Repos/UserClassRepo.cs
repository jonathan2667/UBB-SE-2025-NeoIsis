using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoIsisJob.Data;
using NeoIsisJob.Models;

namespace NeoIsisJob.Repos
{
    public class UserClassRepo
    {
        private readonly DatabaseHelper _dbHelper;

        public UserClassRepo(DatabaseHelper dbHelper) { _dbHelper = dbHelper; }

        public UserClassModel GetUserClassModelById(int userId, int classId, DateTime enrollmentDate)
        {
            UserClassModel userClass = new UserClassModel();

            using (SqlConnection connection = _dbHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "SELECT UID, CID, Date FROM UserClasses WHERE UID = @UID AND CID = @CID AND Date = @Date";

                // Create a command
                SqlCommand cmd = new SqlCommand(query, connection);

                // Add the parameters
                cmd.Parameters.AddWithValue("@UID", userId);
                cmd.Parameters.AddWithValue("@CID", classId);
                cmd.Parameters.AddWithValue("@Date", enrollmentDate);

                // Execute the command
                SqlDataReader reader = cmd.ExecuteReader();

                // Read the data
                if (reader.Read())
                {
                    userClass = new UserClassModel(
                        (int)reader["UID"],
                        (int)reader["CID"],
                        (DateTime)reader["Date"]
                    );

                    return userClass;
                }
            }

            return new UserClassModel();
        }

        public List<UserClassModel> GetAllUserClassModel()
        {
            List<UserClassModel> userClasses = new List<UserClassModel>();

            using (SqlConnection connection = _dbHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "SELECT UID, CID, Date FROM UserClasses";

                // Create a command
                SqlCommand cmd = new SqlCommand(query, connection);

                // Execute the command
                SqlDataReader reader = cmd.ExecuteReader();

                // Read the data
                while (reader.Read())
                {
                    userClasses.Add(new UserClassModel(
                        (int)reader["UID"],
                        (int)reader["CID"],
                        (DateTime)reader["Date"]
                    ));
                }
            }
            return userClasses;
        }

        public void AddUserClassModel(UserClassModel userClass)
        {
            using (SqlConnection connection = _dbHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "INSERT INTO UserClasses (UID, CID, Date) VALUES (@UID, @CID, @Date)";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameters
                command.Parameters.AddWithValue("@UID", userClass.UserId);
                command.Parameters.AddWithValue("@Date", userClass.EnrollmentDate);
                command.Parameters.AddWithValue("@CID", userClass.ClassId);

                // Execute the command
                command.ExecuteNonQuery();
            }
        }

        public void DeleteUserClassModel(int userId, int classId, DateTime enrollmentDate)
        {
            using (SqlConnection connection = _dbHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "DELETE FROM UserClasses WHERE UID = @UID AND CID = @CID AND Date = @Date";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameters
                command.Parameters.AddWithValue("@UID", userId);
                command.Parameters.AddWithValue("@CID", classId);
                command.Parameters.AddWithValue("@Date", enrollmentDate);

                // Execute the command
                command.ExecuteNonQuery();
            }
        }

        public List<UserClassModel> GetUserClassModelByDate(DateTime date)
        {
            List<UserClassModel> userClasses = new List<UserClassModel>();
            using (SqlConnection connection = _dbHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "SELECT UID, CID, Date FROM UserClasses WHERE Date = @Date";

                // Create a command
                SqlCommand cmd = new SqlCommand(query, connection);

                // Add the parameters
                cmd.Parameters.AddWithValue("@Date", date);

                // Execute the command
                SqlDataReader reader = cmd.ExecuteReader();

                // Read the data
                while (reader.Read())
                {
                    userClasses.Add(new UserClassModel(
                        (int)reader["UID"],
                        (int)reader["CID"],
                        (DateTime)reader["Date"]
                    ));
                }
            }
            return userClasses;
        }
    }
}
