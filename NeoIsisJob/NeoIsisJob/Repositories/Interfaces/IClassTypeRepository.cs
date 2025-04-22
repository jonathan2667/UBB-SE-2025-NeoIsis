using System.Collections.Generic;
using NeoIsisJob.Models;

namespace NeoIsisJob.Repositories.Interfaces
{
    public interface IClassTypeRepository
    {
        ClassTypeModel GetClassTypeModelById(int classTypeId);
        List<ClassTypeModel> GetAllClassTypeModel();
        void AddClassTypeModel(ClassTypeModel classType);
        void DeleteClassTypeModel(int classTypeId);
    }
}