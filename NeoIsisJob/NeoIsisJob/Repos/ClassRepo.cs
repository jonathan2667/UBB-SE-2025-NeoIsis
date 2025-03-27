using NeoIsisJob.Data;
using NeoIsisJob.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Repos
{
    public class ClassRepo
    {
        private readonly DatabaseHelper _dbHelper;

        public ClassRepo(DatabaseHelper dbHelper) { _dbHelper = dbHelper; }

        public ClassModel GetClassModelById(int cid)
        {
            using (SqlConnection connection = _dbHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "SELECT Cid, Name, Description, Ctid, Ptid FROM Class WHERE Cid = @cid";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameter
                command.Parameters.AddWithValue("@cid", cid);

                // Execute the command
                SqlDataReader reader = command.ExecuteReader();

                // Read the data
                if (reader.Read())
                {
                    // Return the class model
                    return new ClassModel
                    {
                        Id = (int)reader["Cid"],
                        Name = reader["Name"].ToString() ?? string.Empty,
                        Description = reader["Description"].ToString() ?? string.Empty,
                        ClassTypeId = (int)reader["Ctid"],
                        PersonalTrainerId = (int)reader["Ptid"]
                    };
                }

                // Return an empty instance
                return new ClassModel();
            }
        }

        public List<ClassModel> GetAllClassModel()
        {
            List<ClassModel> classes = new List<ClassModel>();

            using (SqlConnection connection = _dbHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "SELECT Cid, Name, Description, Ctid, Ptid FROM Class";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Execute the command
                SqlDataReader reader = command.ExecuteReader();

                // Read the data
                while (reader.Read())
                {
                    classes.Add(new ClassModel
                    {
                        Id = (int)reader["Cid"],
                        Name = reader["Name"].ToString() ?? string.Empty,
                        Description = reader["Description"].ToString() ?? string.Empty,
                        ClassTypeId = (int)reader["Ctid"],
                        PersonalTrainerId = (int)reader["Ptid"]
                    });
                }

                // Return the classes
                return classes;
            }
        }

        public void AddClassModel(ClassModel classModel)
        {
            using (SqlConnection connection = _dbHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "INSERT INTO Class (Name, Description, Ctid, Ptid) VALUES (@name, @description, @ctid, @ptid)";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameters
                command.Parameters.AddWithValue("@name", classModel.Name);
                command.Parameters.AddWithValue("@description", classModel.Description);
                command.Parameters.AddWithValue("@ctid", classModel.ClassTypeId);
                command.Parameters.AddWithValue("@ptid", classModel.PersonalTrainerId);

                // Execute the command
                command.ExecuteNonQuery();
            }
        }

        public void DeleteClassModel(int cid)
        {
            using (SqlConnection connection = _dbHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "DELETE FROM Class WHERE Cid = @cid";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameter
                command.Parameters.AddWithValue("@cid", cid);

                // Execute the command
                command.ExecuteNonQuery();
            }
        }
    }
}
