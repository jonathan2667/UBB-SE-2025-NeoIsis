using NeoIsisJob.Models;
using NeoIsisJob.Servs;
using NeoIsisJob.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoIsisJob.Commands;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using Windows.UI;

namespace NeoIsisJob.ViewModels.Rankings
{
    public class RankDefinition
    {
        public string Name { get; set; }
        public int MinPoints { get; set; }
        public int MaxPoints { get; set; }
        public Windows.UI.Color Color { get; set; }
        public string ImagePath { get; set; }
    }

    class RankingsViewModel
    {
        private readonly RankingsService _rankingsService;
        private readonly int _user_id = 1; // !!!!!!!!!!!!!!! HARDCODED USER VALUE !!!!!!! CHANGE THIS FOR PROD !!!!!!!!
        private readonly List<RankDefinition> _rankDefinitions;

        public RankingsViewModel()
        {
            this._rankingsService = new RankingsService();
            this._rankDefinitions = InitializeRankDefinitions();
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
            return _rankDefinitions;
        }

        public RankDefinition GetRankDefinitionForPoints(int points)
        {
            return _rankDefinitions.FirstOrDefault(r => points >= r.MinPoints && points < r.MaxPoints) 
                   ?? _rankDefinitions.Last();
        }

        public int GetNextRankPoints(int currentRank)
        {
            var currentRankDef = GetRankDefinitionForPoints(currentRank);
            var nextRank = _rankDefinitions.FirstOrDefault(r => r.MinPoints > currentRankDef.MinPoints);
            return nextRank?.MinPoints - currentRank ?? 0;
        }

        public RankingModel GetRankingByMGID(int muscleGroupid)
        {
            return this._rankingsService.GetRankingByFullID(this._user_id, muscleGroupid);
        }

        public SolidColorBrush GetRankColor(int rank)
        {
            var rankDef = GetRankDefinitionForPoints(rank);
            return new SolidColorBrush(rankDef.Color);
        }

        public string GetRankIcon(int rank)
        {
            var rankDef = GetRankDefinitionForPoints(rank);
            return rankDef.ImagePath;
        }

        public int GetRankLowerBound(int rank)
        {
            var rankDef = GetRankDefinitionForPoints(rank);
            return rankDef.MinPoints;
        }

        public int GetRankUpperBound(int rank)
        {
            var rankDef = GetRankDefinitionForPoints(rank);
            return rankDef.MaxPoints;
        }
    }
}
