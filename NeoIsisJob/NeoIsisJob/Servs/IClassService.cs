using NeoIsisJob.Models;
using System;
using System.Collections.Generic;

namespace NeoIsisJob.Servs
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