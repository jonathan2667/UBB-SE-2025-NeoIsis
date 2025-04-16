using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using NeoIsisJob.Data;
using NeoIsisJob.Models;
using NeoIsisJob.Repositories.Interfaces;

namespace NeoIsisJob.Repositories
{
    public class ClassTypeRepository : IClassTypeRepository
    {
        private readonly DatabaseHelper _databaseHelper;

        public ClassTypeRepository() { this._databaseHelper = new DatabaseHelper(); }

        public ClassTypeModel GetClassTypeModelById(int classTypeId)
        {
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                // open the connection
                connection.Open();

                // create the query
                string query = "SELECT CTID, Name FROM ClassTypes WHERE CTID=@ctid";

                // create the command now
                SqlCommand command = new SqlCommand(query, connection);

                // add the parameter
                command.Parameters.AddWithValue("@ctid", classTypeId);

                // read the data
                SqlDataReader reader = command.ExecuteReader();

                // now check if the type exists -> if yes return it
                if (reader.Read())
                {
                    return new ClassTypeModel(Convert.ToInt32(reader["WTID"]), Convert.ToString(reader["Name"]) ?? string.Empty);
                }

                // otherwise return empty instance
                return new ClassTypeModel();
            }
        }

        public List<ClassTypeModel> GetAllClassTypeModel()
        {
            List<ClassTypeModel> classTypes = new List<ClassTypeModel>();

            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                // open the connection
                connection.Open();

                // create the query
                string query = "SELECT CTID, Name from ClassTypes";

                // create the command now
                SqlCommand command = new SqlCommand(query, connection);

                // read the data
                SqlDataReader reader = command.ExecuteReader();

                // now check if the type exists -> if yes return it
                while (reader.Read())
                {
                    classTypes.Add(new ClassTypeModel(Convert.ToInt32(reader["CTID"]), Convert.ToString(reader["Name"]) ?? string.Empty));
                }
            }

            return classTypes;
        }

        public void AddClassTypeModel(ClassTypeModel classType)
        {
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                // open the connection
                connection.Open();
                
                // create the query
                string query = "INSERT INTO ClassTypes (Name) VALUES (@name)";
                
                // create the command now
                SqlCommand command = new SqlCommand(query, connection);
                
                // add the parameter
                command.Parameters.AddWithValue("@name", classType.Name);
                
                // execute the query
                command.ExecuteNonQuery();
            }
        }

        public void DeleteClassTypeModel(int classTypeId)
        {
            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                // open the connection
                connection.Open();

                // create the query
                string query = "DELETE FROM ClassTypes WHERE CTID=@ctid";
                
                // create the command now
                SqlCommand command = new SqlCommand(query, connection);
                
                // add the parameter
                command.Parameters.AddWithValue("@ctid", classTypeId);
                
                // execute the query
                command.ExecuteNonQuery();
            }
        }
    }
}
