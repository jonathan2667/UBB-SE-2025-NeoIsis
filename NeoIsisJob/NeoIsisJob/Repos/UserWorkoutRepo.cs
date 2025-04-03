using NeoIsisJob.Data;
using NeoIsisJob.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NeoIsisJob.Repos
{
    public class UserWorkoutRepo
    {
        private readonly DatabaseHelper _dbHelper;

        public UserWorkoutRepo(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<UserWorkoutModel> GetUserWorkoutModelByDate(DateTime date)
        {
            List<UserWorkoutModel> userWorkouts = new List<UserWorkoutModel>();
            using (SqlConnection connection = _dbHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "SELECT UID, WID, Date, Completed FROM UserWorkouts WHERE Date = @Date";

                // Create a command
                SqlCommand cmd = new SqlCommand(query, connection);

                // Add the parameters
                cmd.Parameters.AddWithValue("@Date", date);

                // Execute the command
                SqlDataReader reader = cmd.ExecuteReader();

                // Read the data
                while (reader.Read())
                {
                    UserWorkoutModel userWorkout = new UserWorkoutModel(
                        (int)reader["UID"],
                        (int)reader["WID"],
                        (DateTime)reader["Date"],
                        (bool)reader["Completed"]
                    );
                    userWorkouts.Add(userWorkout);
                }
            }
            return userWorkouts;
        }

        public UserWorkoutModel GetUserWorkoutModel(int userId, int workoutId, DateTime date)
        {
            UserWorkoutModel userWorkout = null;
            using (SqlConnection connection = _dbHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "SELECT UID, WID, Date, Completed FROM UserWorkouts WHERE UID = @UID AND WID = @WID AND Date = @Date";

                // Create a command
                SqlCommand cmd = new SqlCommand(query, connection);

                // Add the parameters
                cmd.Parameters.AddWithValue("@UID", userId);
                cmd.Parameters.AddWithValue("@WID", workoutId);
                cmd.Parameters.AddWithValue("@Date", date);

                // Execute the command
                SqlDataReader reader = cmd.ExecuteReader();

                // Read the data
                if (reader.Read())
                {
                    userWorkout = new UserWorkoutModel(
                        (int)reader["UID"],
                        (int)reader["WID"],
                        (DateTime)reader["Date"],
                        (bool)reader["Completed"]
                    );
                }
            }
            return userWorkout;
        }

        public void AddUserWorkout(UserWorkoutModel userWorkout)
        {
            using (SqlConnection connection = _dbHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "INSERT INTO UserWorkouts(UID, WID, Date, Completed) VALUES (@UID, @WID, @Date, @Completed)";

                // Create a command
                SqlCommand cmd = new SqlCommand(query, connection);

                // Add the parameters
                cmd.Parameters.AddWithValue("@UID", userWorkout.UserId);
                cmd.Parameters.AddWithValue("@WID", userWorkout.WorkoutId);
                cmd.Parameters.AddWithValue("@Date", userWorkout.Date);
                cmd.Parameters.AddWithValue("@Completed", userWorkout.Completed);

                // Execute the command
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateUserWorkout(UserWorkoutModel userWorkout)
        {
            using (SqlConnection connection = _dbHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "UPDATE UserWorkouts SET Completed = @Completed WHERE UID = @UID AND WID = @WID AND Date = @Date";

                // Create a command
                SqlCommand cmd = new SqlCommand(query, connection);

                // Add the parameters
                cmd.Parameters.AddWithValue("@UID", userWorkout.UserId);
                cmd.Parameters.AddWithValue("@WID", userWorkout.WorkoutId);
                cmd.Parameters.AddWithValue("@Date", userWorkout.Date);
                cmd.Parameters.AddWithValue("@Completed", userWorkout.Completed);

                // Execute the command
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUserWorkout(int userId, int workoutId, DateTime date)
        {
            using (SqlConnection connection = _dbHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "DELETE FROM UserWorkouts WHERE UID = @UID AND WID = @WID AND Date = @Date";

                // Create a command
                SqlCommand cmd = new SqlCommand(query, connection);

                // Add the parameters
                cmd.Parameters.AddWithValue("@UID", userId);
                cmd.Parameters.AddWithValue("@WID", workoutId);
                cmd.Parameters.AddWithValue("@Date", date);

                // Execute the command
                cmd.ExecuteNonQuery();
            }
        }
    }
}