using NeoIsisJob.Repositories;
using NeoIsisJob.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NeoIsisJob.Services.Interfaces;
using NeoIsisJob.Repositories.Interfaces;

// please add validation for the input parameters

namespace NeoIsisJob.Services
{
    public class ClassService : IClassService
    {
        //private readonly ClassRepository _classRepository;
        private readonly IClassRepository _classRepository;
        private readonly UserClassService _userClassService;

        public ClassService() 
        { 
            this._classRepository = new ClassRepository();
            this._userClassService = new UserClassService();
        }

        public ClassService(IClassRepository classRepository)
        {
            this._classRepository = classRepository;
            this._userClassService = new UserClassService();
        }

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

        public string ConfirmRegistration(int userId, int classId, DateTime date)
        {
            // Validate date is not in the past
            if (date < DateTime.Today)
            {
                return "Please choose a valid date (today or future)";
            }

            try
            {
                var userClass = new UserClassModel
                {
                    UserId = userId,
                    ClassId = classId,
                    EnrollmentDate = date
                };

                _userClassService.AddUserClass(userClass);
                Debug.WriteLine($"Successfully registered for class {GetClassById(classId).Name}");
                return ""; // Return empty string for success
            }
            catch (Exception ex)
            {
                string errorMessage = $"Registration failed: {ex.Message}";
                Debug.WriteLine(errorMessage);
                return errorMessage;
            }
        }
    }
}
