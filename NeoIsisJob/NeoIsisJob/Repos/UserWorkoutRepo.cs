using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoIsisJob.Data;
using NeoIsisJob.Models;
using System.Data.SqlClient;

namespace NeoIsisJob.Repos
{
    public class UserWorkoutRepo
    {
        private readonly DatabaseHelper _dbHelper;

        public UserWorkoutRepo(DatabaseHelper dbHelper) { _dbHelper = dbHelper; }

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
    }
}
