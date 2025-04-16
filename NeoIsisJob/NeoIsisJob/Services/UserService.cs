using System;
using System.Collections.Generic;
using NeoIsisJob.Models;
using NeoIsisJob.Repositories;

namespace NeoIsisJob.Services
{
    public class UserService
    {
        private readonly IUserRepo _userRepository;

        public UserService(IUserRepo userRepo) { _userRepository = userRepo; }

        public int RegisterNewUser() { return _userRepository.InsertUser(); }

        public UserModel GetUser(int userId) { return _userRepository.GetUserById(userId); }

        public bool RemoveUser(int userId) { return _userRepository.DeleteUserById(userId); }

        public List<UserModel> GetAllUsers() { return _userRepository.GetAllUsers(); }
    }
}
