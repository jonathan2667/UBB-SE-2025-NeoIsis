using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NeoIsisJob.Data;
using NeoIsisJob.Data.Interfaces;
using NeoIsisJob.Models;

namespace NeoIsisJob.Repositories
{
    public class UserClassRepo : IUserClassRepo
    {
        private readonly IDatabaseHelper databaseHelper;

        public UserClassRepo()
        {
            databaseHelper = new DatabaseHelper();
        }

        public UserClassRepo(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }

        public UserClassModel GetUserClassModelById(int userId, int classId, DateTime enrollmentDate)
        {
            string query = "SELECT UID, CID, Date FROM UserClasses WHERE UID = @UID AND CID = @CID AND Date = @Date";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UID", userId),
                new SqlParameter("@CID", classId),
                new SqlParameter("@Date", enrollmentDate)
            };

            DataTable result = databaseHelper.ExecuteReader(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new UserClassModel(
                    Convert.ToInt32(row["UID"]),
                    Convert.ToInt32(row["CID"]),
                    Convert.ToDateTime(row["Date"]));
            }

            return new UserClassModel();
        }

        public List<UserClassModel> GetAllUserClassModel()
        {
            string query = "SELECT UID, CID, Date FROM UserClasses";

            DataTable result = databaseHelper.ExecuteReader(query, null);
            List<UserClassModel> userClasses = new List<UserClassModel>();

            foreach (DataRow row in result.Rows)
            {
                userClasses.Add(new UserClassModel(
                    Convert.ToInt32(row["UID"]),
                    Convert.ToInt32(row["CID"]),
                    Convert.ToDateTime(row["Date"])));
            }

            return userClasses;
        }

        public void AddUserClassModel(UserClassModel userClass)
        {
            string query = "INSERT INTO UserClasses (UID, CID, Date) VALUES (@UID, @CID, @Date)";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UID", userClass.UserId),
                new SqlParameter("@CID", userClass.ClassId),
                new SqlParameter("@Date", userClass.EnrollmentDate)
            };

            databaseHelper.ExecuteNonQuery(query, parameters);
        }

        public void DeleteUserClassModel(int userId, int classId, DateTime enrollmentDate)
        {
            string query = "DELETE FROM UserClasses WHERE UID = @UID AND CID = @CID AND Date = @Date";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UID", userId),
                new SqlParameter("@CID", classId),
                new SqlParameter("@Date", enrollmentDate)
            };

            databaseHelper.ExecuteNonQuery(query, parameters);
        }

        public List<UserClassModel> GetUserClassModelByDate(DateTime date)
        {
            string query = "SELECT UID, CID, Date FROM UserClasses WHERE Date = @Date";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Date", date)
            };

            DataTable result = databaseHelper.ExecuteReader(query, parameters);
            List<UserClassModel> userClasses = new List<UserClassModel>();

            foreach (DataRow row in result.Rows)
            {
                userClasses.Add(new UserClassModel(
                    Convert.ToInt32(row["UID"]),
                    Convert.ToInt32(row["CID"]),
                    Convert.ToDateTime(row["Date"])));
            }

            return userClasses;
        }
    }
}
