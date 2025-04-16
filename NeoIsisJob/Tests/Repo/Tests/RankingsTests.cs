using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Repo.Mocks;
using NeoIsisJob.Models;
using NeoIsisJob.Data.Interfaces;
using Moq;
using NeoIsisJob.Repositories;

namespace Tests.Repo.Tests
{
    [TestClass]
    public class RankingsTests
    {
        private  Mock<IDatabaseHelper> _mockDatabaseHelper;
        private  RankingsRepository _rankingRepository;

        [TestInitialize]
        public void Setup()
        {
            _rankingRepository = new RankingsRepository(_mockDatabaseHelper.Object);
        }

    }
}
