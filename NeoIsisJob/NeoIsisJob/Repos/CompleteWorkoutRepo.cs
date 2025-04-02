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

        //deletes all entries from CompleteWorkouts table which have the specified WID
        public void DeleteCompleteWorkoutsByWid(int wid)
        {
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                connection.Open();

                String deleteCommand = "DELETE FROM CompleteWorkouts WHERE WID=@wid";
                SqlCommand command = new SqlCommand(deleteCommand, connection);
                command.Parameters.AddWithValue("@wid", wid);

                command.ExecuteNonQuery();
            }
        }

        public void InsertCompleteWorkout(int wid, int eid, int sets, int repsPerSet)
        {
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                connection.Open();

                String insertCommand = "INSERT INTO CompleteWorkouts(WID, EID, [Sets], RepsPerSet) VALUES (@wid, @eid, @sets, @repsPerSet)";
                SqlCommand command = new SqlCommand(insertCommand, connection);
                command.Parameters.AddWithValue("@wid", wid);
                command.Parameters.AddWithValue("@eid", eid);
                command.Parameters.AddWithValue("@sets", sets);
                command.Parameters.AddWithValue("@repsPerSet", repsPerSet);

                command.ExecuteNonQuery();
            }
        }

        //TODO -> implement the rest of CRUD
    }
}
