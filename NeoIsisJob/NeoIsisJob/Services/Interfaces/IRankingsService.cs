using System.Collections.Generic;
using NeoIsisJob.Models;

namespace NeoIsisJob.Services.Interfaces
{
    public interface IRankingsService
    {
        RankingModel GetRankingByFullID(int userId, int muscleGroupId);
        IList<RankingModel> GetAllRankingsByUserID(int userId);
        int CalculatePointsToNextRank(int currentPoints, IList<RankDefinition> rankDefinitions);
    }
}