using NeoIsisJob.Data;
using NeoIsisJob.Models;
using NeoIsisJob.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;

namespace NeoIsisJob.Repos
{
    public class UserWorkoutRepo : IUserWorkoutRepository
    {
        private readonly DatabaseHelper _databaseHelper;

        public UserWorkoutRepo(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public List<UserWorkoutModel> GetUserWorkoutModelByDate(DateTime date)
        {
            List<UserWorkoutModel> userWorkouts = new List<UserWorkoutModel>();
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "SELECT UID, WID, Date, Completed FROM UserWorkouts WHERE Date = @Date";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameters
                command.Parameters.AddWithValue("@Date", date);

                // Execute the command
                SqlDataReader reader = command.ExecuteReader();

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
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "SELECT UID, WID, Date, Completed FROM UserWorkouts WHERE UID = @UID AND WID = @WID AND Date = @Date";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameters
                command.Parameters.AddWithValue("@UID", userId);
                command.Parameters.AddWithValue("@WID", workoutId);
                command.Parameters.AddWithValue("@Date", date);

                // Execute the command
                SqlDataReader reader = command.ExecuteReader();

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
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "INSERT INTO UserWorkouts(UID, WID, Date, Completed) VALUES (@UID, @WID, @Date, @Completed)";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameters
                command.Parameters.AddWithValue("@UID", userWorkout.UserId);
                command.Parameters.AddWithValue("@WID", userWorkout.WorkoutId);
                command.Parameters.AddWithValue("@Date", userWorkout.Date);
                command.Parameters.AddWithValue("@Completed", userWorkout.Completed);

                // Execute the command
                command.ExecuteNonQuery();
            }
        }

        public void UpdateUserWorkout(UserWorkoutModel userWorkout)
        {
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "UPDATE UserWorkouts SET Completed = @Completed WHERE UID = @UID AND WID = @WID AND Date = @Date";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameters
                command.Parameters.AddWithValue("@UID", userWorkout.UserId);
                command.Parameters.AddWithValue("@WID", userWorkout.WorkoutId);
                command.Parameters.AddWithValue("@Date", userWorkout.Date);
                command.Parameters.AddWithValue("@Completed", userWorkout.Completed);

                // Execute the command
                command.ExecuteNonQuery();
            }
        }

        public void DeleteUserWorkout(int userId, int workoutId, DateTime date)
        {
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "DELETE FROM UserWorkouts WHERE UID = @UID AND WID = @WID AND Date = @Date";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameters
                command.Parameters.AddWithValue("@UID", userId);
                command.Parameters.AddWithValue("@WID", workoutId);
                command.Parameters.AddWithValue("@Date", date);

                // Execute the command
                command.ExecuteNonQuery();
            }
        }
    }
}