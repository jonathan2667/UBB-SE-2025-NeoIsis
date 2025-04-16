using NeoIsisJob.Models;
using NeoIsisJob.Repositories;
using NeoIsisJob.Repositories.Interfaces;
using NeoIsisJob.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Services
{
    public class WorkoutTypeService : IWorkoutTypeService
    {
        private readonly IWorkoutTypeRepository _workoutTypeRepository;

        public WorkoutTypeService() 
        { 
            this._workoutTypeRepository = new WorkoutTypeRepo(); 
        }

        public WorkoutTypeService(IWorkoutTypeRepository workoutTypeRepository)
        {
            this._workoutTypeRepository = workoutTypeRepository ?? throw new ArgumentNullException(nameof(workoutTypeRepository));
        }

        public void InsertWorkoutType(String workoutTypeName) 
        {
            //NAME HAS TO BE UNIQUE
            if (string.IsNullOrWhiteSpace(workoutTypeName))
                throw new ArgumentException("Workout type name cannot be empty or null.");

            try
            {
                this._workoutTypeRepository.InsertWorkoutType(workoutTypeName);
            }
            catch (SqlException ex) when (ex.Number == 2627) // SQL Server unique constraint violation
            {
                throw new Exception("A workout type with this name already exists.");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting workout type.", ex);
            }
        }

        public void DeleteWorkoutType(int workoutTypeId)
        {
            this._workoutTypeRepository.DeleteWorkoutType(workoutTypeId);
        }

        public WorkoutTypeModel GetWorkoutTypeById(int workoutTypeId) 
        {
            return this._workoutTypeRepository.GetWorkoutTypeById(workoutTypeId);
        }

        public IList<WorkoutTypeModel> GetAllWorkoutTypes()
        {
            return this._workoutTypeRepository.GetAllWorkoutTypes();
        }
    }
}
