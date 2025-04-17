using System.Collections.ObjectModel;
using System.Linq;
using NeoIsisJob.Models;
using NeoIsisJob.Services;

namespace NeoIsisJob.ViewModels
{
    public class UserViewModel
    {
        private readonly UserService userService;

        public ObservableCollection<UserModel> Users { get; set; }
        public UserModel? SelectedUser { get; set; } // Make SelectedUser nullable

        public UserViewModel(UserService userService)
        {
            this.userService = userService;
            Users = new ObservableCollection<UserModel>(this.userService.GetAllUsers());
            SelectedUser = null; // Initialize SelectedUser to null
        }

        public void AddUser()
        {
            int newUserId = userService.RegisterNewUser();
            Users.Add(new UserModel(newUserId));
        }

        public void DeleteUser(int userId)
        {
            if (userService.RemoveUser(userId))
            {
                var userToRemove = Users.FirstOrDefault(user => user.Id == userId);
                if (userToRemove != null)
                {
                    Users.Remove(userToRemove);
                }
            }
        }

        public UserModel? GetUserById(int userId)
        {
            SelectedUser = userService.GetUser(userId);
            return SelectedUser;
        }

        public void RefreshUsers()
        {
            Users.Clear();
            foreach (var user in userService.GetAllUsers())
            {
                Users.Add(user);
            }
        }
    }
}
