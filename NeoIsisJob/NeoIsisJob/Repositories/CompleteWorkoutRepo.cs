using NeoIsisJob.Data;
using NeoIsisJob.Models;
using NeoIsisJob.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Repositories
{
    public class CompleteWorkoutRepo : ICompleteWorkoutRepository
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

        //deletes all entries from CompleteWorkouts table which have the specified WID
        public void DeleteCompleteWorkoutsByWorkoutId(int workoutId)
        {
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                connection.Open();

                String deleteCommand = "DELETE FROM CompleteWorkouts WHERE WID=@wid";
                SqlCommand command = new SqlCommand(deleteCommand, connection);
                command.Parameters.AddWithValue("@wid", workoutId);

                command.ExecuteNonQuery();
            }
        }

        public void InsertCompleteWorkout(int workoutId, int exerciseId, int sets, int repetitionsPerSet)
        {
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                connection.Open();

                String insertCommand = "INSERT INTO CompleteWorkouts(WID, EID, [Sets], RepsPerSet) VALUES (@wid, @eid, @sets, @repsPerSet)";
                SqlCommand command = new SqlCommand(insertCommand, connection);
                command.Parameters.AddWithValue("@wid", workoutId);
                command.Parameters.AddWithValue("@eid", exerciseId);
                command.Parameters.AddWithValue("@sets", sets);
                command.Parameters.AddWithValue("@repsPerSet", repetitionsPerSet);

                command.ExecuteNonQuery();
            }
        }

        //TODO -> implement the rest of CRUD
    }
}
