using NeoIsisJob.Data;
using NeoIsisJob.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                connection.Open();

                string updateStatement = "UPDATE Worokuts SET [Name]=@name, WTID=@wtid WHERE WID=@wid";
                SqlCommand command = new SqlCommand(updateStatement, connection);
                command.Parameters.AddWithValue("@name", workout.Name);
                command.Parameters.AddWithValue("@wtid", workout.WorkoutTypeId);
                command.Parameters.AddWithValue("@wid", workout.Id);
                command.ExecuteNonQuery();
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
