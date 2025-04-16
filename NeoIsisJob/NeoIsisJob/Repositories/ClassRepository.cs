using NeoIsisJob.Data;
using NeoIsisJob.Models;
using NeoIsisJob.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly DatabaseHelper _databaseHelper;

        public ClassRepository() { this._databaseHelper = new DatabaseHelper(); }

        public ClassModel GetClassById(int classId)
        {
            return GetClassModelById(classId);
        }

        public ClassModel GetClassModelById(int classId)
        {
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "SELECT CID, Name, Description, CTID, PTID FROM Classes WHERE Cid = @cid";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameter
                command.Parameters.AddWithValue("@cid", classId);

                // Execute the command
                SqlDataReader reader = command.ExecuteReader();

                // Read the data
                if (reader.Read())
                {
                    // Return the class model
                    return new ClassModel
                    {
                        Id = (int)reader["CID"],
                        Name = reader["Name"].ToString() ?? string.Empty,
                        Description = reader["Description"].ToString() ?? string.Empty,
                        ClassTypeId = (int)reader["CTID"],
                        PersonalTrainerId = (int)reader["PTID"]
                    };
                }

                // Return an empty instance
                return new ClassModel();
            }
        }

        public List<ClassModel> GetAllClasses()
        {
            return GetAllClassModel();
        }

        public List<ClassModel> GetAllClassModel()
        {
            List<ClassModel> classes = new List<ClassModel>();
            try
            {
                using (SqlConnection connection = this._databaseHelper.GetConnection())
                {
                    // Open the connection
                    connection.Open();

                    // Create a query
                    string query = "SELECT CID, Name, Description, CTID, PTID FROM Classes";

                    // Create a command
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command
                    SqlDataReader reader = command.ExecuteReader();

                    // Read the data
                    while (reader.Read())
                    {
                        classes.Add(new ClassModel
                        {
                            Id = (int)reader["CID"],
                            Name = reader["Name"].ToString() ?? string.Empty,
                            Description = reader["Description"].ToString() ?? string.Empty,
                            ClassTypeId = (int)reader["CTID"],
                            PersonalTrainerId = (int)reader["PTID"]
                        });
                    }

                    // Return the classes
                    return classes;
                }
            }catch(Exception ex) { throw new Exception(ex.Message); }
        }

        public List<ClassModel> GetClassesForUserOnDate(int userId, DateTime date)
        {
            List<ClassModel> classes = new List<ClassModel>();
            
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query to get class IDs for the user on the specified date
                string query = @"
                    SELECT C.CID, C.Name, C.Description, C.CTID, C.PTID 
                    FROM Classes C
                    INNER JOIN UserClasses UC ON C.CID = UC.CID
                    WHERE UC.UID = @userId AND UC.Date = @date";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameters
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@date", date.Date);

                // Execute the command
                SqlDataReader reader = command.ExecuteReader();

                // Read the data
                while (reader.Read())
                {
                    classes.Add(new ClassModel
                    {
                        Id = (int)reader["CID"],
                        Name = reader["Name"].ToString() ?? string.Empty,
                        Description = reader["Description"].ToString() ?? string.Empty,
                        ClassTypeId = (int)reader["CTID"],
                        PersonalTrainerId = (int)reader["PTID"]
                    });
                }
            }

            return classes;
        }

        public void AssignClassToUser(int userId, int classId, DateTime date)
        {
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "INSERT INTO UserClasses (UID, CID, Date) VALUES (@userId, @classId, @date)";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameters
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@classId", classId);
                command.Parameters.AddWithValue("@date", date.Date);

                // Execute the command
                command.ExecuteNonQuery();
            }
        }

        public void RemoveClassFromUser(int userId, int classId, DateTime date)
        {
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "DELETE FROM UserClasses WHERE UID = @userId AND CID = @classId AND Date = @date";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameters
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@classId", classId);
                command.Parameters.AddWithValue("@date", date.Date);

                // Execute the command
                command.ExecuteNonQuery();
            }
        }

        public void AddClassModel(ClassModel classModel)
        {
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "INSERT INTO Classes (Name, Description, CTID, PTID) VALUES (@name, @description, @ctid, @ptid)";

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

        public void DeleteClassModel(int classId)
        {
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                // Open the connection
                connection.Open();

                // Create a query
                string query = "DELETE FROM Classes WHERE CID = @cid";

                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add the parameter
                command.Parameters.AddWithValue("@cid", classId);

                // Execute the command
                command.ExecuteNonQuery();
            }
        }
    }
}
