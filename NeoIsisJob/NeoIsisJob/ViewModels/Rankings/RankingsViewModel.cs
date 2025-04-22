﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using NeoIsisJob.Models;
using NeoIsisJob.Commands;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using Windows.UI;
using NeoIsisJob.Services.Interfaces;

namespace NeoIsisJob.ViewModels.Rankings
{
    public class RankingsViewModel
    {
        private readonly IRankingsService rankingsService;
        private readonly int userId = 1; // !!!!!!!!!!!!!!! HARDCODED USER VALUE !!!!!!! CHANGE THIS FOR PROD !!!!!!!!
        private readonly List<RankDefinition> rankDefinitions;

        public RankingsViewModel(IRankingsService rankingsService)
        {
            this.rankingsService = rankingsService;
            this.rankDefinitions = InitializeRankDefinitions();
        }

        private List<RankDefinition> InitializeRankDefinitions()
        {
            return new List<RankDefinition>
            {
                new RankDefinition { Name = "Challenger", MinPoints = 9500, MaxPoints = 10000, Color = Colors.Aquamarine, ImagePath = "/Assets/Ranks/Rank8.png" },
                new RankDefinition { Name = "Grandmaster", MinPoints = 8500, MaxPoints = 9500, Color = Colors.OrangeRed, ImagePath = "/Assets/Ranks/Rank7.png" },
                new RankDefinition { Name = "Master", MinPoints = 7000, MaxPoints = 8500, Color = Colors.DarkViolet, ImagePath = "/Assets/Ranks/Rank6.png" },
                new RankDefinition { Name = "Elite", MinPoints = 5000, MaxPoints = 7000, Color = Colors.DarkGreen, ImagePath = "/Assets/Ranks/Rank5.png" },
                new RankDefinition { Name = "Gold", MinPoints = 3500, MaxPoints = 5000, Color = Colors.Gold, ImagePath = "/Assets/Ranks/Rank4.png" },
                new RankDefinition { Name = "Silver", MinPoints = 2250, MaxPoints = 3500, Color = Colors.Silver, ImagePath = "/Assets/Ranks/Rank3.png" },
                new RankDefinition { Name = "Bronze", MinPoints = 1000, MaxPoints = 2250, Color = Colors.SandyBrown, ImagePath = "/Assets/Ranks/Rank2.png" },
                new RankDefinition { Name = "Beginner", MinPoints = 0, MaxPoints = 1000, Color = Colors.DimGray, ImagePath = "/Assets/Ranks/Rank1.png" }
            };
        }

        public IList<RankDefinition> GetRankDefinitions()
        {
            return rankDefinitions;
        }

        public RankDefinition GetRankDefinitionForPoints(int points)
        {
            return rankDefinitions.FirstOrDefault(r => points >= r.MinPoints && points < r.MaxPoints)
                   ?? rankDefinitions.Last();
        }

        public int GetNextRankPoints(int currentRank)
        {
            return this.rankingsService.CalculatePointsToNextRank(currentRank, rankDefinitions);
        }

        public RankingModel GetRankingByMGID(int muscleGroupid)
        {
            return this.rankingsService.GetRankingByFullID(this.userId, muscleGroupid);
        }

        public SolidColorBrush GetRankColor(int rank)
        {
            var rankDefinition = GetRankDefinitionForPoints(rank);
            return new SolidColorBrush(rankDefinition.Color);
        }

        public string GetRankIcon(int rank)
        {
            var rankDefinition = GetRankDefinitionForPoints(rank);
            return rankDefinition.ImagePath;
        }

        public int GetRankLowerBound(int rank)
        {
            var rankDefinition = GetRankDefinitionForPoints(rank);
            return rankDefinition.MinPoints;
        }

        public int GetRankUpperBound(int rank)
        {
            var rankDefinition = GetRankDefinitionForPoints(rank);
            return rankDefinition.MaxPoints;
        }
    }
}
