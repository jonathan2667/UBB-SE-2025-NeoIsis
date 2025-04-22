using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using NeoIsisJob.Repositories;
using NeoIsisJob.Services;
using NeoIsisJob.Data;
using NeoIsisJob.ViewModels;
using NeoIsisJob.Views;
using Microsoft.UI.Xaml.Controls;

namespace NeoIsisJob
{
    public sealed partial class MainWindow : Window
    {
        // instance for singleton
        public static MainWindow Instance { get; private set; }

        public MainWindow()
        {
            this.InitializeComponent();
            Instance = this;

            // go directly to the main page
            MainFrame.Navigate(typeof(MainPage));
        }
    }
}
