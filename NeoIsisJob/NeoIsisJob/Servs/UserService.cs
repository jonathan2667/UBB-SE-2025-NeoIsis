using System;
using System.Collections.Generic;
using NeoIsisJob.Models;
using NeoIsisJob.Repos;

namespace NeoIsisJob.Servs
{
    public class UserService
    {
        private readonly UserRepo _userRepo;

        public UserService(UserRepo userRepo) { _userRepo = userRepo; }

        public int RegisterNewUser() { return _userRepo.InsertUser(); }

        public UserModel GetUser(int id) { return _userRepo.GetUserById(id); }

        public bool RemoveUser(int id) { return _userRepo.DeleteUserById(id); }

        public List<UserModel> GetAllUsers() { return _userRepo.GetAllUsers(); }
    }
}
