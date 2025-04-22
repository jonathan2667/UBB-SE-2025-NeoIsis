using System.Collections.Generic;
using NeoIsisJob.Models;

namespace NeoIsisJob.Repositories.Interfaces
{
    public interface IRankingsRepository
    {
        RankingModel GetRankingByFullID(int userId, int muscleGroupId);
        IList<RankingModel> GetAllRankingsByUserID(int userId);
    }
}