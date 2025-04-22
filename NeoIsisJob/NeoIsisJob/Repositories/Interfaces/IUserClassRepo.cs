using System;
using System.Collections.Generic;

using NeoIsisJob.Models;

namespace NeoIsisJob.Repositories
{
    public interface IUserClassRepo
    {
        UserClassModel GetUserClassModelById(int userId, int classId, DateTime enrollmentDate);
        List<UserClassModel> GetAllUserClassModel();
        void AddUserClassModel(UserClassModel userClass);
        void DeleteUserClassModel(int userId, int classId, DateTime enrollmentDate);
        List<UserClassModel> GetUserClassModelByDate(DateTime date);
    }
}
