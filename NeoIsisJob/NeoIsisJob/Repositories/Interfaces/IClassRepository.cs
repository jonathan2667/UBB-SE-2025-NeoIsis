using System;
using System.Collections.Generic;
using NeoIsisJob.Models;

namespace NeoIsisJob.Repositories.Interfaces
{
    public interface IClassRepository
    {
        public ClassModel GetClassModelById(int classId);
        public List<ClassModel> GetAllClassModel();
        public void AddClassModel(ClassModel classModel);
        public void DeleteClassModel(int classId);
    }
}