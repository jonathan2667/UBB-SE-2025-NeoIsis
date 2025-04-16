using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Repo.Mocks;

namespace Tests.Repo.Tests
{
    [TestClass]
    class RankingsTests
    {
        private readonly RankingsMock _rankingRepository = new RankingsMock();

        [TestMethod]
        public void TestGetByUserID()
        {
        }
    }
}
