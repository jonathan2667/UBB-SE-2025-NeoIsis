using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Repo.Mocks;
using NeoIsisJob.Models;

namespace Tests.Repo.Tests
{
    [TestClass]
    public class RankingsTests
    {
        private readonly RankingsMock _rankingRepository = new RankingsMock();

        [TestMethod]
        public void TestGetByUserID()
        {
            List<RankingModel> res = (List<RankingModel>)_rankingRepository.GetAllRankingsByUserID(1);
            Assert.AreEqual(res.Count, 3);
        }

        [TestMethod]

        public void TestGetByFullID()
        {
            RankingModel res = _rankingRepository.GetRankingByFullID(1, 1);
            Assert.AreEqual(res.UserId, 1);
            Assert.AreEqual(res.MuscleGroupId, 1);
            Assert.AreEqual(res.Rank, 1);
        }

    }
}
