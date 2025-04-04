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

namespace NeoIsisJob.ViewModels.Rankings
{
    class RankingsViewModel
    {
        private readonly RankingsService _rankingsService;
        private readonly int _user_id = 1; // !!!!!!!!!!!!!!! HARDCODED USER VALUE !!!!!!! CHANGE THIS FOR PROD !!!!!!!!
    
        //private ObservableCollection<RankingModel> _rankings;

        public RankingsViewModel()
        {
            this._rankingsService = new RankingsService();
        }

        public RankingModel GetRankingByMGID(int mgid)
        {
            RankingModel ranking = this._rankingsService.GetRankingByFullID(this._user_id, mgid);
            
            return ranking;
            
        }

        public SolidColorBrush GetRankColor(int rank)
        {
            return rank switch
            {
                <= 1000 => new SolidColorBrush(Colors.DimGray),
                >= 1000 and < 2250 => new SolidColorBrush(Colors.SandyBrown),
                >=2250 and < 3500 => new SolidColorBrush(Colors.Silver),
                >=3500 and < 5000 => new SolidColorBrush(Colors.Gold),
                >=5000 and < 7000 => new SolidColorBrush(Colors.DarkGreen),
                >= 7000 and < 8500 => new SolidColorBrush(Colors.DarkViolet),
                >= 8500 and < 9500 => new SolidColorBrush(Colors.OrangeRed),
                > 9500 => new SolidColorBrush(Colors.Aquamarine)
                
            };
        }

        public String GetRankIcon(int rank)
        {
            return rank switch
            {
                <= 1000 => "/Assets/Ranks/Rank1.png",
                >= 1000 and < 2250 => "/Assets/Ranks/Rank2.png",
                >= 2250 and < 3500 => "/Assets/Ranks/Rank3.png",
                >= 3500 and < 5000 => "/Assets/Ranks/Rank4.png",
                >= 5000 and < 7000 => "/Assets/Ranks/Rank5.png",
                >= 7000 and < 8500 => "/Assets/Ranks/Rank6.png",
                >= 8500 and < 9500 => "/Assets/Ranks/Rank7.png",
                > 9500 => "/Assets/Ranks/Rank8.png"
            };
        }

        public int GetRankLowerBound(int rank)
        {
            return rank switch
            {
                <= 1000 => 0,
                >= 1000 and < 2250 => 1000,
                >= 2250 and < 3500 => 2250,
                >= 3500 and < 5000 => 3500,
                >= 5000 and < 7000 => 5000,
                >= 7000 and < 8500 => 7000,
                >= 8500 and < 9500 => 8500,
                > 9500 => 9500
            };
        }
        public int GetRankUpperBound(int rank)
        {
            return rank switch
            {
                <= 1000 => 1000,
                >= 1000 and < 2250 => 2250,
                >= 2250 and < 3500 => 3500,
                >= 3500 and < 5000 => 5000,
                >= 5000 and < 7000 => 7000,
                >= 7000 and < 8500 => 8500,
                >= 8500 and < 9500 => 9500,
                > 9500 => 10000
            };
        }

    }
}
