using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoIsisJob.Repos;
using NeoIsisJob.Models;

// please add validation for the input parameters

namespace NeoIsisJob.Servs
{
    public class ClassTypeService
    {
        private readonly ClassTypeRepo _classTypeRepository;

        public ClassTypeService() { this._classTypeRepository = new ClassTypeRepo(); }

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
        // create a method here that calls the UpdateClassTypeModel method from the ClassTypeRepo +
        // create the UpdateClassTypeModel method in the ClassTypeRepo
    }
}
