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
    public class CompleteWorkoutRepo
    {
        private readonly DatabaseHelper _databaseHelper;

        public CompleteWorkoutRepo()
        {
            _databaseHelper = new DatabaseHelper();
        }

        public IList<CompleteWorkoutModel> GetAllCompleteWorkouts()
        {
            IList<CompleteWorkoutModel> completeWorkouts = new List<CompleteWorkoutModel>();

            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                connection.Open();

                String query = "SELECT * FROM CompleteWorkouts";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    completeWorkouts.Add(new CompleteWorkoutModel(Convert.ToInt32(reader["WID"]), Convert.ToInt32(reader["EID"]), Convert.ToInt32(reader["Sets"]), Convert.ToInt32(reader["RepsPerSet"])));
                }
            }

            return completeWorkouts;
        }

        //TODO -> implement the rest of CRUD
    }
}
