using NeoIsisJob.Data.Interfaces; // ✅ Use the interface
using NeoIsisJob.Models;
using NeoIsisJob.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NeoIsisJob.Repositories
{
    public class CompleteWorkoutRepo : ICompleteWorkoutRepository
    {
        private readonly IDatabaseHelper _databaseHelper;

        public CompleteWorkoutRepo(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public IList<CompleteWorkoutModel> GetAllCompleteWorkouts()
        {
            IList<CompleteWorkoutModel> completeWorkouts = new List<CompleteWorkoutModel>();
            string query = "SELECT * FROM CompleteWorkouts";

            try
            {
                var dataTable = _databaseHelper.ExecuteReader(query, null);
                foreach (System.Data.DataRow row in dataTable.Rows)
                {
                    completeWorkouts.Add(new CompleteWorkoutModel(
                        Convert.ToInt32(row["WID"]),
                        Convert.ToInt32(row["EID"]),
                        Convert.ToInt32(row["Sets"]),
                        Convert.ToInt32(row["RepsPerSet"])
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching complete workouts: " + ex.Message);
            }

            return completeWorkouts;
        }

        public void DeleteCompleteWorkoutsByWorkoutId(int workoutId)
        {
            string deleteCommand = "DELETE FROM CompleteWorkouts WHERE WID=@wid";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@wid", workoutId)
            };

            try
            {
                _databaseHelper.ExecuteNonQuery(deleteCommand, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting complete workouts: " + ex.Message);
            }
        }

        public void InsertCompleteWorkout(int workoutId, int exerciseId, int sets, int repetitionsPerSet)
        {
            string insertCommand = "INSERT INTO CompleteWorkouts(WID, EID, [Sets], RepsPerSet) VALUES (@wid, @eid, @sets, @repsPerSet)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@wid", workoutId),
                new SqlParameter("@eid", exerciseId),
                new SqlParameter("@sets", sets),
                new SqlParameter("@repsPerSet", repetitionsPerSet)
            };

            try
            {
                _databaseHelper.ExecuteNonQuery(insertCommand, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while inserting complete workout: " + ex.Message);
            }
        }
    }
}
