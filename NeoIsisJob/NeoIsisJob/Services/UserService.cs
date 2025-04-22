using System;
using System.Collections.Generic;
using NeoIsisJob.Models;
using NeoIsisJob.Repositories;

namespace NeoIsisJob.Services
{
    public class UserService
    {
        private readonly IUserRepo userRepository;

        public UserService(IUserRepo userRepo)
        {
            userRepository = userRepo;
        }

        public int RegisterNewUser()
        {
            return userRepository.InsertUser();
        }

        public UserModel GetUser(int userId)
        {
            return userRepository.GetUserById(userId);
        }

        public bool RemoveUser(int userId)
        {
            return userRepository.DeleteUserById(userId);
        }

        public List<UserModel> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }
    }
}
