using System.Collections.Generic;
using NeoIsisJob.Models;

namespace NeoIsisJob.Repositories
{
    public interface IUserRepo
    {
        UserModel GetUserById(int userId);
        int InsertUser();
        bool DeleteUserById(int userId);
        List<UserModel> GetAllUsers();
    }
}
