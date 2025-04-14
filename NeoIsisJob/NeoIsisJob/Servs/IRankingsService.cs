using NeoIsisJob.Models;
using System.Collections.Generic;

namespace NeoIsisJob.Servs
{
    public interface IRankingsService
    {
        RankingModel GetRankingByFullID(int userId, int muscleGroupId);
        IList<RankingModel> GetAllRankingsByUserID(int userId);
    }
} 