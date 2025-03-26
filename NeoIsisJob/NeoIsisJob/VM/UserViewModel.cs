using System.Collections.ObjectModel;
using System.Linq;
using NeoIsisJob.Models;
using NeoIsisJob.Servs;

namespace NeoIsisJob.ViewModels
{
    public class UserViewModel
    {
        private readonly UserService _userService;

        public ObservableCollection<UserModel> Users { get; set; }
        public UserModel? SelectedUser { get; set; } // Make SelectedUser nullable

        public UserViewModel(UserService userService)
        {
            _userService = userService;
            Users = new ObservableCollection<UserModel>(_userService.GetAllUsers());
            SelectedUser = null; // Initialize SelectedUser to null
        }

        public void AddUser()
        {
            int newUserId = _userService.RegisterNewUser();
            Users.Add(new UserModel(newUserId));
        }

        public void DeleteUser(int userId)
        {
            if (_userService.RemoveUser(userId))
            {
                var userToRemove = Users.FirstOrDefault(u => u.Id == userId);
                if (userToRemove != null)
                {
                    Users.Remove(userToRemove);
                }
            }
        }

        public UserModel? GetUserById(int userId)
        {
            SelectedUser = _userService.GetUser(userId);
            return SelectedUser;
        }

        public void RefreshUsers()
        {
            Users.Clear();
            foreach (var user in _userService.GetAllUsers())
            {
                Users.Add(user);
            }
        }
    }
}
