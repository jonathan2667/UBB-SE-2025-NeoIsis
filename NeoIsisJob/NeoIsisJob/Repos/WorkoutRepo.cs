using NeoIsisJob.Data;
using NeoIsisJob.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Repos
{
    public class WorkoutRepo
    {
        private readonly DatabaseHelper _databaseHelper;

        public WorkoutRepo() { this._databaseHelper = new DatabaseHelper(); }

        public WorkoutModel GetWorkoutById(int wid)
        {
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                //open the connection
                connection.Open();

                //create the query
                string query = "SELECT * FROM Workouts WHERE WID=@wid";

                //create the command now
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@wid", wid);
                SqlDataReader reader = command.ExecuteReader();

                //now check if the type exists -> if yes return it
                if (reader.Read())
                {
                    //if ok return it
                    return new WorkoutModel(Convert.ToInt32(reader["WID"]), Convert.ToString(reader["Name"]), Convert.ToInt32(reader["WTID"]));
                }
            }

            //if not found -> return empty object
            return new WorkoutModel();
        }

        public WorkoutModel GetWorkoutByName(String name)
        {
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                //open the connection
                connection.Open();

                //create the query
                string query = "SELECT * FROM Workouts WHERE Name=@name";

                //create the command now
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                SqlDataReader reader = command.ExecuteReader();

                //now check if the type exists -> if yes return it
                if (reader.Read())
                {
                    //if ok return it
                    return new WorkoutModel(Convert.ToInt32(reader["WID"]), Convert.ToString(reader["Name"]), Convert.ToInt32(reader["WTID"]));
                }
            }

            //if not found -> return empty object
            return new WorkoutModel();
        }

        public void InsertWorkout(string name, int wtid)
        {
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                //open the connection
                connection.Open();

                //create the insert statement
                string insertStatement = "INSERT INTO Workouts([Name], WTID) VALUES (@name, @wtid)";

                //create the command, add params and execute it
                SqlCommand command = new SqlCommand(insertStatement, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@wtid", wtid);
                command.ExecuteNonQuery();
            }
        }
   
        public void DeleteWorkout(int wid)
        {
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                connection.Open();

                string deleteStatement = "DELETE FROM Workouts WHERE WID=@wid";

                SqlCommand command = new SqlCommand(deleteStatement, connection);
                command.Parameters.AddWithValue("@wid", wid);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateWorkout(WorkoutModel workout)
        {
            if (workout == null)
                throw new ArgumentNullException(nameof(workout), "Workout cannot be null.");

            string checkQuery = "SELECT COUNT(*) FROM Workouts WHERE Name = @Name AND WID != @Id";
            string updateQuery = "UPDATE Workouts SET Name = @Name WHERE WID = @Id";

            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                connection.Open();

                // Check for duplicate names
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Name", workout.Name);
                    checkCommand.Parameters.AddWithValue("@Id", workout.Id);

                    int count = (int)checkCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        throw new Exception("A workout with this name already exists.");
                    }
                }

                // Perform the update
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@Name", workout.Name);
                    updateCommand.Parameters.AddWithValue("@Id", workout.Id);

                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("No workout was updated. Ensure the workout ID exists.");
                    }
                }
            }
        }

        public IList<WorkoutModel> GetAllWorkouts()
        {

            IList<WorkoutModel> workouts = new List<WorkoutModel>();

            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                //open the connection
                connection.Open();

                //create the query
                string query = "SELECT * FROM Workouts";

                //create the command now
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    // Create WorkoutTypeModel and add to the list
                    workouts.Add(new WorkoutModel(Convert.ToInt32(reader["WID"]), Convert.ToString(reader["Name"]), Convert.ToInt32(reader["WTID"])));
                }
            }

            return workouts;
        }
    }
}
