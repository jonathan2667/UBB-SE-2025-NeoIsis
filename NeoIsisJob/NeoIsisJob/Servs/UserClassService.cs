using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoIsisJob.Models;
using NeoIsisJob.Repos;

// please add validation for the input parameters

namespace NeoIsisJob.Servs
{
    public class UserClassService
    {
        private readonly UserClassRepo _userClassRepo;

        public UserClassService() { this._userClassRepo = new UserClassRepo(); }

        public List<UserClassModel> GetAllUserClasses()
        {
            return _userClassRepo.GetAllUserClassModel();
        }

        public UserClassModel GetUserClassById(int ucid, int cid, DateTime date)
        {
            return _userClassRepo.GetUserClassModelById(ucid, cid, date);
        }

        public void AddUserClass(UserClassModel userClassModel)
        {
            _userClassRepo.AddUserClassModel(userClassModel);
        }

        public void DeleteUserClass(int ucid, int cid, DateTime date)
        {
            _userClassRepo.DeleteUserClassModel(ucid, cid, date);
        }

        // In case you guys need to update a user class
        // create a method here that calls the UpdateUserClassModel method from the UserClassRepo +
        // create the UpdateUserClassModel method in the UserClassRepo
    }
}
