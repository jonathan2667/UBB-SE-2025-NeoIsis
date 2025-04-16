using NeoIsisJob.Models;
using System.Collections.Generic;

namespace NeoIsisJob.Repositories.Interfaces
{
    public interface IRankingsRepository
    {
        RankingModel GetRankingByFullID(int userId, int muscleGroupId);
        IList<RankingModel> GetAllRankingsByUserID(int userId);
    }
} 