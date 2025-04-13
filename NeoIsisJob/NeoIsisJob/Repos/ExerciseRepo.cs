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
    class ExerciseRepo
    {
        private readonly DatabaseHelper _databaseHelper;

        public ExerciseRepo()
        {
            this._databaseHelper = new DatabaseHelper();
        }

        public IList<ExercisesModel> GetAllExercises()
        {
            IList<ExercisesModel> exercises = new List<ExercisesModel>();

            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                connection.Open();

                String query = "SELECT * FROM Exercises";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ExercisesModel exercise = new ExercisesModel(
                        Convert.ToInt32(reader["EID"]),
                        Convert.ToString(reader["Name"]),
                        Convert.ToString(reader["Description"]),
                        Convert.ToInt32(reader["Difficulty"]),
                        Convert.ToInt32(reader["MGID"])
                    );
                    exercises.Add(exercise);
                }
            }

            return exercises;
        }

        public ExercisesModel GetExerciseById(int exerciseId)
        {
            //return it as null if not found
            ExercisesModel? exercise = null;

            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                connection.Open();

                String query = "SELECT * FROM Exercises WHERE EID=@eid";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@eid", exerciseId);
                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    exercise = new ExercisesModel(
                        Convert.ToInt32(reader["EID"]), 
                        Convert.ToString(reader["Name"]), 
                        Convert.ToString(reader["Description"]), 
                        Convert.ToInt32(reader["Difficulty"]), 
                        Convert.ToInt32(reader["MGID"])
                    );
                }
            }

            return exercise;
        }

        //TODO -> implement the rest of CRUD if needed
    }
}
