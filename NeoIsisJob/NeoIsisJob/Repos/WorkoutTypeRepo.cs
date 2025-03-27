using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoIsisJob.Data;
using NeoIsisJob.Models;

namespace NeoIsisJob.Repos
{
    internal class WorkoutTypeRepo
    {
        private readonly DatabaseHelper _dbHelper;

        public WorkoutTypeRepo() { this._dbHelper = new DatabaseHelper(); }

        public WorkoutTypeModel GetWorkoutTypeModelById(int wtid)
        {
            using (SqlConnection connection = this._dbHelper.GetConnection())
            {
                //open the connection
                connection.Open();

                //create the query
                string query = "SELECT FROM WorkoutTypes WHERE WTID=@wtid";

                //create the command now
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@wtid", wtid);
                SqlDataReader reader = command.ExecuteReader();

                //now check if the type exists -> if yes return it
                if(reader.Read())
                {
                    return new WorkoutTypeModel(Convert.ToString(reader["Name"]));
                }
            }

            //otherwise return empty instance
            return new WorkoutTypeModel();
        }

        //TODO rest of crud
    }
}
