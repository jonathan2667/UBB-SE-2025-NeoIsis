using System;
using System.Collections.Generic;
using NeoIsisJob.Models;

namespace NeoIsisJob.Services.Interfaces
{
    public interface IClassService
    {
        List<ClassModel> GetAllClasses();
        ClassModel GetClassById(int classId);
        void AddClass(ClassModel classModel);
        void DeleteClass(int classId);
        string ConfirmRegistration(int userId, int classId, DateTime date);
    }
}