using System;
using Microsoft.UI.Xaml;
using NeoIsisJob.Repos;
using NeoIsisJob.Servs;
using NeoIsisJob.Data;
using NeoIsisJob.ViewModels;
using System.Runtime.CompilerServices;

namespace NeoIsisJob
{
    public sealed partial class MainWindow : Window
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly UserViewModel _userViewModel;
        private readonly UserService _userService;
        private readonly UserRepo _userRepo;

        public MainWindow()
        {
            this.InitializeComponent();
            _dbHelper = new DatabaseHelper();
            // Initialize UserRepo
            _userRepo = new UserRepo(_dbHelper);
            // Initialize UserService
            _userService = new UserService(_userRepo);
            // Initialize UserViewModel
            _userViewModel = new UserViewModel(_userService);
            LoadUsers();
        }

        // Fetch users from view model and display them
        private void LoadUsers()
        {
            // Get all users from the view model
            _userViewModel.RefreshUsers();
            var users = _userViewModel.Users;

            // Bind the users to the ListView
            UsersListView.ItemsSource = users;
        }


        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            _userViewModel.AddUser();
            LoadUsers();
        }

        private void GetUser_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(UserIdTextBox.Text, out int userId)) { _userViewModel.GetUserById(userId); }
            LoadUsers();
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(UserIdTextBox.Text, out int userId)) { _userViewModel.DeleteUser(userId); }
            LoadUsers();
        }
    }
}
