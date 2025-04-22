using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NeoIsisJob.Models;
using NeoIsisJob.Repositories;
using NeoIsisJob.Repositories.Interfaces;
using NeoIsisJob.Services.Interfaces;

namespace NeoIsisJob.Services
{
    public class WorkoutTypeService : IWorkoutTypeService
    {
        private readonly IWorkoutTypeRepository workoutTypeRepository;

        public WorkoutTypeService()
        {
            this.workoutTypeRepository = new WorkoutTypeRepo();
        }

        public WorkoutTypeService(IWorkoutTypeRepository workoutTypeRepository)
        {
            this.workoutTypeRepository = workoutTypeRepository ?? throw new ArgumentNullException(nameof(workoutTypeRepository));
        }

        public void InsertWorkoutType(string workoutTypeName)
        {
            // NAME HAS TO BE UNIQUE
            if (string.IsNullOrWhiteSpace(workoutTypeName))
            {
                throw new ArgumentException("Workout type name cannot be empty or null.");
            }

            try
            {
                this.workoutTypeRepository.InsertWorkoutType(workoutTypeName);
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
            this.workoutTypeRepository.DeleteWorkoutType(workoutTypeId);
        }

        public WorkoutTypeModel GetWorkoutTypeById(int workoutTypeId)
        {
            return this.workoutTypeRepository.GetWorkoutTypeById(workoutTypeId);
        }

        public IList<WorkoutTypeModel> GetAllWorkoutTypes()
        {
            return this.workoutTypeRepository.GetAllWorkoutTypes();
        }
    }
}
