using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NeoIsisJob.Models;
using NeoIsisJob.Data.Interfaces;
using NeoIsisJob.Data;

namespace NeoIsisJob.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly IDatabaseHelper databaseHelper;

        public UserRepo()
        {
            databaseHelper = new DatabaseHelper();
        }
        public UserRepo(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }

        public UserModel GetUserById(int userId)
        {
            string query = "SELECT UID FROM Users WHERE UID = @Id";
            var parameters = new[]
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = userId }
            };

            DataTable result = databaseHelper.ExecuteReader(query, parameters);

            if (result.Rows.Count > 0)
            {
                return new UserModel(Convert.ToInt32(result.Rows[0]["UID"]));
            }

            return new UserModel();
        }

        public int InsertUser()
        {
            string query = "INSERT INTO Users DEFAULT VALUES; SELECT SCOPE_IDENTITY();";

            return databaseHelper.ExecuteScalar<int>(query);
        }

        public bool DeleteUserById(int userId)
        {
            string query = "DELETE FROM Users WHERE UID = @Id";
            var parameters = new[]
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = userId }
            };

            int rowsAffected = databaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public List<UserModel> GetAllUsers()
        {
            string query = "SELECT UID FROM Users";
            DataTable result = databaseHelper.ExecuteReader(query, null);

            var users = new List<UserModel>();

            foreach (DataRow row in result.Rows)
            {
                users.Add(new UserModel(Convert.ToInt32(row["UID"])));
            }

            return users;
        }
    }
}
