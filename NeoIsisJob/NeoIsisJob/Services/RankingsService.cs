using NeoIsisJob.Models;
using NeoIsisJob.Repositories.Interfaces;
using NeoIsisJob.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Services
{
    public class RankingsService : IRankingsService
    {
        private readonly IRankingsRepository _rankingsRepository;

        public RankingsService(IRankingsRepository rankingsRepository)
        {
            this._rankingsRepository = rankingsRepository;
        }

        public IList<RankingModel> GetAllRankingsByUserID(int userId)
        {
            return this._rankingsRepository.GetAllRankingsByUserID(userId);
        }

        public RankingModel GetRankingByFullID(int userId, int muscleGroupId)
        {
            return this._rankingsRepository.GetRankingByFullID(userId, muscleGroupId);
        }

        public int CalculatePointsToNextRank(int currentPoints, IList<RankDefinition> rankDefinitions)
        {
            // Find the current rank based on the points
            var currentRankDefinition = rankDefinitions.FirstOrDefault(r => 
                currentPoints >= r.MinPoints && currentPoints < r.MaxPoints) 
                ?? rankDefinitions.Last();
            
            // Find the next rank (with higher minimum points)
            var nextRank = rankDefinitions.FirstOrDefault(r => r.MinPoints > currentRankDefinition.MinPoints);
            
            // Calculate points needed to reach next rank or return 0 if at highest rank
            return nextRank?.MinPoints - currentPoints ?? 0;
        }
    }
}
