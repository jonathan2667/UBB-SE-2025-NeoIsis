using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NeoIsisJob.Data;
using NeoIsisJob.Models;

namespace NeoIsisJob.Repos
{
    public class WorkoutTypeRepo
    {
        private readonly DatabaseHelper _dbHelper;

        public WorkoutTypeRepo() { this._dbHelper = new DatabaseHelper(); }

        public WorkoutTypeModel GetWorkoutTypeById(int wtid)
        {
            using (SqlConnection connection = this._dbHelper.GetConnection())
            {
                //open the connection
                connection.Open();

                //create the query
                string query = "SELECT * FROM WorkoutTypes WHERE WTID=@wtid";

                //create the command now
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@wtid", wtid);
                SqlDataReader reader = command.ExecuteReader();

                //now check if the type exists -> if yes return it
                if (reader.Read())
                {
                    return new WorkoutTypeModel(Convert.ToInt32(reader["WTID"]), Convert.ToString(reader["Name"]));
                }
            }

            //otherwise return empty instance
            return new WorkoutTypeModel();
        }

        public void InsertWorkoutType(String name)
        {
            //use the setup connection
            using (SqlConnection connection = this._dbHelper.GetConnection())
            {
                //open the connection
                connection.Open();

                //insert statement to insert the workout type
                string insertStatement = "INSERT INTO WorkoutTypes([Name]) VALUES (@name)";

                //now create the command and set the parameters
                SqlCommand command = new SqlCommand(insertStatement, connection);
                command.Parameters.AddWithValue("@name", name);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteWorkoutType(int wtid)
        {
            //use the setup connection
            using (SqlConnection connection = this._dbHelper.GetConnection())
            {
                //open the connection
                connection.Open();

                //delete statement
                string deleteStatement = "DELETE FROM WorkoutTypes WHERE WTID=@wtid";

                //now create the command and set the parameters
                SqlCommand command = new SqlCommand(deleteStatement, connection);
                command.Parameters.AddWithValue("@wtid", wtid);

                command.ExecuteNonQuery();
            }
        }

        public IList<WorkoutTypeModel> GetAllWorkoutTypes()
        {

            List<WorkoutTypeModel> workoutTypes = new List<WorkoutTypeModel>();

            using (SqlConnection connection = this._dbHelper.GetConnection())
            {
                //open the connection
                connection.Open();

                //create the query
                string query = "SELECT * FROM WorkoutTypes";

                //create the command now
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                //now check if the type exists -> if yes return it
                while (reader.Read())
                {
                    // Ensure data is not null before accessing it
                    string name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "Unknown";
                    int wtid = Convert.ToInt32(reader["WTID"]);

                    // Create WorkoutTypeModel and add to the list
                    workoutTypes.Add(new WorkoutTypeModel(wtid, name));
                }
            }

            //if no entry -> return empty list annyways
            return workoutTypes;
        }

        //eventually edit if it is needed
    }
}
