using NeoIsisJob.Models;
using NeoIsisJob.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Servs
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
    }
}
