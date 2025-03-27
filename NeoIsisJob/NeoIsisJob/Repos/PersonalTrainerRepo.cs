using NeoIsisJob.Data;
using NeoIsisJob.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Repos
{
    public class PersonalTrainerRepo
    {
        private readonly DatabaseHelper _dbHelper;

        public PersonalTrainerRepo(DatabaseHelper dbHelper) { _dbHelper = dbHelper; }

        public PersonalTrainerModel GetPersonalTrainerModelById(int ptid)
        {
            using (SqlConnection connection = _dbHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "SELECT PTID, LastName, FirstName, WorksSince FROM PersonalTrainers WHERE PTID=@ptid";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameter
                command.Parameters.AddWithValue("@ptid", ptid);

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
                        WorksSince = reader["WorksSince"] as DateTime? ?? SqlDateTime.MinValue.Value
                    };
                }

                // Otherwise return empty instance
                return new PersonalTrainerModel();
            }
        }

        public List<PersonalTrainerModel> GetAllPersonalTrainerModel()
        {
            List<PersonalTrainerModel> personalTrainers = new List<PersonalTrainerModel>();

            using (SqlConnection connection = this._dbHelper.GetConnection())
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
                        WorksSince = reader["WorksSince"] as DateTime? ?? SqlDateTime.MinValue.Value
                    });
                }

            }

            return personalTrainers;
        }

        public void AddPersonalTrainerModel(PersonalTrainerModel personalTrainer)
        {
            using (SqlConnection connection = _dbHelper.GetConnection())
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
                command.Parameters.AddWithValue("@worksSince", personalTrainer.WorksSince);

                // Execute the query
                command.ExecuteNonQuery();
            }
        }

        public void DeletePersonalTrainerModel(int ptid)
        {
            using (SqlConnection connection = _dbHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create the query
                string query = "DELETE FROM PersonalTrainers WHERE PTID=@ptid";

                // Create the command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameter
                command.Parameters.AddWithValue("@ptid", ptid);

                // Execute the query
                command.ExecuteNonQuery();
            }
        }
    }
}
