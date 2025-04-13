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
    class RankingsRepo
    {
        private readonly DatabaseHelper _databaseHelper;

        public RankingsRepo()
        {
            this._databaseHelper = new DatabaseHelper();
        }

        public RankingModel GetRankingByFullID(int userId, int muscleGroupId)
        {
            RankingModel ranking = null;

            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                connection.Open();
                String query = "Select * from Rankings where UID=@UID and MGID=@MGID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UID", userId);
                command.Parameters.AddWithValue("@MGID", muscleGroupId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ranking = new RankingModel(
                        Convert.ToInt32(reader["UID"]),
                        Convert.ToInt32(reader["MGID"]),
                        Convert.ToInt32(reader["Rank"])
                    );
                }
            }

            return ranking;
        }

        public IList<RankingModel> GetAllRankingsByUserID(int userId) 
        {
            IList<RankingModel> rankings = new List<RankingModel>();

            using (SqlConnection connection = this._databaseHelper.GetConnection())
            {
                connection.Open();

                String query = "Select * from Rankings where UID=@UID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UID", userId);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    rankings.Add(new RankingModel(
                        Convert.ToInt32(reader["UID"]),
                        Convert.ToInt32(reader["MGID"]),
                        Convert.ToInt32(reader["Rank"])
                    ));
                }
            }

            return rankings;
        }
    }
}
