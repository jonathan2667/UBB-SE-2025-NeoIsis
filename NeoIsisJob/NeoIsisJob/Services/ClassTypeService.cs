using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoIsisJob.Repositories;
using NeoIsisJob.Models;
using NeoIsisJob.Repositories.Interfaces;

// please add validation for the input parameters

namespace NeoIsisJob.Services
{
    public class ClassTypeService
    {
        private readonly IClassTypeRepository _classTypeRepository;

        public ClassTypeService() 
        { 
            this._classTypeRepository = new ClassTypeRepository(); 
        }

        public ClassTypeService(IClassTypeRepository classTypeRepository)
        {
            this._classTypeRepository = classTypeRepository;
        }

        public List<ClassTypeModel> GetAllClassTypes()
        {
            return _classTypeRepository.GetAllClassTypeModel();
        }

        public ClassTypeModel GetClassTypeById(int classTypeId)
        {
            return _classTypeRepository.GetClassTypeModelById(classTypeId);
        }

        public void AddClassType(ClassTypeModel classTypeModel)
        {
            _classTypeRepository.AddClassTypeModel(classTypeModel);
        }

        public void DeleteClassType(int classTypeId)
        {
            _classTypeRepository.DeleteClassTypeModel(classTypeId);
        }

        // In case you guys need to update a class type
        // create a method here that calls the UpdateClassTypeModel method from the ClassTypeRepository +
        // create the UpdateClassTypeModel method in the ClassTypeRepository
    }
}
