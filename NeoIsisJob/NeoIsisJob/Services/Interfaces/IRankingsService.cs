using NeoIsisJob.Models;
using System.Collections.Generic;

namespace NeoIsisJob.Services.Interfaces
{
    public interface IRankingsService
    {
        RankingModel GetRankingByFullID(int userId, int muscleGroupId);
        IList<RankingModel> GetAllRankingsByUserID(int userId);
        int CalculatePointsToNextRank(int currentPoints, IList<RankDefinition> rankDefinitions);
    }
} 