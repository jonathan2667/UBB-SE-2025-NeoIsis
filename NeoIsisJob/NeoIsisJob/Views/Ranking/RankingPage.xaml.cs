using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI;
using NeoIsisJob.ViewModels.Rankings;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NeoIsisJob.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RankingPage : Page
    {
        // I am sorry for whoever has to work on this code in the future. Know that the failures of this code 
        // are not by design, but a victim of time constraint, frustration and endless crashing.
        private readonly RankingsViewModel _rankingsViewModel;
        public RankingPage()
        {
            this.InitializeComponent();
            this._rankingsViewModel = new RankingsViewModel();
            this.LoadRankings();
            this.LoadMuscleGroupColors();
        }

        public void GoToMainPage_Tap(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        public void GoToWorkoutPage_Tap(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(WorkoutPage));
        }

        public void GoToCalendarPage_Tap(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CalendarPage));
        }

        public void GoToClassPage_Tap(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ClassPage));
        }

        public void GoToRankingPage_Tap(object sender, RoutedEventArgs e)
        {
            // Already on RankingPage
        }

        private void Page_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (e.OriginalSource is not Microsoft.UI.Xaml.Shapes.Path)
            {
                LoadRankings();
            }
        }

        private void Chest_Clicked(object sender, RoutedEventArgs e)
        {
            LoadMuscleGroupPanel(1, "Chest");
        }

        private void Legs_Clicked(object sender, RoutedEventArgs e)
        {
            LoadMuscleGroupPanel(2, "Legs");
        }

        private void Arms_Clicked(object sender, RoutedEventArgs e)
        {
            LoadMuscleGroupPanel(3, "Arms");
        }

        private void Abs_Clicked(object sender, RoutedEventArgs e)
        {
            LoadMuscleGroupPanel(4, "Abs");
        }

        private void Back_Clicked(object sender, RoutedEventArgs e)
        {
            LoadMuscleGroupPanel(5, "Back");
        }

        private void LoadMuscleGroupPanel(int muscleGroupId, string muscleGroupName)
        {
            var ranking = this._rankingsViewModel.GetRankingByMGID(muscleGroupId);
            if (ranking != null)
            {
                var rankingPanel = FindName("RankingPanel") as StackPanel;
                if (rankingPanel != null)
                {
                    rankingPanel.Children.Clear();
                    var rankDef = _rankingsViewModel.GetRankDefinitionForPoints(ranking.Rank);
                    rankingPanel.Children.Add(CreateMuscleGroupPanel(rankDef, muscleGroupName, ranking.Rank));
                }
            }
        }

        private void LoadMuscleGroupColors()
        {
            LoadMuscleGroupColor(1, "Chest");
            LoadMuscleGroupColor(2, "Legs");
            LoadMuscleGroupColor(3, "Arms");
            LoadMuscleGroupColor(4, "Abs");
            LoadMuscleGroupColor(5, "Back");
        }

        private void LoadMuscleGroupColor(int muscleGroupId, string muscleGroupName)
        {
            var svg = FindName(muscleGroupName) as Microsoft.UI.Xaml.Shapes.Path;
            var ranking = this._rankingsViewModel.GetRankingByMGID(muscleGroupId);
            if (svg != null && ranking != null)
            {
                svg.Fill = this._rankingsViewModel.GetRankColor(ranking.Rank);
            }
        }

        private void LoadRankings()
        {
            var rankingPanel = FindName("RankingPanel") as StackPanel;
            if (rankingPanel != null)
            {
                rankingPanel.Children.Clear();
                rankingPanel.Children.Add(new TextBlock { Text = "All Rankings Explained:", FontSize = 25 });

                foreach (var rankDef in _rankingsViewModel.GetRankDefinitions())
                {
                    rankingPanel.Children.Add(CreateRankItem(rankDef));
                }
            }
        }

        private StackPanel CreateMuscleGroupPanel(RankDefinition rankDef, string muscleGroup, int rank)
        {
            StackPanel stackPanel = new StackPanel();
            StackPanel rowStackPanel = new StackPanel { Orientation = Orientation.Horizontal };

            Image rankImage = new Image { Source = new BitmapImage(new Uri(this.BaseUri, rankDef.ImagePath)), Width = 150, Height = 150 };
            TextBlock muscleGroupName = new TextBlock { Text = muscleGroup, FontSize = 25, Foreground = new SolidColorBrush(rankDef.Color), Margin = new Thickness(20, 60, 0, 10)};
            ProgressBar progressBar = new ProgressBar { Value = rank, Minimum = rankDef.MinPoints, Maximum = rankDef.MaxPoints, Foreground = new SolidColorBrush(rankDef.Color)};
            TextBlock nextRankBlock = new TextBlock { Text = $"You require {_rankingsViewModel.GetNextRankPoints(rank)} points to reach the next ranking!"};

            rowStackPanel.Children.Add(rankImage);
            rowStackPanel.Children.Add(muscleGroupName);
            
            stackPanel.Children.Add(rowStackPanel);
            stackPanel.Children.Add(progressBar);
            stackPanel.Children.Add(nextRankBlock);
            
            return stackPanel;
        }

        private StackPanel CreateRankItem(RankDefinition rankDef)
        {
            StackPanel stackPanel = new StackPanel { Orientation = Orientation.Horizontal };

            Image rankImage = new Image { Source = new BitmapImage(new Uri(this.BaseUri, rankDef.ImagePath)), Width = 50, Height = 50 };
            TextBlock rankText = new TextBlock { Text = rankDef.Name, Foreground = new SolidColorBrush(rankDef.Color), Width = 150, Margin = new Thickness(10, 15, 0, 0) };
            TextBlock minText = new TextBlock { Text = rankDef.MinPoints.ToString(), Foreground = new SolidColorBrush(rankDef.Color), TextAlignment = TextAlignment.Center, Width = 50, Margin = new Thickness(25, 15, 0, 0) };
            TextBlock dashText = new TextBlock { Text = "-", Width = 10, Margin = new Thickness(15, 15, 0, 0) };
            TextBlock maxText = new TextBlock { Text = rankDef.MaxPoints.ToString(), Foreground = new SolidColorBrush(rankDef.Color), TextAlignment = TextAlignment.Center, Width = 50, Margin = new Thickness(15, 15, 0, 0) };

            stackPanel.Children.Add(rankImage);
            stackPanel.Children.Add(rankText);
            stackPanel.Children.Add(minText);
            stackPanel.Children.Add(dashText);
            stackPanel.Children.Add(maxText);

            return stackPanel;
        }

        private void Abs_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }
    }
}
