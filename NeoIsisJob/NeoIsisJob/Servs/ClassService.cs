using NeoIsisJob.Repos;
using NeoIsisJob.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// please add validation for the input parameters

namespace NeoIsisJob.Servs
{
    public class ClassService
    {
        private readonly ClassRepo _classRepo;

        public ClassService() { this._classRepo = new ClassRepo(); }

        public List<ClassModel> GetAllClasses()
        {
            return this._classRepo.GetAllClassModel();
        }
    
        public ClassModel GetClassById(int cid)
        {
            return _classRepo.GetClassModelById(cid);
        }

        public void AddClass(ClassModel classModel)
        {
            _classRepo.AddClassModel(classModel);
        }

        public void DeleteClass(int cid)
        {
            _classRepo.DeleteClassModel(cid);
        }

        // In case you guys need to update a class
        // create a method here that calls the UpdateClassModel method from the ClassRepo + 
        // create the UpdateClassModel method in the ClassRepo
    }
}
