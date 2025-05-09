﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NeoIsisJob.Data;
using NeoIsisJob.Data.Interfaces;
using NeoIsisJob.Models;
using NeoIsisJob.Repositories.Interfaces;

namespace NeoIsisJob.Repositories
{
    public class RankingsRepository : IRankingsRepository
    {
        private readonly IDatabaseHelper databaseHelper;

        public RankingsRepository()
        {
            this.databaseHelper = new DatabaseHelper();
        }

        public RankingsRepository(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }

        public RankingModel GetRankingByFullID(int userId, int muscleGroupId)
        {
            string query = "SELECT * FROM Rankings WHERE UID = @UID AND MGID = @MGID";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UID", userId),
                new SqlParameter("@MGID", muscleGroupId)
            };

            DataTable dt = databaseHelper.ExecuteReader(query, parameters);

            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                return new RankingModel(
                    Convert.ToInt32(row["UID"]),
                    Convert.ToInt32(row["MGID"]),
                    Convert.ToInt32(row["Rank"]));
            }

            return null;
        }

        public IList<RankingModel> GetAllRankingsByUserID(int userId)
        {
            string query = "SELECT * FROM Rankings WHERE UID = @UID";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UID", userId)
            };

            DataTable dt = databaseHelper.ExecuteReader(query, parameters);
            IList<RankingModel> rankings = new List<RankingModel>();

            foreach (DataRow row in dt.Rows)
            {
                rankings.Add(new RankingModel(
                    Convert.ToInt32(row["UID"]),
                    Convert.ToInt32(row["MGID"]),
                    Convert.ToInt32(row["Rank"])));
            }

            return rankings;
        }
    }
}
