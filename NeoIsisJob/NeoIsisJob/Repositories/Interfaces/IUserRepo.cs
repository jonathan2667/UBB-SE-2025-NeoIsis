using NeoIsisJob.Models;
using System.Collections.Generic;

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
