using NeoIsisJob.Models;
using NeoIsisJob.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Servs
{
    class RankingsService
    {
        private readonly RankingsRepo _rankingsRepo;

        public RankingsService()
        {
            this._rankingsRepo = new RankingsRepo();
        }

        public IList<RankingModel> GetAllRankingsByUID(int uid)
        {
            return this._rankingsRepo.GetAllRankingsByUID(uid);
        }

        public RankingModel GetRankingByFullID(int uid, int mgid)
        {
            return this._rankingsRepo.GetRankingByFullID(uid, mgid);
        }
    }
}
