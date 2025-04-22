using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NeoIsisJob.Data;
using NeoIsisJob.Data.Interfaces;
using NeoIsisJob.Models;
using NeoIsisJob.Repositories.Interfaces;

namespace NeoIsisJob.Repositories
{
    public class ExerciseRepo : IExerciseRepository
    {
        private readonly IDatabaseHelper databaseHelper;

        public ExerciseRepo()
        {
            this.databaseHelper = new DatabaseHelper();
        }

        public ExerciseRepo(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }

        public IList<ExercisesModel> GetAllExercises()
        {
            IList<ExercisesModel> exercises = new List<ExercisesModel>();
            string query = "SELECT * FROM Exercises";

            try
            {
                var dataTable = databaseHelper.ExecuteReader(query, null);

                foreach (DataRow row in dataTable.Rows)
                {
                    exercises.Add(new ExercisesModel(
                        Convert.ToInt32(row["EID"]),
                        Convert.ToString(row["Name"]),
                        Convert.ToString(row["Description"]),
                        Convert.ToInt32(row["Difficulty"]),
                        Convert.ToInt32(row["MGID"])));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching exercises: " + ex.Message);
            }

            return exercises;
        }

        public ExercisesModel? GetExerciseById(int exerciseId)
        {
            ExercisesModel? exercise = null;
            string query = "SELECT * FROM Exercises WHERE EID=@eid";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@eid", exerciseId)
            };

            try
            {
                var dataTable = databaseHelper.ExecuteReader(query, parameters);

                if (dataTable.Rows.Count > 0)
                {
                    var row = dataTable.Rows[0];
                    exercise = new ExercisesModel(
                        Convert.ToInt32(row["EID"]),
                        Convert.ToString(row["Name"]),
                        Convert.ToString(row["Description"]),
                        Convert.ToInt32(row["Difficulty"]),
                        Convert.ToInt32(row["MGID"]));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching exercise by ID: " + ex.Message);
            }

            return exercise;
        }

        // TODO -> implement the rest of CRUD if needed
    }
}
