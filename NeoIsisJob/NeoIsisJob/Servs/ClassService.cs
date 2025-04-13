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
        private readonly ClassRepo _classRepository;

        public ClassService() { this._classRepository = new ClassRepo(); }

        public List<ClassModel> GetAllClasses()
        {
            return this._classRepository.GetAllClassModel();
        }
    
        public ClassModel GetClassById(int classId)
        {
            return _classRepository.GetClassModelById(classId);
        }

        public void AddClass(ClassModel classModel)
        {
            _classRepository.AddClassModel(classModel);
        }

        public void DeleteClass(int classId)
        {
            _classRepository.DeleteClassModel(classId);
        }

        // In case you guys need to update a class
        // create a method here that calls the UpdateClassModel method from the ClassRepo + 
        // create the UpdateClassModel method in the ClassRepo
    }
}
