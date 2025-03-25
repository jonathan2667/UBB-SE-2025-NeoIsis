using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using NeoIsisJob.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NeoIsisJob
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private UsersModel userModel;
        public MainWindow()
        {
            this.InitializeComponent();
            userModel = new UsersModel();
            userModel.Id = 1;
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = userModel.Id.ToString();
            Console.WriteLine(userModel.Id);
        }

        private void myTesting_Click(object sender, RoutedEventArgs e)
        {
            testingButton.Content = "mata2";
        }
    }
}
