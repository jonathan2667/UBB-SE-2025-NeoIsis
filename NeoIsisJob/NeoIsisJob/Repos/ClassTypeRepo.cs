using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using NeoIsisJob.Data;
using NeoIsisJob.Models;

namespace NeoIsisJob.Repos
{
    public class ClassTypeRepo
    {
        private readonly DatabaseHelper _dbHelper;

        public ClassTypeRepo() { this._dbHelper = new DatabaseHelper(); }

        public ClassTypeModel GetClassTypeModelById(int ctid)
        {
            using (SqlConnection connection = this._dbHelper.GetConnection())
            {
                // open the connection
                connection.Open();

                // create the query
                string query = "SELECT CTID, Name FROM ClassTypes WHERE CTID=@ctid";

                // create the command now
                SqlCommand command = new SqlCommand(query, connection);

                // add the parameter
                command.Parameters.AddWithValue("@ctid", ctid);

                // read the data
                SqlDataReader reader = command.ExecuteReader();

                // now check if the type exists -> if yes return it
                if (reader.Read())
                {
                    return new ClassTypeModel(Convert.ToString(reader["Name"]) ?? string.Empty);
                }

                // otherwise return empty instance
                return new ClassTypeModel();
            }
        }

        public List<ClassTypeModel> GetAllClassTypeModel()
        {
            List<ClassTypeModel> classTypes = new List<ClassTypeModel>();

            using (SqlConnection connection = this._dbHelper.GetConnection())
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
                    classTypes.Add(new ClassTypeModel(reader["Name"].ToString() ?? string.Empty) { Id = (int)reader["CTID"] });
                }
            }

            return classTypes;
        }

        public void AddClassTypeModel(ClassTypeModel classType)
        {
            using (SqlConnection connection = this._dbHelper.GetConnection())
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

        public void DeleteClassTypeModel(int ctid)
        {
            using (SqlConnection connection = this._dbHelper.GetConnection())
            {
                // open the connection
                connection.Open();

                // create the query
                string query = "DELETE FROM ClassTypes WHERE CTID=@ctid";
                
                // create the command now
                SqlCommand command = new SqlCommand(query, connection);
                
                // add the parameter
                command.Parameters.AddWithValue("@ctid", ctid);
                
                // execute the query
                command.ExecuteNonQuery();
            }
        }
    }
}
