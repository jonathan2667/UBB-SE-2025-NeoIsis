using NeoIsisJob.Data;
using NeoIsisJob.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Repositories
{
    public class PersonalTrainerRepo
    {
        private readonly DatabaseHelper _databasebHelper;

        public PersonalTrainerRepo() { this._databasebHelper = new DatabaseHelper(); }

        public PersonalTrainerModel GetPersonalTrainerModelById(int personalTrainerId)
        {
            using (SqlConnection connection = _databasebHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "SELECT PTID, LastName, FirstName, WorksSince FROM PersonalTrainers WHERE PTID=@ptid";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameter
                command.Parameters.AddWithValue("@ptid", personalTrainerId);

                // Read the data
                SqlDataReader reader = command.ExecuteReader();

                // Check if the type exists -> if yes return it
                if (reader.Read())
                {
                    return new PersonalTrainerModel
                    {
                        Id = (int)reader["PTID"],
                        LastName = reader["LastName"].ToString() ?? string.Empty,
                        FirstName = reader["FirstName"].ToString() ?? string.Empty,
                        WorkStartDateTime = reader["WorksSince"] as DateTime? ?? SqlDateTime.MinValue.Value
                    };
                }

                // Otherwise return empty instance
                return new PersonalTrainerModel();
            }
        }

        public List<PersonalTrainerModel> GetAllPersonalTrainerModel()
        {
            List<PersonalTrainerModel> personalTrainers = new List<PersonalTrainerModel>();

            using (SqlConnection connection = this._databasebHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create the query
                string query = "SELECT PTID, LastName, FirstName, WorksSince FROM PersonalTrainers";

                // Create the command
                SqlCommand command = new SqlCommand(query, connection);

                // Read the data
                SqlDataReader reader = command.ExecuteReader();

                // Check if the type exists -> if yes return it
                while (reader.Read())
                {
                    personalTrainers.Add(new PersonalTrainerModel
                    {
                        Id = (int)reader["PTID"],
                        LastName = reader["LastName"].ToString() ?? string.Empty,
                        FirstName = reader["FirstName"].ToString() ?? string.Empty,
                        WorkStartDateTime = reader["WorksSince"] as DateTime? ?? SqlDateTime.MinValue.Value
                    });
                }

            }

            return personalTrainers;
        }

        public void AddPersonalTrainerModel(PersonalTrainerModel personalTrainer)
        {
            using (SqlConnection connection = _databasebHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create the query
                string query = "INSERT INTO PersonalTrainers (LastName, FirstName, WorksSince) VALUES (@lastName, @firstName, @worksSince)";

                // Create the command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameters
                command.Parameters.AddWithValue("@lastName", personalTrainer.LastName);
                command.Parameters.AddWithValue("@firstName", personalTrainer.FirstName);
                command.Parameters.AddWithValue("@worksSince", personalTrainer.WorkStartDateTime);

                // Execute the query
                command.ExecuteNonQuery();
            }
        }

        public void DeletePersonalTrainerModel(int personalTrainerId)
        {
            using (SqlConnection connection = _databasebHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create the query
                string query = "DELETE FROM PersonalTrainers WHERE PTID=@ptid";

                // Create the command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameter
                command.Parameters.AddWithValue("@ptid", personalTrainerId);

                // Execute the query
                command.ExecuteNonQuery();
            }
        }
    }
}
