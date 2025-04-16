using NeoIsisJob.Models;
using System;
using System.Collections.Generic;

namespace NeoIsisJob.Repositories.Interfaces
{
    public interface IClassRepository
    {
        List<ClassModel> GetAllClasses();
        ClassModel GetClassById(int classId);
        List<ClassModel> GetClassesForUserOnDate(int userId, DateTime date);
        void AssignClassToUser(int userId, int classId, DateTime date);
        void RemoveClassFromUser(int userId, int classId, DateTime date);
    }
} 