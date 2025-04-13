using System;
using System.Collections.Generic;
using NeoIsisJob.Models;
using NeoIsisJob.Repos;

namespace NeoIsisJob.Servs
{
    public class UserService
    {
        private readonly UserRepo _userRepository;

        public UserService(UserRepo userRepo) { _userRepository = userRepo; }

        public int RegisterNewUser() { return _userRepository.InsertUser(); }

        public UserModel GetUser(int userId) { return _userRepository.GetUserById(userId); }

        public bool RemoveUser(int userId) { return _userRepository.DeleteUserById(userId); }

        public List<UserModel> GetAllUsers() { return _userRepository.GetAllUsers(); }
    }
}
