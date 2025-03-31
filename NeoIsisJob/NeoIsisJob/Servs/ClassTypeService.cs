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
        private readonly ClassTypeRepo _classTypeRepo;

        public ClassTypeService(ClassTypeRepo classTypeRepo) { _classTypeRepo = classTypeRepo; }

        public List<ClassTypeModel> GetAllClassTypes()
        {
            return _classTypeRepo.GetAllClassTypeModel();
        }

        public ClassTypeModel GetClassTypeById(int ctid)
        {
            return _classTypeRepo.GetClassTypeModelById(ctid);
        }

        public void AddClassType(ClassTypeModel classTypeModel)
        {
            _classTypeRepo.AddClassTypeModel(classTypeModel);
        }

        public void DeleteClassType(int ctid)
        {
            _classTypeRepo.DeleteClassTypeModel(ctid);
        }

        // In case you guys need to update a class type
        // create a method here that calls the UpdateClassTypeModel method from the ClassTypeRepo +
        // create the UpdateClassTypeModel method in the ClassTypeRepo
    }
}
