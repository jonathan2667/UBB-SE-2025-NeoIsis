using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoIsisJob.Models;
using NeoIsisJob.Repositories;
using Windows.System;

// please add validation for the input parameters
namespace NeoIsisJob.Services
{
    public class UserClassService
    {
        private readonly IUserClassRepo userClassRepository;

        public UserClassService()
        {
            this.userClassRepository = new UserClassRepo();
        }

        public List<UserClassModel> GetAllUserClasses()
        {
            return userClassRepository.GetAllUserClassModel();
        }

        public UserClassModel GetUserClassById(int userId, int classId, DateTime date)
        {
            return userClassRepository.GetUserClassModelById(userId, classId, date);
        }

        public void AddUserClass(UserClassModel userClassModel)
        {
            userClassRepository.AddUserClassModel(userClassModel);
        }

        public void DeleteUserClass(int userId, int classId, DateTime date)
        {
            userClassRepository.DeleteUserClassModel(userId, classId, date);
        }

        // In case you guys need to update a user class
        // create a method here that calls the UpdateUserClassModel method from the UserClassRepo +
        // create the UpdateUserClassModel method in the UserClassRepo
    }
}
